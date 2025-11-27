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

        // 문제
        // Data/level.csv 파일에 levels 리스트의 내용을 작성하는 코드를 작성하세요.
        // Level,Exp 형식으로 작성되어야 합니다.

        // 이미 파일이 존재하면 생성하지 않음
        if (File.Exists(fullPath))
        {
            return;
        }

        // 디렉토리가 없으면 생성
        if (!Directory.Exists(CSV_DIR_PATH))
        {
            Directory.CreateDirectory(CSV_DIR_PATH);
        }

        try 
        {
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                // 헤더 작성
                writer.WriteLine("Level,Exp");

                // 데이터 작성
                foreach (var level in levels)
                {
                    // LevelData의 Level과 Exp를 콤마로 구분하여 작성
                    // csv 파일의 각 줄은 "Level,Exp" 형식이어야 함
                    // 예: 1,0
                    writer.WriteLine($"{level.Level},{level.Exp}");
                }
            }
        }
        // File IOException 처리
        catch (IOException e)
        {
            System.Console.WriteLine("An error occurred while creating the CSV file: " + e.Message);
        }
        // 기타 예외 처리
        catch (System.Exception e)
        {
            System.Console.WriteLine("An unexpected error occurred: " + e.Message);
        }
    }

    public static List<LevelData> LoadCSV()
    {
        List<LevelData> levels = new List<LevelData>();

        // 문제
        // Data/level.csv 파일을 읽어서 LevelData 리스트로 반환하는 코드를 작성하세요.

        string fullPath = System.IO.Path.Combine(CSV_DIR_PATH, CSV_FILE_NAME);

        if(File.Exists(fullPath) == false)
        {
            return levels;
        }

        try 
        {
            using (StreamReader reader = new StreamReader(fullPath))
            {
                // 첫 번째 줄(헤더) 건너뛰기 ( level,exp )
                string headerLine = reader.ReadLine();

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    // 각 줄을 콤마로 분리
                    // string.Split 사용 (',')
                    // Level과 Exp를 파싱하여 LevelData 객체 생성 후 리스트에 추가
                    string[] parts = line.Split(',');

                    // parts[0] -> Level
                    // parts[1] -> Exp
                    if (parts.Length == 2 &&
                        int.TryParse(parts[0], out int level) &&
                        int.TryParse(parts[1], out int exp))
                    {
                        // LevelData 객체 생성 및 리스트에 추가
                        levels.Add(new LevelData(level, exp));
                    }
                }
            }
        }
        catch (IOException e)
        {
            System.Console.WriteLine("An error occurred while reading the CSV file: " + e.Message);
        }
        catch (System.Exception e)
        {
            System.Console.WriteLine("An unexpected error occurred: " + e.Message);
        }

        return levels;
    }
}