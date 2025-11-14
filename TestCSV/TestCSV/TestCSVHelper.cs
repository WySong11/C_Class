using System;
using System.IO;
using System.Collections.Generic;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Location { get; set; }

    // Default constructor
    public Person() { }

    public Person(string name, int age, string location)
    {
        Name = name;
        Age = age;
        Location = location;
    }
}

public class CSVAnswer
{
    const string DirPath = "CSV";
    const string FileName = "Person.csv";

    public void Run()
    {
        bool isContinue = true;
        string fullPath = Path.Combine(DirPath, FileName);

        while (isContinue)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Read CSV File");
            Console.WriteLine("2. Write CSV File");
            Console.WriteLine("3. Exit");
            string? input = Console.ReadLine();            

            switch (input)
            {
                case "1":
                    ReadCSV(fullPath);
                    break;
                case "2":
                    WriteCSV(fullPath);
                    break;
                case "3":
                    isContinue = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    public void ReadCSV(string fullPath)
    {
        Console.WriteLine();

        if(File.Exists(fullPath) == false)
        {
            Console.WriteLine($"File not found: {fullPath}");
            return;
        }

        using ( var reader = new StreamReader(fullPath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                // Split : ,
                var values = line.Split(',');

                // 출력
                Console.WriteLine($"Name: {values[0]}, Age: {values[1]}, Location: {values[2]}");
            }
        }

        Console.WriteLine();
    }

    public void WriteCSV(string fullPath)
    {
        Console.WriteLine();

        // 이름 입력
        Console.Write("Enter Name: ");
        string name = Console.ReadLine() ?? "";

        // 나이 입력
        Console.Write("Enter Age: ");
        int age = int.Parse(Console.ReadLine() ?? "0");

        // 지역 입력
        Console.Write("Enter Location: ");
        string location = Console.ReadLine() ?? "";

        // Person 객체 생성
        Person newPerson = new Person(name, age, location);

        // 디렉토리 검사
        if(Directory.Exists(DirPath) == false)
        {
            // 디렉토리 생성
            Directory.CreateDirectory(DirPath);
        }

        // CSV 파일에 추가
        // Append 모드로 열기
        // StreamWriter의 두번째 인자에 true 전달
        // 파일이 없으면 새로 생성
        // 파일이 있으면 이어쓰기
        using (var writer = new StreamWriter(fullPath, append: true))
        {
            string line = $"{newPerson.Name},{newPerson.Age},{newPerson.Location}";
            writer.WriteLine(line);
        }

        Console.WriteLine();
    }
}