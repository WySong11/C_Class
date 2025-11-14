using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Student
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string StudentID { get; set; }
}

public class StudentList
{
    const string directoryPath = "JSON";
    const string filePath = "students.json";

    public void Run()
    {
        // 1. 학생 여러 명을 List로 생성
        List<Student> students = new List<Student>
        {
            new Student { Name = "John Doe", Age = 20, StudentID = "S1001" },
            new Student { Name = "Jane Smith", Age = 22, StudentID = "S1002" },
            new Student { Name = "Sam Brown", Age = 21, StudentID = "S1003" }
        };

        // JSON 직렬화 옵션 설정
        var options = new JsonSerializerOptions { WriteIndented = true };


        // 2. List를 JSON 문자열로 직렬화
        string jsonText = JsonSerializer.Serialize(students, options);

        // 3. JSON 문자열을 파일로 저장
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string fullPath = Path.Combine(directoryPath, filePath);

        File.WriteAllText(fullPath, jsonText);

        // 4. 파일에서 JSON 문자열을 읽어오기

        string loadedJsonText = File.ReadAllText(fullPath);

        // 5. JSON 문자열을 List로 역직렬화
        List<Student> loadedStudents = JsonSerializer.Deserialize<List<Student>>(loadedJsonText);

        // 6. 역직렬화된 List 출력
        foreach (var student in loadedStudents)
        {
            Console.WriteLine($"Name: {student.Name}, Age: {student.Age}, StudentID: {student.StudentID}");
        }
    }
}
