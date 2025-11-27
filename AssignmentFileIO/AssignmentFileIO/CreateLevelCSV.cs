using System.Collections.Generic;
using System.IO;

/// <summary>
/// 레벨 관련 CSV 파일을 생성하고 로드하는 유틸리티 클래스입니다.
/// - 기본 디렉터리: "Data"
/// - 기본 파일명: "levels.csv"
/// - CSV 형식(헤더 포함): "Level,Exp"
/// </summary>
public static class CreateLevelCSV
{
    /// <summary>
    /// CSV를 저장할 기본 디렉터리 경로
    /// 필요 시 외부에서 변경 가능합니다(예: 테스트용 경로).
    /// </summary>
    public static string CSV_DIR_PATH = "Data";

    /// <summary>
    /// 기본 CSV 파일명 (레거시 호환용).
    /// 제네릭 CSV 유틸을 도입할 경우 타입 기반 파일명으로 확장할 수 있습니다.
    /// </summary>
    public static string CSV_FILE_NAME = "levels.csv";

    /// <summary>
    /// 기본 레벨 표본을 만들어 CSV 파일로 저장합니다.
    /// - 파일이 이미 존재하면 덮어쓰지 않고 생성을 건너뜁니다.
    /// - 디렉터리가 없으면 생성합니다.
    /// - 출력되는 CSV는 UTF-8 기본 인코딩(StreamWriter 기본)으로 작성됩니다.
    /// </summary>
    public static void CreateCSV()
    {
        // 기본 레벨 데이터 샘플 (레벨 번호와 해당 레벨에 도달하기 위한 누적 경험치)
        List<LevelData> levels = new List<LevelData>
        {
            new LevelData(1, 0),
            new LevelData(2, 100),
            new LevelData(3, 300),
            new LevelData(4, 600),
            new LevelData(5, 1000),
            new LevelData(6, 1500),
            new LevelData(7, 3000),
            new LevelData(8, 5000),
            new LevelData(9, 8000),
            new LevelData(10, 10000)
        };

        // 디렉터리 경로와 파일명을 결합하여 전체 경로를 구성
        string fullPath = Path.Combine(CSV_DIR_PATH, CSV_FILE_NAME);

        // 파일이 이미 있으면 새로 생성하지 않고 스킵.
        // 의도: 기존 레벨 설정을 보존하고 실수로 덮어쓰는 상황 방지.
        if (File.Exists(fullPath))
        {
            System.Console.WriteLine("File already exists. Skipping creation.");
            System.Console.WriteLine();
            return;
        }

        // 저장 디렉터리가 없으면 생성
        if (Directory.Exists(CSV_DIR_PATH) == false)
        {
            Directory.CreateDirectory(CSV_DIR_PATH);
        }

        // StreamWriter를 이용해 CSV 파일을 생성
        // 첫 줄은 헤더("Level,Exp"), 이후 각 LevelData를 "레벨,경험치" 형태로 기록
        using (var writer = new StreamWriter(fullPath))
        {
            // 헤더 작성: CSV 소비자(파서 등)가 컬럼 순서를 알 수 있도록 함
            writer.WriteLine("Level,Exp");

            // 각 레벨 항목을 "Level,Exp" 형태로 한 줄씩 작성
            foreach (var level in levels)
            {
                // 단순 숫자만 기록하므로 별도의 이스케이프 처리 불필요
                writer.WriteLine($"{level.Level},{level.Exp}");
            }
        }
    }

    /// <summary>
    /// CSV 파일을 읽어 <see cref="LevelData"/> 리스트로 변환하여 반환합니다.
    /// - 헤더("Level,Exp")는 첫 줄로 가정하고 건너뜁니다.
    /// - 각 데이터 라인은 쉼표로 분리되며, 유효한 정수 파싱에 실패하는 라인은 무시됩니다.
    /// - 파일이 없으면 빈 리스트를 반환합니다.
    /// </summary>
    /// <returns>읽어온 <see cref="LevelData"/> 리스트 (파일이 없거나 파싱 실패 시 빈 리스트)</returns>
    public static List<LevelData> LoadCSV()
    {
        List<LevelData> levels = new List<LevelData>();

        // 파일 전체 경로 구성
        string fullPath = Path.Combine(CSV_DIR_PATH, CSV_FILE_NAME);

        // 파일이 존재하지 않으면 빈 리스트 반환
        if (File.Exists(fullPath) == false)
        {
            return levels;
        }

        // 파일을 열어 한 줄씩 읽기
        using (var reader = new StreamReader(fullPath))
        {
            string? line;
            bool isFirstLine = true; // 헤더를 건너뛰기 위한 플래그

            while ((line = reader.ReadLine()) != null)
            {
                // 첫 줄(헤더)은 건너뛰기
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }

                // 간단한 CSV 파싱: 쉼표(,)로 분리
                // 주의: 현재 구현은 필드 내 쉼표나 따옴표를 지원하지 않음(숫자 전용이므로 문제 없음)
                var parts = line.Split(',');

                // 유효한 라인은 두 개의 컬럼(Level, Exp)을 가져야 하며 정수로 파싱 가능해야 함
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int level) &&
                    int.TryParse(parts[1], out int exp))
                {
                    levels.Add(new LevelData(level, exp));
                }
                else
                {
                    // 파싱 실패한 라인은 무시(로깅이 필요하면 여기서 기록 추가 권장)
                    // 예: System.Console.WriteLine($"Skipped invalid line: {line}");
                }
            }
        }

        return levels;
    }
}