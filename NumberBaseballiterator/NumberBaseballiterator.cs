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
        Console.Clear();
        Console.WriteLine("숫자 야구 자리수 입력하세요 (1~10): ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int digits) && digits >= 1 && digits <= 10)
        {
            Console.WriteLine($"게임 시작! 자리수: {digits}");
            // 게임 로직을 여기에 추가하세요.
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 1에서 10 사이의 숫자를 입력하세요.");
        }
        Console.WriteLine();

        // 자리수 입력
        int PlayCount = PlayGame(digits);


        Console.WriteLine("\n이름을 입력하세요 : ");
        string? PlayerName = Console.ReadLine();

        // Null 체크 추가
        PlayerName = string.IsNullOrWhiteSpace(PlayerName) ? "Player" : PlayerName;


        Console.WriteLine("\n다시 하시겠습니까? (Y/N)");
        string? playAgain = Console.ReadLine()?.ToUpper();
        if (playAgain == "Y")
        {
            StartGame();
        }
        else
        {
            Console.WriteLine("게임을 종료합니다.");
        }
    }

    public int PlayGame(int InDigits)
    {
        List<int> Quest = new();

        while (Quest.Count < InDigits)
        {
            Quest.Add(GetQuestNumber(Quest));
        }

        Console.WriteLine("생성된 숫자: " + string.Join(", ", Quest));
        Console.WriteLine();

        return InputAnswer(InDigits, Quest);
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

    public int InputAnswer(int InDigits, List<int> InQuest)
    {
        int count = 0;
        int strike = 0;
        int ball = 0;
        while (strike < InDigits)
        {
            Console.WriteLine("숫자를 입력하세요: ");
            string? input = Console.ReadLine();

            // 입력이 null이거나 길이가 InDigits가 아니거나 숫자가 아닌 경우
            if (input == null || input.Length != InDigits || !input.All(char.IsDigit))
            {
                Console.WriteLine($"잘못된 입력입니다. {InDigits}자리 숫자를 입력하세요.");
                Console.WriteLine();
                continue;
            }
            Console.WriteLine();

            strike = 0;
            ball = 0;
            for (int i = 0; i < InDigits; i++)
            {
                // 입력된 숫자를 정수로 변환하고 비교
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
            Console.WriteLine($"{strike} 스트라이크, {ball} 볼");
            Console.WriteLine();

            count++;
        }
        Console.WriteLine("축하합니다! 정답을 맞추셨습니다.");
        return count;
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