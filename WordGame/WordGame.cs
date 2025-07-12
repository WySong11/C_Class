using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WordGame
{
    public void Initialize()
    {
        // 게임 데이터 리스트를 CSV 파일에서 로드합니다.
        GameDataManager.Instance.LoadGameDataListFromCSV();

        // JSON 파일에서 저장된 게임 데이터를 로드합니다.
        GameDataManager.Instance.LoadSaveDataListFromJSON();

        // 게임 데이터 리스트를 출력합니다.
        GameDataManager.Instance.PrintGameDataList();
    }

    public void StartGame()
    {
        GameData gamedata = GameDataManager.Instance.GetRandomAnswer();
        string? Answer = gamedata.Word;
        int HindCount = 0;
        int HP = GameDataManager.Instance.GetMaxHP();

        ShowHint(HindCount++, gamedata);

        while (HP > 0)
        {
            // 단어를 입력받습니다.
            string word = PromptWord();

            if (string.IsNullOrWhiteSpace(word))
            {
                Console.WriteLine("단어가 비어있습니다. 다시 입력해주세요.");
                continue;
            }

            /*
            •	CurrentCulture
            현재 스레드의 문화권을 기준으로, 문화권에 민감하게 문자열을 비교합니다. (대소문자 구분)
            •	CurrentCultureIgnoreCase
            현재 문화권을 기준으로, 대소문자를 무시하고 비교합니다.
            •	InvariantCulture
            고정 문화권(변하지 않는 문화권, 주로 시스템 / 프로그램 내부 처리용)을 기준으로 비교합니다. (대소문자 구분)
            •	InvariantCultureIgnoreCase
            고정 문화권을 기준으로, 대소문자를 무시하고 비교합니다.
            •	Ordinal
            유니코드 값(이진 값)으로 직접 비교합니다. (문화권 무시, 대소문자 구분)
            •	OrdinalIgnoreCase
            유니코드 값으로 비교하되, 대소문자를 무시합니다.
            */

            // 힌트라는 단어가 입력되면 힌트 기능을 실행합니다.
            if (word.Equals("힌트", StringComparison.OrdinalIgnoreCase))
            {
                ShowHint(HindCount++, gamedata);
                HP -= 2;
                continue;
            }

            if (word.Equals(Answer, StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            HP--;
        }

        if (HP <= 0)
        {
            Console.WriteLine($"\n게임 오버! 정답은 {Answer}였습니다.\n");
        }
        else
        {
            Console.WriteLine($"\n정답입니다! {Answer}를 맞추셨습니다.\n");

            // 플레이어 이름을 입력받습니다.
            string playerName = PromptPlayerName();

            // 게임을 종료하고 결과를 저장합니다.
            SaveData saveData = new SaveData(playerName, GameDataManager.Instance.GetMaxHP() - HP, Answer);
            GameDataManager.Instance.AddSaveData(saveData);
        }

        Console.WriteLine("\n게임을 계속하시겠습니까? (y/n)");
        string? continueGame = Console.ReadLine();
        if (continueGame?.ToLower() == "y")
        {
            // 게임을 다시 시작합니다.
            StartGame();
        }
    }

    private string PromptPlayerName()
    {
        Console.WriteLine("\n이름을 입력하세요 : ");
        string? playerName = Console.ReadLine();
        playerName = string.IsNullOrWhiteSpace(playerName) ? "Player" : playerName;
        return playerName;
    }

    private string PromptWord()
    {
        Console.WriteLine("\n단어를 입력하세요 : ");
        string? word = Console.ReadLine();
        return string.IsNullOrWhiteSpace(word) ? "default" : word;
    }

    private void ShowHint(int hintCount, GameData gameData)
    {
        if (gameData.Hints == null )
        {
            Console.WriteLine("\n힌트가 없습니다.\n");
            return;
        }

        if ( hintCount >= gameData.Hints.Count)
        {
            Console.WriteLine("\n모든 힌트를 사용했습니다.\n");
            return;
        }

        Console.WriteLine($"\n힌트 {hintCount + 1}: {gameData.Hints[hintCount]}\n");
    }
}