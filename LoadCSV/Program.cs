using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Employee
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Email { get; set; }
}


public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        WriteCSV(); // CSV 파일 생성

        ReadCSV();  // CSV 파일 읽기
    }

    static void WriteCSV(string filePath = "test.csv")
    {
        // CSV 파일 경로 설정
        using (var writer = new StreamWriter(filePath))
        {
            // 헤더 작성
            writer.WriteLine("ID,Name,Department,Email");

            // 데이터 작성
            writer.WriteLine("101,김민지,인사부,minji.kim@example.com");
            writer.WriteLine("102,박지훈,마케팅부,jihoon.park@example.com");
            writer.WriteLine("103,이수연,개발부,sooyeon.lee@example.com");
            writer.WriteLine("104,최유진,재무부,yujin.choi@example.com");
        }

        Console.WriteLine("CSV 파일이 성공적으로 생성되었습니다!");

    }

    static void ReadCSV(string filePath = "test.csv")
    {
        var employees = new List<Employee>();

        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        {
            bool isHeader = true;

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (line == null) // Null 체크 추가
                {
                    continue;
                }

                if (isHeader)
                {
                    isHeader = false; // 첫 줄은 헤더이므로 건너뜁니다.
                    continue;
                }

                var values = line.Split(',');

                if (values.Length < 4) // 배열 길이 체크 추가
                {
                    continue;
                }

                var employee = new Employee
                {
                    ID = int.Parse(values[0]),
                    Name = values[1],
                    Department = values[2],
                    Email = values[3]
                };

                employees.Add(employee);
            }
        }

        // 결과 출력
        Console.WriteLine("CSV 파일에서 읽은 직원 목록:");
        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.ID} | {emp.Name} | {emp.Department} | {emp.Email}");
        }
    }
}

