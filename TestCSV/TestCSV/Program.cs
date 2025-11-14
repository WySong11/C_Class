using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

public class Program
{
    const string DirPath = "CSV";
    const string FileName = "test.csv";

    static void Main(string[] args)
    {
        /////////////////////////////////////////////////////////
        //  Read CSV File

        string fullPath = Path.Combine(DirPath, FileName);

        if(File.Exists(fullPath) == false)
        {
            WriteLine($"File not found: {fullPath}");
            return;
        }

        using ( var reader = new StreamReader(fullPath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                var values = line.Split(',');
                WriteLine($"Column 1: {values[0]}, Column 2: {values[1]}");
            }
        }


        /////////////////////////////////////////////////////////
        // Write CSV File

        string outputFileName = "output.csv";
        string outputFullPath = Path.Combine(DirPath, outputFileName);

        try
        {
            using ( var writer = new StreamWriter(outputFullPath))
            {
                writer.WriteLine("Name,Age,Location");
                writer.WriteLine("Alice,30,New York");
                writer.WriteLine("Bob,25,Los Angeles");
                writer.WriteLine("Charlie,35,Chicago");
            }
        }
        catch (IOException ioEx)
        {
            WriteLine($"IO Error reading CSV: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            WriteLine($"Error reading CSV: {ex.Message}");
        }

        /////////////////////////////////////////////////////////

        List<Person> people = new List<Person>();

        try
        {
            using (var reader = new StreamReader(outputFullPath))
            {
                string? headerLine = reader.ReadLine(); // Read header
                WriteLine($"Header: {headerLine}");
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    var person = new Person(values[0], int.Parse(values[1]), values[2]);

                    people.Add(person);
                }
            }
        }
        catch (IOException ioEx)
        { 
            WriteLine($"IO Error reading CSV: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            WriteLine($"Error reading CSV: {ex.Message}");
        }


        WriteLine();

        foreach ( var person in people)
        {
            WriteLine($"PeopleList : {person.Name}, Age: {person.Age}, Location: {person.Location}");
        }

        ////////////////////////////////////
        ///
        // 입력 받아서 CSV 파일에 추가하기
        // 출력하기

        // CSVAnswer 클래스 객체 생성 및 실행
        CSVAnswer test = new CSVAnswer();

        // CSV 파일 읽기 및 쓰기 실행
        test.Run();
    }
}