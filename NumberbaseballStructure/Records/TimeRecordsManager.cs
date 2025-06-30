using System;
using System.Collections.Generic;
using System.Threading;

// JSON(JavaScript Object Notation)은 데이터를 저장하거나 전송할 때 사용하는 가볍고 읽기 쉬운 데이터 형식입니다.
// 주로 웹 애플리케이션에서 서버와 클라이언트 간의 데이터를 주고받을 때 사용됩니다.

// JSON은 텍스트 기반의 데이터 형식으로, 객체와 배열을 표현할 수 있습니다.
// C#에서는 System.Text.Json 네임스페이스를 사용하여 JSON 데이터를 쉽게 직렬화(serialize)하고 역직렬화(deserialize)할 수 있습니다.
// 직렬화 는 객체를 JSON 문자열로 변환하는 과정이고, 역직렬화는 JSON 문자열을 객체로 변환하는 과정입니다.

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

// Singleton 패턴을 사용하여 TimeRecordsManager의 인스턴스를 하나만 생성하고,
// 멀티스레드 환경에서도 안전하게 기록을 관리하는 TimeRecordsManager 클래스를 정의합니다.
// TimeRecordsManager는 시간 기록을 추가, 조회, 삭제, JSON 파일로 저장 및 불러오기 기능을 제공합니다.

// 프로세스(Process): 실행 중인 프로그램 자체
// 스레드(Thread): 프로세스 안에서 실제로 코드를 실행하는 작업 단위
// 하나의 프로세스는 여러 개의 스레드를 가질 수 있어 → 이걸 멀티스레딩이라고 함.
// 동기화(Synchronization): 여러 스레드가 동시에 공유 자원에 접근할 때 충돌을 방지하기 위해 사용하는 기술

// Singleton 패턴은 클래스의 인스턴스를 하나만 생성하고,
// 전역적으로 접근할 수 있도록 하는 디자인 패턴입니다.
// 멀티스레드 환경에서 안전하게 동작하도록 하기 위해 락을 사용하여 동기화를 구현합니다.

public class TimeRecordsManager
{
    private const int MAX_RECORDS = 10; // 최대 기록 수를 상수로 정의
    private const string DEFAULT_RANKER = "Player"; // 기본 랭커 이름
    private const string RECORD_DIR_PATH = "\\Records\\"; // JSON 파일이 저장될 디렉토리
    private const string RECORD_FILE_PATH = "records.json"; // JSON 파일 경로

    // 싱글턴 패턴을 사용하여 TimeRecordsManager의 인스턴스를 하나만 생성
    private static TimeRecordsManager? _instance;

    // _lock 객체를 기준으로 한 스레드가 lock (_lock)에 들어가면, 다른 스레드는 해당 블록이 끝날 때까지 기다리게 됨.
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

    // lock을 사용하여 멀티스레드 환경에서 안전하게 인스턴스를 생성
    // 
    public static TimeRecordsManager GetInstance()
    {
        // 여러 스레드가 동시에 GetInstance()를 호출하면, if (_instance == null) 조건을 동시에 통과할 수 있음.
        // 이 경우, 두 개 이상의 스레드가 동시에 _instance를 생성하려고 시도하게 되어,
        // 싱글턴 패턴의 목적에 어긋나게 됩니다.
        // 따라서, _instance가 null인 경우에만 인스턴스를 생성하도록 하고,
        // 이때, lock (_lock)을 사용하여 멀티스레드 환경에서 안전하게 인스턴스를 생성합니다.
        // lock (_lock)을 사용하여 멀티스레드 환경에서 안전하게 인스턴스를 생성        

        lock (_lock)
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

    public void AddRecord(int playcount, TimeSpan record, string ranker = DEFAULT_RANKER)
    {
        // 새로운 기록을 추가하기 전에 기존 기록과 비교하여 순위를 결정
        // 랭커 이름이 비어있으면 기본 랭커 이름을 사용
        if (string.IsNullOrEmpty(ranker))
        {
            ranker = DEFAULT_RANKER;
        }

        // 새로운 기록을 TimeRecord 객체로 생성
        records.Add(new TimeRecord(playcount, record, ranker));

        // 새로운 기록을 추가하기 전에 기존 기록과 비교하여 순위를 결정
        // 랭커 이름이 비어있으면 기본 랭커 이름을 사용
        if (records.Count > 0)
        {
            // playcount가 작은 순서 정렬
            // playcount가 같으면 record가 작은 순서 정렬
            // 랭커 이름이 같으면 알파벳 순서 정렬
            // TimeRecord 클래스의 CompareTo 메서드를 사용하여 정렬
            // records 리스트가 최대 기록 수를 초과하는 경우, 기존 기록을 정렬하여 가장 낮은 기록을 제거

            records.Sort((x, y) =>
            {
                int playCountComparison = x.PlayCount.CompareTo(y.PlayCount);
                if (playCountComparison != 0)
                {
                    return playCountComparison; // 플레이 횟수로 정렬
                }
                int recordComparison = x.PlayTime.CompareTo(y.PlayTime);
                if (recordComparison != 0)
                {
                    return recordComparison; // 기록 시간으로 정렬
                }
                return string.Compare(x.PlayerName, y.PlayerName, StringComparison.Ordinal); // 랭커 이름으로 정렬
            });
        }

        // 최대 기록 수를 초과하는 경우, 가장 낮은 기록을 제거
        if (records.Count > MAX_RECORDS)
        {
            records.RemoveAt(records.Count - 1); // 가장 낮은 기록 제거
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
        Console.WriteLine();
        if (records.Count == 0)
        {
            Console.WriteLine("기록이 없습니다.\n");            
            return;
        }

        int rank = 1;
        foreach (var record in records)
        {
            Console.WriteLine($"순위 : {rank++} , 플레이어: {record.PlayerName} , 플레이 횟수: {record.PlayCount} , 플레이 시간: {record.PlayTime}");
        }
        Console.WriteLine();
    }

    // JSON 저장
    public void SaveRecordsToJson(string dirPath = RECORD_DIR_PATH, string filePath = RECORD_FILE_PATH)
    {
        var json = System.Text.Json.JsonSerializer.Serialize(records);

        // 실행 중인 애플리케이션의 기본 디렉토리를 기준으로 경로를 설정
        dirPath = AppDomain.CurrentDomain.BaseDirectory + dirPath;

        // 디렉토리가 존재하지 않으면 생성
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }

        // JSON 파일을 지정된 경로에 저장
        System.IO.File.WriteAllText(dirPath + filePath, json);

        Console.WriteLine("\n기록 파일을 성공적으로 저장했습니다.\n");
    }

    // JSON 불러오기
    public void LoadRecordsFromJson(string dirPath = RECORD_DIR_PATH, string filePath = RECORD_FILE_PATH)
    {
        // 실행 중인 애플리케이션의 기본 디렉토리를 기준으로 경로를 설정
        dirPath = AppDomain.CurrentDomain.BaseDirectory + dirPath;

        if (System.IO.File.Exists(dirPath + filePath))
        {
            var json = System.IO.File.ReadAllText(dirPath + filePath);
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

