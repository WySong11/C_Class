using System;
using System.Numerics;

// 구조체는 값형식( Value Typoe ) : 스택에 저장되어 성능이 빠르고, 복사 시 값 자체가 복제됩니다.
// 상속 불가
// 데이터의 크기가 작고 단순할 때
// 객체가 자주 생성되고 파괴되는 경우( 가비지 컬렉션 부담 회피 )

// 클래스는 참조 형식( Reference Type ) : 힙에 저장됨 상대적으로 느림. 
// 상속 가능
// 데이터 크기가 크거나 복잡한 객체
// 상속, 다형성 등 객체지향 설계가 필요한 경우


public struct TimeRecordStruct
{
    public string? PlayerName;
    public int PlayCount;
    public TimeSpan PlayTime;

    public TimeRecordStruct(int InPlayCount, TimeSpan InPlayTime, string? InPlayerName)
    {
        PlayerName = InPlayerName;
        PlayCount = InPlayCount;
        PlayTime = InPlayTime;
    }

    public static bool operator >(TimeRecordStruct a, TimeRecordStruct b)
    {
        if (a.PlayCount > b.PlayCount)
        {
            return true;
        }
        else if (a.PlayCount == b.PlayCount)
        {
            return a.PlayTime > b.PlayTime;
        }

        return false;
    }

    public static bool operator <(TimeRecordStruct a, TimeRecordStruct b)
    {
        if (a.PlayCount < b.PlayCount)
        {
            return true;
        }
        else if (a.PlayCount == b.PlayCount)
        {
            return a.PlayTime < b.PlayTime;
        }

        return false;
    }

    public static bool operator >=(TimeRecordStruct a, TimeRecordStruct b)
    {
        return (a > b) || (a == b);
    }

    public static bool operator <=(TimeRecordStruct a, TimeRecordStruct b)
    {
        return (a < b) || (a == b);
    }

    public static bool operator ==(TimeRecordStruct a, TimeRecordStruct b)
    {
        return a.PlayCount == b.PlayCount && a.PlayTime == b.PlayTime && a.PlayerName == b.PlayerName;
    }

    public static bool operator !=(TimeRecordStruct a, TimeRecordStruct b)
    {
        return !(a == b);
    }

    public int CompareTo(TimeRecordStruct other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        return 0; // 같을 경우
    }

    public override bool Equals(object? obj)
    {
        if (obj is TimeRecordStruct other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PlayerName, PlayCount, PlayTime);
    }
}

public class TimeRecord
{
    public string? PlayerName { get; set; }
    public int PlayCount { get; set; }
    public TimeSpan PlayTime { get; set; }

    public TimeRecord() { }

    public TimeRecord(int InPlayCount, TimeSpan InPlayTime, string? InPlayerName)
    {
        PlayerName = InPlayerName;
        PlayCount = InPlayCount;
        PlayTime = InPlayTime;
    }

    public static bool operator >(TimeRecord a, TimeRecord b)
    {
        if (a.PlayCount > b.PlayCount)
        {
            return true;
        }
        else if (a.PlayCount == b.PlayCount)
        {
            return a.PlayTime > b.PlayTime;
        }

        return false;
    }

    public static bool operator <(TimeRecord a, TimeRecord b)
    {
        if (a.PlayCount < b.PlayCount)
        {
            return true;
        }
        else if (a.PlayCount == b.PlayCount)
        {
            return a.PlayTime < b.PlayTime;
        }

        return false;
    }

    public static bool operator >=(TimeRecord a, TimeRecord b)
    {
        return (a > b) || (a == b);
    }

    public static bool operator <=(TimeRecord a, TimeRecord b)
    {
        return (a < b) || (a == b);
    }

    public static bool operator ==(TimeRecord a, TimeRecord b)
    {
        return a.PlayCount == b.PlayCount && a.PlayTime == b.PlayTime && a.PlayerName == b.PlayerName;
    }

    public static bool operator !=(TimeRecord a, TimeRecord b)
    {
        return !(a == b);
    }

    public int CompareTo(TimeRecord other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        return 0; // 같을 경우
    }

    public override bool Equals(object? obj)
    {
        if (obj is TimeRecord other)
        {
            return this == other;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PlayerName, PlayCount, PlayTime);
    }
}