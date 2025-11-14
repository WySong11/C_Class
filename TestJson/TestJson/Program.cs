using System;
using System.IO;
using System.Text.Json;
using static System.Console;

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class AppSettings
{
    public string AppName { get; set; }
    public string Version { get; set; }
    public bool EnableLogging { get; set; }
}

public class  Database
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
}

public class Config
{     
    public AppSettings AppSettings { get; set; }
    public Database Database { get; set; }
}

public class Program
{
    const string DirPath = "JSON";

    static void Main(string[] args)
    {

        string filePath = Path.Combine(DirPath, "person.json");

        ///////////////////////////////////////////////////////////////
        // 쓰기

        var person = new Person
        {
            Name = "Alice",
            Age = 30        
        };

        if(Directory.Exists(DirPath) == false)
        {
            Directory.CreateDirectory(DirPath);
        }

        // Serialize : 객체를 JSON 문자열로 변환
        string jsonString = JsonSerializer.Serialize(person);
        File.WriteAllText(filePath, jsonString);

        WriteLine("JSON 저장 완료");
        WriteLine();

        ///////////////////////////////////////////////////////////////
        // 읽기

        string readJson = File.ReadAllText(filePath);

        // Deserialize : JSON 문자열을 객체로 변환
        var loadedPerson = JsonSerializer.Deserialize<Person>(readJson);

        WriteLine("JSON 읽기 완료");
        WriteLine($"Name: {loadedPerson.Name}, Age: {loadedPerson.Age}");

        ///////////////////////////////////////////////////////////////
        ///

        Person person1 = new Person { Name = "Bob", Age = 25 };
        Database database = new Database
        {
            Host = "localhost",
            Port = 5432,
            User = "admin",
            Password = "password"
        };

        Config config1 = new Config
        {
            AppSettings = new AppSettings
            {
                AppName = "MyApp",
                Version = "1.0.0",
                EnableLogging = true
            },
            Database = database
        };

        //string json = File.ReadAllText(Path.Combine(DirPath, "config.json"));


        if(Directory.Exists(DirPath) == false)
        {
            Directory.CreateDirectory(DirPath);
        }

        string configPath = Path.Combine(DirPath, "config.json");

        // 파일이 있으면 읽고, 없으면 생성
        if (File.Exists(configPath))
        {
            string existing = File.ReadAllText(configPath);
            // 기존 설정 사용
        }
        else
        {
            string jsonContent = JsonSerializer.Serialize(config1, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(configPath, jsonContent);
        }

        //////////////////////////////////////////////
        ///

        StudentList studentList = new StudentList();
        studentList.Run();
    }
}