using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

/// <summary>
/// 간단한 콘솔 기반 플레이어 관리 프로그램
/// - 레벨 기준 데이터는 CSV에서 불러오며, 플레이어 데이터는 JSON으로 저장/로드합니다。
/// - 기능: 플레이어 생성/조회/저장/삭제/정렬/수정 및 통계 리포트 출력
/// </summary>
public class Program
{
    // 레벨 관련 데이터(예: 각 레벨에 도달하기 위한 누적 경험치)
    // CreateLevelCSV에서 초기화/로드됩니다.
    static public List<LevelData> levelDataList = new List<LevelData>();

    // 플레이어 데이터 리스트 (메모리 내 작업 대상)
    // 프로그램 시작 시 SaveLoadJson에서 로드됩니다.
    static public List<PlayerData> playerDataList = new List<PlayerData>();

    /// <summary>
    /// 프로그램 진입점
    /// 흐름:
    /// 1) 레벨 CSV 생성/로드
    /// 2) 플레이어 데이터 로드
    /// 3) 메뉴 기반 반복 처리 (종료 시까지)
    /// </summary>
    static void Main(string[] args)
    {
        // 한 번만 필요한 레벨 CSV 파일 생성 (이미 있으면 스킵)
        CreateLevelCSV.CreateCSV();

        bool exit = true;

        // 저장된 플레이어 데이터와 레벨 데이터를 불러옵니다.
        playerDataList = LoadPlayerData();
        levelDataList = LoadLevelData();

        // 프로그램 종료 명령을 받을 때까지 메뉴 반복
        while (exit)
        {
            WriteLine();

            switch( DisplayMenu() )
            {
                case 1:
                    // 플레이어 생성: 입력으로 이름과 경험치 받아 레벨 계산 후 리스트에 추가
                    PlayerData newPlayer = CreatePlayerData();
                    playerDataList.Add(newPlayer);
                    WriteLine("Player data created.");
                    break;

                case 2:
                    // 플레이어 전체 출력. 데이터가 없으면 안내 메시지 출력
                    if(playerDataList.Count == 0)
                    {
                        WriteLine("No player data available.");
                        break;
                    }

                    foreach (var player in playerDataList)
                    {
                        WriteLine($"Name: {player.Name}, Level: {player.Level}, Exp: {player.Exp}");
                    }
                    break;

                case 3:
                    // 현재 메모리 플레이어 리스트를 JSON으로 저장
                    SavePlayerData();
                    WriteLine("Player data saved.");
                    break;

                case 4:
                    // 이름으로 플레이어 삭제
                    DeletePlayerData();
                    WriteLine("Player data deleted");
                    break;

                case 5:
                    // 레벨 오름차순 정렬
                    SortPlayerDataByLevel(true);
                    WriteLine("Player data sorted by level -> Increase");
                    break;

                case 6:
                    // 레벨 내림차순 정렬
                    SortPlayerDataByLevel(false);
                    WriteLine("Player data sorted by level -> Decrease");
                    break;

                case 7:
                    // 플레이어 편집 (이름 또는 경험치 변경)
                    EditPlayerData();
                    WriteLine("Player data sorted by level -> Increase");
                    break;

                case 8:
                    // 통계 및 리포트 출력
                    ShowStatistics();
                    break;

                case 0:
                    // 프로그램 종료
                    exit = false;
                    break;

                default:
                    WriteLine("Invalid menu selection.");
                    break;
            }

            WriteLine();
        }
    }

    /// <summary>
    /// CSV에서 레벨 데이터를 불러와 반환합니다.
    /// CreateLevelCSV.LoadCSV 내부 구현에 의존합니다.
    /// </summary>
    static List<LevelData> LoadLevelData()
    {
        levelDataList = CreateLevelCSV.LoadCSV();
        return levelDataList;
    }

    /// <summary>
    /// 메뉴를 화면에 출력하고 사용자 입력을 정수로 파싱해 반환합니다.
    /// 파싱에 실패하면 0을 반환합니다(Invalid 처리).
    /// </summary>
    /// <returns>선택한 메뉴 번호 (정수)</returns>
    static int DisplayMenu()
    {
        WriteLine("1. Create Player Data");
        WriteLine("2. View Player Data");
        WriteLine("3. Save Player Data");
        WriteLine("4. Delete Player Data");
        WriteLine("5. Sort Player Data By Level -> Increase");
        WriteLine("6. Sort Player Data By Level -> Decrease");
        WriteLine("7. Edit Player Data By Exp");
        WriteLine("8. Show Statistics / Report");
        WriteLine("0. Exit");
        Write("Select Menu: ");

        // 안전한 파싱: TryParse 실패 시 0 리턴
        if( !int.TryParse(ReadLine(), out int menu) )
        {
            WriteLine();
            return 0;
        }

        WriteLine();
        return menu;
    }

    /// <summary>
    /// 사용자로부터 이름과 경험치를 입력받아 PlayerData 객체를 생성합니다.
    /// 주의: 경험치는 현재 int.Parse로 처리되어 FormatException이 발생할 수 있음 -> 필요시 TryParse로 개선 권장
    /// </summary>
    /// <returns>생성된 PlayerData</returns>
    static PlayerData CreatePlayerData()
    {
        Write("Enter player name: ");
        string name = ReadLine() ?? "Unknown";

        Write("Enter player exp : ");
        // 현재 구현은 기본값 "1"을 사용해 숫자 파싱; 입력 검증을 추가하는 것이 안전
        int exp = int.Parse(ReadLine() ?? "1");

        // 경험치로부터 레벨을 계산
        int level = GetLevelFromExp(exp);

        return new PlayerData(name, level, exp);
    }

    /// <summary>
    /// 경험치(exp)를 받아 해당 경험치에 해당하는 레벨을 반환합니다.
    /// 동작 원리:
    /// - levelDataList를 순회하면서 각 레벨의 필요 경험치와 비교합니다.
    /// - exp가 특정 레벨의 필요 경험치보다 작으면 해당 레벨의 -1을 반환(즉 이전 레벨).
    /// 가정:
    /// - levelDataList는 낮은 레벨에서 높은 레벨로 정렬되어 있어야 정확하게 동작합니다.
    /// - 만약 모든 레벨의 필요 경험치보다 크거나 같으면 최종 레벨(=count)을 반환합니다.
    /// </summary>
    /// <param name="exp">플레이어의 누적 경험치</param>
    /// <returns>계산된 레벨 (int)</returns>
    static int GetLevelFromExp(int exp)
    {
        foreach (var levelData in levelDataList)
        {
            if (exp < levelData.Exp)
            {
                // 해당 레벨에 도달하지 못하므로 이전 레벨을 반환
                return levelData.Level - 1;
            }
        }
        // 모든 레벨의 필요 경험치보다 같거나 큰 경우 최대 레벨 수 반환
        return levelDataList.Count;
    }

    /// <summary>
    /// 저장된 플레이어 JSON 파일을 로드하여 반환합니다.
    /// SaveLoadJson.LoadList의 반환값을 그대로 사용합니다.
    /// 예외 처리가 필요할 수 있음(파일 손상, 접근 권한 등) -> SaveLoadJson 내부에서 처리하지 않으면 여기서 래핑 권장
    /// </summary>
    static public List<PlayerData> LoadPlayerData()
    {
        playerDataList = SaveLoadJson_T.LoadList<PlayerData>();
        return playerDataList;
    }

    /// <summary>
    /// 현재 메모리의 플레이어 리스트를 저장합니다.
    /// SaveLoadJson.SaveList 호출.
    /// 파일 입출력 예외 처리가 필요하면 try/catch로 래핑하세요.
    /// </summary>
    static public void SavePlayerData()
    {
        SaveLoadJson_T.SaveList<PlayerData>(playerDataList);
    }

    /// <summary>
    /// 사용자로부터 이름을 입력받아 해당 플레이어를 찾아 삭제합니다.
    /// - ReadLine()이 null일 경우 "Unknown"으로 대체하여 무효 입력을 판별합니다.
    /// - 이름 매칭은 대소문자 구분(현재 Equals 사용) 이므로 필요시 StringComparison.OrdinalIgnoreCase로 변경 고려.
    /// </summary>
    static public void DeletePlayerData()
    {
        Write("Enter player name to delete: ");

        // ?? : null 병합 연산자 사용
        // ReadLine()이 null을 반환할 경우 "Unknown"으로 대체
        string name = ReadLine() ?? "Unknown";

        if(name.Equals("Unknown"))
        {
            WriteLine("Invalid player name.");
            return;
        }

        // 리스트에서 일치하는 이름의 인덱스를 찾음 (첫 번째 항목 삭제)
        int removeIndex = playerDataList.FindIndex(player => player.Name.Equals(name));

        if (removeIndex != -1)
        {
            playerDataList.RemoveAt(removeIndex);
            WriteLine($"Player data for '{name}' deleted.");
        }
        else
        {
            WriteLine($"Player data for '{name}' not found.");
        }
    }

    /// <summary>
    /// 플레이어 리스트를 레벨 기준으로 정렬합니다.
    /// - inCrease == true  : 오름차순 (레벨 낮은 -> 높은)
    /// - inCrease == false : 내림차순 (레벨 높은 -> 낮은)
    /// 설명:
    /// - 내부적으로 List.Sort를 사용하며 비교자에서 CompareTo 결과를 반전시켜 오름/내림을 제어합니다.
    /// - 동일 레벨 내에서는 기존 순서(또는 이름 기준 등)를 추가적으로 지정하려면 ThenBy 등을 사용하세요.
    /// </summary>
    /// <param name="inCrease">오름차순 여부</param>
    static void SortPlayerDataByLevel(bool inCrease = false)
    {
        // Lambda expression to sort playerDataList by Level property
        // p1 and p2 represent two PlayerData objects being compared
        playerDataList.Sort((p1, p2) =>
        {
            if (inCrease)
                return p1.Level.CompareTo(p2.Level);
            else
                return p2.Level.CompareTo(p1.Level);

        });
    }

    /// <summary>
    /// 플레이어를 이름으로 찾아 이름/경험치를 편집합니다.
    /// - 검색은 대소문자 구분 없이 수행됩니다 (StringComparison.OrdinalIgnoreCase 사용).
    /// - 경험치를 변경하면 자동으로 레벨을 재계산합니다.
    /// - 입력 값이 유효하지 않으면 기존 값을 유지합니다.
    /// </summary>
    static void EditPlayerData()
    {
        Write("Enter player name to edit: ");
        string name = ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(name))
        {
            WriteLine("Invalid name.");
            return;
        }

        // 대소문자 구분 없이 검색
        int index = playerDataList.FindIndex(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (index == -1)
        {
            WriteLine($"Player '{name}' not found.");
            return;
        }

        var player = playerDataList[index];
        WriteLine($"Editing: Name: {player.Name}, Level: {player.Level}, Exp: {player.Exp}");

        Write("New name (leave empty to keep): ");
        string newName = ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(newName))
        {
            player.Name = newName;
        }

        Write("New exp (leave empty to keep): ");
        string expInput = ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(expInput))
        {
            if (int.TryParse(expInput, out int newExp) && newExp >= 0)
            {
                // 경험치 변경 시 레벨 재계산
                player.Exp = newExp;
                player.Level = GetLevelFromExp(newExp);
            }
            else
            {
                WriteLine("Invalid exp value. Keeping previous exp.");
            }
        }

        // 변경된 객체로 리스트에 다시 저장
        playerDataList[index] = player;
        WriteLine("Player updated.");
    }

    /// <summary>
    /// 플레이어 데이터에 대한 통계 및 간단한 리포트를 화면에 출력합니다.
    /// 출력 항목:
    /// - 총 플레이어 수, 평균 레벨, 평균 경험치, 중앙값(median)
    /// - 최고/최저 경험치 플레이어(복수 가능)
    /// - 상위 N명(기본 3명) 리스트
    /// - 레벨 분포 (레벨별 인원 수)
    /// </summary>
    static void ShowStatistics()
    {
        if (playerDataList == null || playerDataList.Count == 0)
        {
            WriteLine("No player data available.");
            return;
        }

        int totalPlayers = playerDataList.Count;
        double avgLevel = playerDataList.Average(p => p.Level);
        double avgExp = playerDataList.Average(p => p.Exp);

        int maxExp = playerDataList.Max(p => p.Exp);
        int minExp = playerDataList.Min(p => p.Exp);

        // 최고/최저 경험치 가진 플레이어(복수일 수 있음)
        var topPlayers = playerDataList.Where(p => p.Exp == maxExp).ToList();
        var bottomPlayers = playerDataList.Where(p => p.Exp == minExp).ToList();

        // median exp 계산
        var sortedExps = playerDataList.Select(p => p.Exp).OrderBy(e => e).ToArray();
        double medianExp;
        int mid = sortedExps.Length / 2;
        if (sortedExps.Length % 2 == 0)
            medianExp = (sortedExps[mid - 1] + sortedExps[mid]) / 2.0;
        else
            medianExp = sortedExps[mid];

        // 상위 N (여기서는 최대 3명) — 데이터 수가 적을 경우 그 수만큼 반환
        // OrderByDescending으로 경험치 내림차순 정렬, 동점 시 이름 오름차순 정렬
        // ThenBy 사용. OrderBy 이후에 다시 정렬 기준 추가 가능
        // Take으로 상위 N개 선택
        // ToList로 최종 리스트 생성
        int topN = Math.Min(3, totalPlayers);
        var topNPlayers = playerDataList.OrderByDescending(p => p.Exp).ThenBy(p => p.Name).Take(topN).ToList();

        // 레벨별 분포: Level -> Count
        // GroupBy로 레벨별 그룹화 후 Count 집계
        // OrderBy로 레벨 오름차순 정렬
        // Select로 익명 객체 생성
        // ToList로 최종 리스트 생성
        var distribution = playerDataList
            .GroupBy(p => p.Level)
            .OrderBy(g => g.Key)
            .Select(g => new { Level = g.Key, Count = g.Count() })
            .ToList();

        // 화면 출력 (리포트 형태)
        WriteLine("=== Player Statistics / Report ===");
        WriteLine($"Total players : {totalPlayers}");
        WriteLine($"Average level : {avgLevel:F2}");
        WriteLine($"Average exp   : {avgExp:F2}");
        WriteLine($"Median exp    : {medianExp:F2}");
        WriteLine();

        WriteLine($"Highest exp : {maxExp} (Players: {string.Join(", ", topPlayers.Select(p => p.Name))})");
        WriteLine($"Lowest  exp : {minExp} (Players: {string.Join(", ", bottomPlayers.Select(p => p.Name))})");
        WriteLine();

        WriteLine($"Top {topN} players by Exp:");
        foreach (var p in topNPlayers)
        {
            WriteLine($"- {p.Name} (Level {p.Level}, Exp {p.Exp})");
        }
        WriteLine();

        WriteLine("Level distribution:");
        foreach (var d in distribution)
        {
            WriteLine($"Level {d.Level}: {d.Count} player(s)");
        }

        WriteLine("==================================");
    }
}