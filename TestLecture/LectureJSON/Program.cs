using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static System.Console;

public class Person
{
    public string? Name { get; set; }
    public int Age { get; set; }
}

public class program
{
    static void Main()
    {
        string filePath = "person.json";

        var person = new Person
        {
            Name = "Alice",
            Age = 30
        };

        // JSON Serialization
        // Serialize : Object -> JSON string
        string jsonString = JsonSerializer.Serialize(person);
        File.WriteAllText(filePath, jsonString);

        // JSON Deserialization
        // Deserialize : JSON string -> Object
        string readJsonString = File.ReadAllText(filePath);
        var loadedPerson = JsonSerializer.Deserialize<Person>(readJsonString);

        WriteLine($"Name: {loadedPerson?.Name}, Age: {loadedPerson?.Age}");

        ////////////////////////////////////////////////////////////////
        ///

        // CSV to JSON Example

        WriteLine("\n=== CSV to JSON Example ===\n");

        CSVtoJSON csvToJson = new CSVtoJSON();

        string csvFilePath = "testcsv.csv";
        string jsonFilePath = "save.json";

        csvToJson.LoadFromCSV(csvFilePath);

        foreach (var item in csvToJson.list)
        {
            WriteLine($"id: {item.id}, name: {item.name}");
        }

        csvToJson.SaveToJSON(jsonFilePath);
    }
}

public class testcsvData
{
    public int id;
    public string name;

    public testcsvData() { }
    public testcsvData(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}

public class  CSVtoJSON
{
    public List<testcsvData> list = new List<testcsvData>();

    public CSVtoJSON() { }

    public void LoadFromCSV(string filePath)
    {
        if (File.Exists(filePath) == false)
            return;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;
            line = reader.ReadLine(); // 헤더 읽고 넘어감
            while ((line = reader.ReadLine()) != null)
            {
                string[] values = line.Split(',');
                testcsvData data = new testcsvData(int.Parse(values[0]), values[1]);
                list.Add(data);
            }
        }
    }

    public void SaveToJSON(string jsonFilePath)
    {
        // JsonSerializerOptions : JSON 직렬화 옵션 설정
        // WriteIndented = true : JSON 문자열을 들여쓰기하여 가독성 높임
        // IncludeFields = true : 필드도 직렬화 대상에 포함
        // Encoder = UnsafeRelaxedJsonEscaping : 특수문자 이스케이프 완화
        // Sytem.Text.Json 은 기본 설정에서 public "property" 만 직렬화 대상이므로
        // 필드를 포함하려면 IncludeFields 옵션을 true로 설정해야 함
        // 이 옵션들을 사용하여 JSON 문자열 생성

        var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping  };

        string jsonString = JsonSerializer.Serialize(list, options);
        File.WriteAllText(jsonFilePath, jsonString);
    }
}