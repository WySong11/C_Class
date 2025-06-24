using System;
using System.Collections.Generic;
using System.Linq;

public class TimeRecordRanking
{
    private const int MAX_RECORDS = 10;

    private List<TimeSpan> records = new List<TimeSpan>();
    private Dictionary<int, string> recordNames = new Dictionary<int, string>();


    // 인덱서 : 클래스의 인스턴스를 배열처럼 사용할 수 있음
    // this 키워드를 사용하여 인스턴스의 속성이나 메서드에 접근할 수 있음
    public TimeSpan this[int index]
    {
        get
        {
            if (index < 0 || index >= records.Count)
            {
                Console.WriteLine("잘못된 인덱스입니다. 0부터 {0}까지의 인덱스를 사용하세요.", records.Count - 1);
                return TimeSpan.Zero; // 잘못된 인덱스에 대한 기본값 반환
            }
            return records[index];
        }
    }

    // string ranker = ""로 설정하여 기본값을 빈 문자열로 지정
    public void AddRecord(TimeSpan record, string ranker = "")
    {
        records.Add(record);
        records.Sort(); // 기록을 오름차순으로 정렬

        if (records.Count > MAX_RECORDS)
        {
            records.RemoveAt(records.Count - 1); // 최대 기록 수를 초과하면 마지막 기록 제거
        }

        if( recordNames.ContainsKey(records.IndexOf(record) + 1) )
        {
            recordNames[records.IndexOf(record) + 1] = ranker; // 기존 기록의 랭커 이름 업데이트
        }
        else
        {
            recordNames.Add(records.IndexOf(record) + 1, ranker); // 새로운 기록에 랭커 이름 추가
        }
    }

    public void PrintRecords()
    {
        if (records.Count == 0)
        {
            Console.WriteLine("\n기록이 없습니다.");
            return;
        }
        Console.WriteLine("\n기록 목록:");
        for (int i = 0; i < records.Count; i++)
        {
            string rankerName = recordNames.ContainsKey(i + 1) ? recordNames[i + 1] : "익명";
            Console.WriteLine($"{i + 1}. {rankerName} {records[i].TotalSeconds}초");
        }
    }

    public void ClearRecords()
    {
        records.Clear();
        Console.WriteLine("\n기록이 초기화되었습니다.");
    }

    public int Count => records.Count;
}