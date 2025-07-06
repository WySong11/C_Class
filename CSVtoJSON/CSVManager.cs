using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

// sealed : 해당 클래스는 단 하나의 인스턴스만 존재할 수 있도록 보장합니다.
// 해당 클래스를 상속 받을 수 없으며, 이는 싱글턴 패턴을 구현할 때 유용합니다.

public sealed class CSVManager
{
    // readonly : 해당 필드는 초기화 이후 변경할 수 없으며, 이는 멀티스레드 환경에서 안전하게 사용할 수 있도록 보장합니다.
    // Lazy<T> : 인스턴스가 실제로 필요할 때까지 생성하지 않습니다. 이는 성능 최적화에 유용합니다.

    private static readonly Lazy<CSVManager> _instance = new(() => new CSVManager());

    // 싱글턴 인스턴스에 접근하기 위한 프로퍼티
    // _instance.Value에서 Value는 Lazy<T> 클래스의 속성입니다.
    // Lazy<T>의 Value 속성은 객체를 안전하게, 필요할 때 한 번만 생성하도록 보장합니다.

    public static CSVManager Instance => _instance.Value;

    private CSVManager() { }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////

    // CSV 파일 읽기: 각 행을 string[]로 반환, 구분자 지정 가능
    // 한 행을 읽어서 delimiter(예: ,)로 분리하면, 각 열의 값이 문자열 배열(string[])로 저장됩니다.
    // List<string[]>: 여러 행(row)을 저장
    // string[]: 각 행(row)의 열(column) 값을 저장
    // 예: row[0] → level, row[1] → hp, row[2] → atk

    public List<string[]> Read(string filePath, Encoding? encoding = null, char delimiter = ',')
    {
        if(File.Exists(filePath) == false)
        {
            Console.WriteLine($"파일이 존재하지 않습니다: {filePath}");
            return new List<string[]>();
        }

        var result = new List<string[]>();
        encoding ??= Encoding.UTF8;

        using var reader = new StreamReader(filePath, encoding);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            // 지정된 구분자로 분리
            var values = line.Split(delimiter);
            result.Add(values);
        }
        return result;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////

    // CSV 파일 쓰기: 각 행을 string[]로 받아 파일에 저장, 구분자 지정 가능
    public void Write(string filePath, IEnumerable<string[]> rows, Encoding? encoding = null, char delimiter = ',')
    {
        encoding ??= Encoding.UTF8;

        using var writer = new StreamWriter(filePath, false, encoding);
        foreach (var row in rows)
        {
            // 지정된 구분자로 결합하여 한 줄로 작성
            var line = string.Join(delimiter, row);
            writer.WriteLine(line);
        }
    }
}

