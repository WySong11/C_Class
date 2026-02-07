using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

public class program
{
    const string filePath = "testcsv.csv";

    public static void Main()
    {
        if (File.Exists(filePath) == false)
            return;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                // ,(콤마)로 구분된 값들을 배열로 분리
                // ReadLine : 한 줄씩 문자열을 읽어오는 메서드입니다.
                // Split : 문자열을 특정 구분자로 나누어 배열로 반환하는 메서드입니다.
                string[] values = line.Split(',');
                foreach (string value in values)
                {
                    Write($"{value} ");
                }
                WriteLine();
            }
        }

        /////////////////////////////////////////////////////
        ///
        WriteLine("\n");
        List<testcsvData> list = new List<testcsvData>();   

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line;

            // 첫 번째 줄은 헤더이므로 읽고 넘어감
            line = reader.ReadLine(); 

            while ((line = reader.ReadLine()) != null)
            {
                // ,(콤마)로 구분된 값들을 배열로 분리
                string[] values = line.Split(',');

                // testcsvData 객체 생성 및 값 할당
                testcsvData data = new testcsvData(int.Parse(values[0]), values[1]);

                // 리스트에 추가
                list.Add(data);
                //WriteLine($"id: {data.id}, name: {data.name}");
            }
        }

        foreach(var item in list)
        {
            WriteLine($"id: {item.id}, name: {item.name}");
        }
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