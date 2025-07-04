using System;
using System.Collections.Generic;
using System.Linq;

public class TimeRecordsManager
{
    private const int MAX_RECORDS = 10; // 최대 기록 수를 상수로 정의
    private const string DEFAULT_RANKER = "Player"; // 기본 랭커 이름
    private const string RECORD_DIR_PATH = "\\Records\\"; // JSON 파일이 저장될 디렉토리
    private const string RECORD_FILE_PATH = "records.json"; // JSON 파일 경로

    // 싱글턴 패턴을 사용하여 TimeRecordsManager의 인스턴스를 하나만 생성
    private static TimeRecordsManager? _instance;


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

    public void AddRecord(int playcount, TimeSpan playTime, string ranker = DEFAULT_RANKER)
    {
        // 새로운 기록을 추가하기 전에 기존 기록과 비교하여 순위를 결정
        // 랭커 이름이 비어있으면 기본 랭커 이름을 사용
        if (string.IsNullOrEmpty(ranker))
        {
            ranker = DEFAULT_RANKER;
        }

        // 새로운 기록을 TimeRecord 객체로 생성
        records.Add(new TimeRecord(playcount, playTime, ranker));

        // playcount가 작은 순서 정렬
        // playcount가 같으면 record가 작은 순서 정렬
        // 랭커 이름이 같으면 알파벳 순서 정렬
        // TimeRecord 클래스의 CompareTo 메서드를 사용하여 정렬
        // records 리스트가 최대 기록 수를 초과하는 경우, 기존 기록을 정렬하여 가장 낮은 기록을 제거

        // Sort로 정렬
        records.Sort( (x, y) => x.PlayCount == y.PlayCount ? ( x.PlayTime == y.PlayTime ? string.Compare(x.PlayerName, y.PlayerName) 
        : x.PlayTime.CompareTo(y.PlayTime) ) : x.PlayCount.CompareTo(y.PlayCount) );

        // Linq를 사용하여 정렬
        // Prderby를 사용하여 정렬
        // ThenBy를 사용하여 추가 정렬
        // 1. PlayCount 오름차순
        // 2. PlayTime 오름차순
        // 3. PlayerName 오름차순
        // 4. 랭커 이름이 같으면 알파벳 순서 정렬

        records = records
            .OrderBy(r => r.PlayCount)
            .ThenBy(r => r.PlayTime)
            .ThenBy(r => r.PlayerName)
            .ToList();


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