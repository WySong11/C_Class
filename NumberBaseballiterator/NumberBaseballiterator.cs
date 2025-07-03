using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NumberBaseballiterator
{
    public void StartGame()
    {
        while (true)
        {
            Console.Clear();
            int digits = PromptDigits();

            List<int> quest = GenerateQuestNumbers(digits);

            Console.WriteLine("\n생성된 숫자: " + string.Join(", ", quest) + "\n");

            // 이 코드는 PlayGame 메서드가 반환한 IEnumerator 객체를 순회합니다.
            // PlayGame 메서드는 yield return 문을 사용한 이터레이터 블록입니다. 
            // 즉, IEnumerator를 구현하는 상태 머신을 만들어 냅니다.
            // MoveNext()가 호출될 때마다:
            // 1. 다음 yield return 문까지 실행하고,
            // 2. 반환값은 game.Current에 들어감
            // 3. MoveNext()가 false를 반환하면 반복문 종료
            // 4. game.Current는 마지막 yield return의 값을 반환
            // 5. 이터레이터가 끝나면 PlayGame 메서드가 종료됨
            // yield return $"축하합니다! 정답을 맞추셨습니다. 총 시도 횟수: {count}";
            // 이 줄을 마지막으로 더 이상 yield return이 없기 때문에,
            // 그 다음 MoveNext() 호출 시 false를 반환합니다.

            IEnumerator game = PlayGame(digits, quest);
            while (game.MoveNext())
            {
                Console.WriteLine(game.Current);
            }

            string playerName = PromptPlayerName();

            if (!PromptPlayAgain())
            {
                Console.WriteLine("게임을 종료합니다.");
                break;
            }
        }
    }

    private int PromptDigits()
    {
        while (true)
        {
            Console.WriteLine("숫자 야구 자리수 입력하세요 (1~10): ");
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int digits) && digits >= 1 && digits <= 10)
            {
                Console.WriteLine($"\n게임 시작! 자리수: {digits}");
                return digits;
            }
            Console.WriteLine("잘못된 입력입니다. 1에서 10 사이의 숫자를 입력하세요.");
        }
    }

    private List<int> GenerateQuestNumbers(int digits)
    {
        List<int> quest = new();
        while (quest.Count < digits)
        {
            quest.Add(GetQuestNumber(quest));
        }
        return quest;
    }

    private string PromptPlayerName()
    {
        Console.WriteLine("\n이름을 입력하세요 : ");
        string? playerName = Console.ReadLine();
        playerName = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName;
        return playerName;
    }

    private bool PromptPlayAgain()
    {
        Console.WriteLine("\n다시 하시겠습니까? (Y/N)");
        string? playAgain = Console.ReadLine()?.Trim().ToUpper();
        return playAgain == "Y";
    }

    public int GetQuestNumber(List<int> InQuest)
    {
        Random random = new();
        int number;
        do
        {
            number = random.Next(0, 10);
        } while (InQuest.Contains(number));
        return number;
    }

    public IEnumerator PlayGame(int InDigits, List<int> InQuest)
    {
        int count = 0;
        int strike = 0;

        while (strike < InDigits)
        {
            Console.WriteLine("숫자를 입력하세요: ");
            string? input = Console.ReadLine();

            if (input == null || input.Length != InDigits || !input.All(char.IsDigit))
            {
                yield return $"잘못된 입력입니다. {InDigits}자리 숫자를 입력하세요.\n";
                continue;
            }

            strike = 0;
            int ball = 0;

            for (int i = 0; i < InDigits; i++)
            {
                int userNumber = int.Parse(input[i].ToString());
                if (userNumber == InQuest[i])
                {
                    strike++;
                }
                else if (InQuest.Contains(userNumber))
                {
                    ball++;
                }
            }

            count++;
            yield return $"{strike} 스트라이크, {ball} 볼\n";
        }

        yield return $"축하합니다! 정답을 맞추셨습니다. 총 시도 횟수: {count}";
    }
}


/*IEnumerator TeamDataLoad(int teamIndex, string[] files)
{
    List<TeamData> datas = new List<TeamData>();
    for (int i = 0; i < files.Length; ++i)
    {
        yield return null;
        string[] path = files[i].Split('/');
        string fileName = path[path.Length - 1];
        if (fileName.EndsWith(".meta") == true) continue;
        string json;
        bool success = MUtils.Load("SetIn/", fileName, out json);
        TeamData data = Load(json);
        if (data.m_teamIndex == teamIndex) datas.Add(Load(json));
    }
    yield return null;

    if (datas.Count == 0)
    {
        DialogWindow.m_instance.Show("popup_title_notice".Localized("알림"), "데이터가 없습니다.", LanguageManager.m_instance.GetText("common_yes"), null);
        yield break;
    }


    List<int> indexs = new List<int>();
    List<string> displays = new List<string>();

    for (int i = 0; i < datas.Count; ++i)
    {
        TeamData data = datas[i];
        displays.Add($"{data.m_teamIndex}팀 {data.m_datas.Count}개 배치 ({DateTime.Parse(data.m_saveTime)})");
        indexs.Add(i);
    }

    SelectListWindow.m_instance.SetData("리플레이", indexs, displays, (index) =>
    {
        SelectListWindow.m_instance.SetVisible(false);

        TeamData data = datas[index];
        LoadTeamData(data);

    }, null);
}

private TeamData Load(string json)
{
    return JsonUtility.FromJson<TeamData>(json);
}*/

/*IEnumerator AutoSetInGame(GameData gameData)
{
    TeamData team_0 = gameData.m_team_0;
    TeamData team_1 = gameData.m_team_1;

    LoadingPanel.SetVisible(true);

    yield return null;

    NextState();

    yield return null;

    SetInCharacterPanel setInPanel = FindObjectOfType<SetInCharacterPanel>();
    for (int i = 0; i < team_0.m_datas.Count; ++i)
    {
        CharacterSetInData data = team_0.m_datas[i];
        setInPanel.SetIn(data.m_id, data.m_center_position);
        //yield return null;
    }
    NextState();
    yield return null;

    for (int i = 0; i < team_1.m_datas.Count; ++i)
    {
        CharacterSetInData data = team_1.m_datas[i];
        setInPanel.SetIn(data.m_id, data.m_center_position);
        //yield return null;
    }
    NextState();
    yield return null;
    NextState();

    LoadingPanel.SetVisible(false);
}

IEnumerator DeleteGameFromFile(string[] files)
{
    List<GameData> datas = new List<GameData>();
    for (int i = 0; i < files.Length; ++i)
    {
        string[] path = files[i].Split('/');
        string fileName = path[path.Length - 1];
        if (fileName.EndsWith(".meta") == true) continue;
        string json;
        bool success = MUtils.Load("Game/", fileName, out json);
        GameData data = JsonUtility.FromJson<GameData>(json);
        datas.Add(data);
    }

    if (datas.Count == 0)
    {
        DialogWindow.m_instance.Show("popup_title_notice".Localized("알림"), "데이터가 없습니다.", LanguageManager.m_instance.GetText("common_yes"), null);
        yield break;
    }

    List<int> indexs = new List<int>();
    List<string> displays = new List<string>();

    for (int i = 0; i < datas.Count; ++i)
    {
        GameData data = datas[i];
        displays.Add($"{data.m_fileName}");
        indexs.Add(i);
    }


    SelectListWindow.m_instance.SetData("게임 삭제", indexs, displays, (index) =>
    {
        SelectListWindow.m_instance.SetVisible(false);
        MUtils.Delete(files[index]);
        DeleteGame();
    }, null);
}

IEnumerator LoadGameFromFile(string[] files)
{
    List<GameData> datas = new List<GameData>();
    for (int i = 0; i < files.Length; ++i)
    {
        yield return null;
        string[] path = files[i].Split('/');
        string fileName = path[path.Length - 1];
        if (fileName.EndsWith(".meta") == true) continue;
        string json;
        bool success = MUtils.Load("Game/", fileName, out json);
        GameData data = JsonUtility.FromJson<GameData>(json);
        datas.Add(data);
    }

    if (datas.Count == 0)
    {
        DialogWindow.m_instance.Show("popup_title_notice".Localized("알림"), "데이터가 없습니다.", LanguageManager.m_instance.GetText("common_yes"), null);
        yield break;
    }

    List<int> indexs = new List<int>();
    List<string> displays = new List<string>();

    for (int i = 0; i < datas.Count; ++i)
    {
        GameData data = datas[i];
        displays.Add($"{data.m_fileName}");
        indexs.Add(i);
    }

    SelectListWindow.m_instance.SetData("게임 불러오기", indexs, displays, (index) =>
    {
        SelectListWindow.m_instance.SetVisible(false);

        GameData data = datas[index];
        m_autoSetInAction?.Invoke(data);
    }, null);
}*/