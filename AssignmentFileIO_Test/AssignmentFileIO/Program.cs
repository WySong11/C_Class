using System;
using System.Collections.Generic;
using static System.Console;

public class Program
{
    static public List<LevelData> levelDataList = new List<LevelData>();
    static public List<PlayerData> playerDataList = new List<PlayerData>();

    static void Main(string[] args)
    {
        CreateLevelCSV.CreateCSV();

        bool exit = true;

        playerDataList = LoadPlayerData();
        
        levelDataList = LoadLevelData();
        //levelDataList = CreateLevelCSV.LoadCSV();

        while (exit)
        {
            WriteLine();

            switch( DisplayMenu() )
            {
                case 1:
                    PlayerData newPlayer = CreatePlayerData();
                    playerDataList.Add(newPlayer);
                    WriteLine("Player data created.");                    
                    break;

                case 2:

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
                    SavePlayerData();
                    WriteLine("Player data saved.");                    
                    break;

                case 4:
                    DeleterPlayerData();
                    WriteLine("Player data deleted.");
                    break;

                case 5:
                    SortPlayerDataByLevel(true);
                    WriteLine("Player data sorted in increasing order by level.");
                    break;

                case 6:
                    SortPlayerDataByLevel(false);
                    WriteLine("Player data sorted in decreasing order by level.");
                    break;

                case 0:
                    exit = false;
                    break;

                default:                    
                    WriteLine("Invalid menu selection.");
                    break;
            }

            WriteLine();
        }
    }

    static List<LevelData> LoadLevelData()
    {
        // 문제
        // CreateLevelCSV 를 사용하여 levelDataList 를 불러옴

        return CreateLevelCSV.LoadCSV();
    }

    static int DisplayMenu()
    {
        WriteLine("1. Create Player Data");
        WriteLine("2. View Player Data");
        WriteLine("3. Save Player Data");
        WriteLine("4. Deleat Player Data");
        WriteLine("5. Sort InCrease Player Data");
        WriteLine("6. Sort Decrease Player Data");
        WriteLine("0. Exit");
        Write("Select Menu: ");
        int menu = int.Parse(ReadLine() ?? "4");
        WriteLine();
        return menu;
    }

    static PlayerData CreatePlayerData()
    {
        Write("Enter player name: ");
        string name = ReadLine() ?? "Unknown";
        Write("Enter player exp : ");
        int exp = int.Parse(ReadLine() ?? "1");

        // levelDataList 를 참고하여 exp 에 해당하는 level 을 찾아야 함
        int level = GetLevelFromExp(exp);

        return new PlayerData(name, level, exp);
    }

    static int GetLevelFromExp(int exp)
    {
        // 문제
        // levelDataList 에서 exp 에 해당하는 level 을 찾아 반환
        foreach (var levelData in levelDataList)
        {
            // exp 가 levelData.Exp 보다 작으면 이전 레벨을 반환
            // 11 -> Level 1 , Exp 0
            // 11 -> Level 2 , Exp 100
            // level 1을 리턴
            // 150 -> Level 2 , Exp 100
            // 150 -> Level 3 , Exp 300
            // level 2를 리턴
            if (exp < levelData.Exp)
            {
                return levelData.Level - 1;
            }
        }

        // 모든 레벨의 Exp 보다 크면 최대 레벨 반환       
        return levelDataList[levelDataList.Count-1].Level;

        // C# 8.0 이상에서는 아래와 같이 쓸 수도 있음
        //return levelDataList[^1].Level;
    }

    static public List<PlayerData> LoadPlayerData()
    {
        // 문제
        // SaveLoadJson 를 사용하여 playerDataList 를 불러옴
        playerDataList = SaveLoadJson.LoadList();
        return playerDataList;
    }

    static public void SavePlayerData()
    {
        // 문제
        // SaveLoadJson 를 사용하여 playerDataList 를 저장        
        SaveLoadJson.SaveList(playerDataList);
    }

    static public void DeleterPlayerData()
    {
        Write("Enter player name to delete: ");
        string name = ReadLine() ?? "Unknown";

        // String.Equals 를 사용하여 name 이 "Unknown" 인지 비교
        if (name.Equals("Unknown"))
        {
            WriteLine("Invalid name.");
            return;
        }

        // Name.Equals 를 사용하여 playerDataList 에서 해당 이름의 플레이어 데이터를 찾아 삭제
        int removeIndex = playerDataList.FindIndex(player => player.Name.Equals(name));

        // 찾았으면 삭제, 못찾았으면 메시지 출력
        // removeIndex 가 -1 이 아니면 삭제
        // -1 이면 못찾음
        if ( removeIndex != -1)
        {
            playerDataList.RemoveAt(removeIndex);
            WriteLine($"Player data for '{name}' deleted.");
        }
        else
        {
            WriteLine($"Player data for '{name}' not found.");
        }
    }

    static void SortPlayerDataByLevel(bool inCrease = true)
    {        
        if (inCrease)
        {
            // 오름차순 CompareTo 사용
            // a.Level 이 b.Level 보다 작으면 음수 반환
            // a.Level 이 b.Level 보다 크면 양수 반환, 같으면 0 반환
            playerDataList.Sort((a, b) => a.Level.CompareTo(b.Level));
        }
        else
        {
            // 내림차순 CompareTo 사용
            // a.Level 이 b.Level 보다 크면 음수 반환
            // a.Level 이 b.Level 보다 작으면 양수 반환, 같으면 0 반환
            playerDataList.Sort((a, b) => b.Level.CompareTo(a.Level));
        }
    }
}