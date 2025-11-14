using System.Collections.Generic;
using System.IO;

public static class CreateLevelCSV
{
    public static string CSV_DIR_PATH = "Data";
    public static string CSV_FILE_NAME = "levels.csv";

    public static void CreateCSV()
    {
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

        string fullPath = System.IO.Path.Combine(CSV_DIR_PATH, CSV_FILE_NAME);

        if(File.Exists(fullPath))
        {
            System.Console.WriteLine("File already exists. Skipping creation.");
            System.Console.WriteLine();
            return;
        }

        if(Directory.Exists(CSV_DIR_PATH) == false)
        {
            Directory.CreateDirectory(CSV_DIR_PATH);
        }

        using (var writer = new StreamWriter(fullPath))
        {
            writer.WriteLine("Level,Exp");
            foreach (var level in levels)
            {
                writer.WriteLine($"{level.Level},{level.Exp}");
            }
        }
    }

    public static List<LevelData> LoadCSV()
    {
        List<LevelData> levels = new List<LevelData>();
        string fullPath = System.IO.Path.Combine(CSV_DIR_PATH, CSV_FILE_NAME);
        if (File.Exists(fullPath) == false)
        {
            return levels;
        }
        using (var reader = new StreamReader(fullPath))
        {
            string? line;
            bool isFirstLine = true;
            while ((line = reader.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    continue;
                }
                var parts = line.Split(',');
                if (parts.Length == 2 &&
                    int.TryParse(parts[0], out int level) &&
                    int.TryParse(parts[1], out int exp))
                {
                    levels.Add(new LevelData(level, exp));
                }
            }
        }
        return levels;
    }
}