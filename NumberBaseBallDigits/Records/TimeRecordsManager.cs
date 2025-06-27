using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

// JSON(JavaScript Object Notation)은 데이터를 저장하거나 전송할 때 사용하는 가볍고 읽기 쉬운 데이터 형식입니다.
// 주로 웹 애플리케이션에서 서버와 클라이언트 간의 데이터를 주고받을 때 사용됩니다.

// JSON은 텍스트 기반의 데이터 형식으로, 객체와 배열을 표현할 수 있습니다.
// C#에서는 System.Text.Json 네임스페이스를 사용하여 JSON 데이터를 쉽게 직렬화(serialize)하고 역직렬화(deserialize)할 수 있습니다.

// 객체(Object)는 중괄호({})로 감싸고, 속성은 키-값 쌍으로 표현합니다.
/*{
    "name": "Alice",
    "age": 25,
    "isStudent": false
   }
*/

// 배열(Array)은 대괄호([])로 감싸고, 값들은 쉼표(,)로 구분합니다.
/*
 [
  "apple",
  "banana",
  "cherry"
 ]
*/

/*
타입	예시
문자열	"name": "Alice"
숫자	"age": 25
불리언	"isStudent": false
배열	"fruits": ["apple", "banana"]
객체	"address": { "city": "Seoul" }
null	"nickname": null

{
    "user": 
    {
        "id": 101,
        "name": "John Doe",
        "email": "john@example.com",
        "roles": ["admin", "editor"],
        "active": true
    }
}
*/


public class TimeRecordsManager
{
    private const int MAX_RECORDS = 10; // 최대 기록 수를 상수로 정의
    private const string DEFAULT_RANKER = "Player"; // 기본 랭커 이름
    private const string RECORD_FILE_PATH = "records.json"; // JSON 파일 경로

    // 싱글턴 패턴을 사용하여 TimeRecordsManager의 인스턴스를 하나만 생성
    private static TimeRecordsManager? _instance;

    // 락 객체를 사용하여 멀티스레드 환경에서의 안전성을 보장
    private static readonly object _lock = new object(); // 락 객체

    public static TimeRecordsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TimeRecordsManager();
            }
            return _instance;
        }
    }


    private List<TimeRecord> records;

    private TimeRecordsManager()
    {
        records = new List<TimeRecord>();
    }

    public void AddRecord(TimeSpan record, string ranker = DEFAULT_RANKER)
    {
        records.Add(new TimeRecord(record, ranker));
        records.Sort((x, y) => x.Record.CompareTo(y.Record)); // 오름차순 정렬
        if (records.Count > MAX_RECORDS)
        {
            records.RemoveAt(records.Count - 1); // 최대 10개의 기록만 유지
        }
    }

    public List<TimeRecord> GetRecords()
    {
        return records;
    }

    public void ClearRecords()
    {
        records.Clear();
    }

    public void PrintRecords()
    {
        foreach (var record in records)
        {
            Console.WriteLine(record);
        }
    }

    // JSON 저장
    public void SaveRecordsToJson(string filePath = RECORD_FILE_PATH)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(records);
        System.IO.File.WriteAllText(filePath, json);

        Console.WriteLine("\n기록 파일을 성공적으로 저장했습니다.\n");
    }

    // JSON 불러오기
    public void LoadRecordsFromJson(string filePath = RECORD_FILE_PATH)
    {
        if (System.IO.File.Exists(filePath))
        {
            var json = System.IO.File.ReadAllText(filePath);
            records = System.Text.Json.JsonSerializer.Deserialize<List<TimeRecord>>(json) ?? new List<TimeRecord>();

            Console.WriteLine("\n기록 파일을 성공적으로 불러왔습니다.");
            Console.WriteLine($"총 {records.Count}개의 기록이 있습니다.\n");
        }
        else
        {
            Console.WriteLine("기록 파일이 존재하지 않습니다.");
        }
    }
}

