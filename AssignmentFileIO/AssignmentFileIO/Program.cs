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
                    DeletePlayerData();
                    WriteLine("Player data deleted");
                    break;

                case 5:
                    SortPlayerDataByLevel();
                    WriteLine("Player data sorted by level.");
                    break;

                case 6:
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
        levelDataList = CreateLevelCSV.LoadCSV();
        return levelDataList;
    }

    static int DisplayMenu()
    {
        WriteLine("1. Create Player Data");
        WriteLine("2. View Player Data");
        WriteLine("3. Save Player Data");
        WriteLine("4. Delete Player Data");
        WriteLine("5. Sort Player Data By Level");
        WriteLine("6. Exit");
        Write("Select Menu: ");

        if( !int.TryParse(ReadLine(), out int menu) )
        {
            WriteLine();
            return 0;
        }
        
        WriteLine();
        return menu;
    }

    static PlayerData CreatePlayerData()
    {
        Write("Enter player name: ");
        string name = ReadLine() ?? "Unknown";
        Write("Enter player exp : ");
        int exp = int.Parse(ReadLine() ?? "1");
        
        int level = GetLevelFromExp(exp);

        return new PlayerData(name, level, exp);
    }

    static int GetLevelFromExp(int exp)
    {
        foreach (var levelData in levelDataList)
        {
            if (exp < levelData.Exp)
            {
                return levelData.Level - 1;
            }
        }
        return levelDataList.Count;
    }

    static public List<PlayerData> LoadPlayerData()
    {
        playerDataList = SaveLoadJson.LoadList<PlayerData>();
        return playerDataList;
    }

    static public void SavePlayerData()
    {
        SaveLoadJson.SaveList<PlayerData>(playerDataList);
    }

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

    static void EditPlayerData()
    {
        Write("Enter player name to edit: ");
        string name = ReadLine() ?? "";
        if (string.IsNullOrWhiteSpace(name))
        {
            WriteLine("Invalid name.");
            return;
        }

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
                player.Exp = newExp;
                player.Level = GetLevelFromExp(newExp);
            }
            else
            {
                WriteLine("Invalid exp value. Keeping previous exp.");
            }
        }

        playerDataList[index] = player;
        WriteLine("Player updated.");
    }
}