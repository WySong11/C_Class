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

    static void SortPlayerDataByLevel()
    {
        playerDataList.Sort((p1, p2) => p1.Level.CompareTo(p2.Level));
    }
}