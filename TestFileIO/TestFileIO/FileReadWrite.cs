using System;
using System.IO;


public class FileReadWrite
{
    // 저장 디렉터리 및 파일 이름
    const string DirectoryPath = "Save";
    const string fileName = "SaveFile.txt";

    public void StartFileReadWrite()
    {
        bool isContinue = true;

        while (isContinue)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Read File with Existence Check");
            Console.WriteLine("2. Write File with Existence Check");
            Console.WriteLine("3. Exit");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ReadFileWithExistenceCheck();
                    break;
                case "2":
                    WriteFileWithExistenceCheck();
                    break;
                case "3":
                    isContinue = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }            
        }
    }

    public void ReadFileWithExistenceCheck()
    {
        // 파일 경로 설정. 디렉토리와 파일 이름 결합
        string filePath = Path.Combine(DirectoryPath, fileName);

        Console.WriteLine();

        // 존재하지 않는 파일 읽기 시도
        if (File.Exists(filePath))
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;
                    // 한 줄씩 읽기
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            // IOException 처리 : 파일 읽기 중 발생하는 일반적인 예외 처리
            catch (IOException ex)
            {
                Console.WriteLine("An I/O error occurred: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine("File does not exist.");
        }

        Console.WriteLine();
    }

    public void WriteFileWithExistenceCheck()
    {
        // 파일 경로 설정. 디렉토리와 파일 이름 결합
        string filePath = Path.Combine(DirectoryPath, fileName);

        Console.WriteLine();

        // 사용자로부터 입력 받기
        Console.WriteLine("Input Text : ");
        string? inputText = Console.ReadLine() ?? string.Empty;

        try
        {
            if (Directory.Exists(DirectoryPath) == false)
            {
                // 디렉터리 없으면 생성
                Directory.CreateDirectory(DirectoryPath);
            }

            // 파일이 존재하면 append 모드로, 없으면 생성 후 작성
            // StreamWriter의 두 번째 매개변수에 true를 전달하여 append 모드로 설정
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(inputText);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("An I/O error occurred: " + ex.Message);
        }
    }
}
