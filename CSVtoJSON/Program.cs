using System;
using static System.Console;

public class Program
{
    static void Main(string[] args)
    {
        // CSV 파일에서 게임 데이터를 로드합니다.  
        GameDataManager.Instance.LoadGameDataListFromCSV();

        // JSON 파일에서 저장된 게임 데이터를 로드합니다.  
        GameDataManager.Instance.LoadSaveDataListFromJSON();

        GameDataManager.Instance.PrintGameDataList();

        InputPlayerData();
    }

    public static void InputPlayerData()
    {
        // 플레이어 이름과 레벨을 입력받습니다.
        string playerName = PromptPlayerName();
        int playerLevel = PromptPlayerLevel();
        // 플레이어 데이터를 생성합니다.
        var playerData = new SaveData(playerName, playerLevel);
        // 게임 데이터 매니저에 플레이어 데이터를 추가합니다.
        GameDataManager.Instance.AddSaveData(playerData);

        GameDataManager.Instance.PrintGameDataList();

        if( PromptYesNo("플레이어를 계속 생성하시겠습니까?"))
        {
            InputPlayerData();
        }
        else
        {
            // 플레이어 데이터를 JSON 파일에 저장합니다.
            GameDataManager.Instance.SaveSaveDataListToJSON();
            WriteLine("플레이어 데이터가 저장되었습니다.");
        }        
    }

    private static string PromptPlayerName()
    {
        WriteLine("\n이름을 입력하세요 : ");
        string? playerName = Console.ReadLine();
        playerName = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName;
        return playerName;
    }

    private static int PromptPlayerLevel()
    {
        WriteLine("\n레벨을 입력하세요 : ");
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int level) && level > 0)
        {
            return level;
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 기본 레벨 1로 설정합니다.");
            return 1;
        }
    }

    private static bool PromptYesNo(string message)
    {
        WriteLine(message + " (y/n): ");
        string? input = Console.ReadLine()?.Trim().ToLower();
        return input == "y" || input == "yes";
    }
}
