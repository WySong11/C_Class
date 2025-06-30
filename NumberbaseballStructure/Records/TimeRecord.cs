using System;
using System.Collections;
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

    // System.Text.Json은 생성자 기반 역직렬화를 시도할 때, 생성자의 매개변수 이름이 속성 이름과 정확히 일치해야 하고,
    // 기본적으로 **필드가 아닌 속성(property)**에만 매핑한다는 것입니다.
    // 기본 생성자(default constructor) 를 제공해서 System.Text.Json이 먼저 객체를 만들고, 이후 프로퍼티에 값을 할당하도록 합니다.
    public TimeRecord() { }

    public TimeRecord(int InPlayCount, TimeSpan InPlayTime, string? InPlayerName)
    {
        PlayerName = InPlayerName;
        PlayCount = InPlayCount;
        PlayTime = InPlayTime;
    }

    // 연산자 오버로딩을 통해 TimeRecord 객체 간의 비교를 가능하게 합니다.
    // 이 연산자들은 TimeRecord 객체를 비교할 때 PlayCount와 PlayTime을 기준으로 합니다.
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

    // IComparable 인터페이스를 구현하여 TimeRecord 객체를 비교할 수 있도록 합니다.
    // CompareTo 메서드는 현재 객체(this)와 다른 객체(other)를 비교하여 정렬 순서를 결정합니다.
    public int CompareTo(TimeRecord other)
    {
        if (this < other) return -1;
        if (this > other) return 1;
        return 0; // 같을 경우
    }

    // Equals 메서드를 오버라이드하여 TimeRecord 객체의 동등성을 비교합니다.    
    // Equals()는 객체의 동등성을 비교하는 메서드로, 두 객체가 동일한 값을 가지는지 확인합니다.
    public override bool Equals(object? obj)
    {
        if (obj is TimeRecord other)
        {
            return this == other;
        }
        return false;
    }

    // GetHashCode 메서드를 오버라이드하여 TimeRecord 객체의 해시 코드를 반환합니다.
    // GetHashCode()는 객체를 숫자로 표현한 해시 식별자를 반환하는 함수.
    // Dictionary, HashSet, Hashtable 등은 내부적으로 GetHashCode()를 사용하여 객체를 빠르게 찾거나 비교합니다.
    // 해시 코드는 객체의 고유한 식별자로 사용되며, 객체의 속성 값에 따라 결정됩니다.
    // GetHashCode()는 객체의 해시 값을 반환하며, 이 값은 객체의 속성 값에 따라 결정됩니다.
    // GetHashCode()는 객체의 고유한 식별자로 사용되며, 해시 테이블과 같은 자료구조에서 객체를 빠르게 찾거나 비교하는 데 사용됩니다.
    // GetHashCode()는 객체의 속성 값에 따라 결정되며, 객체의 속성 값이 동일하면 동일한 해시 코드를 반환해야 합니다.

    // Equals()와 함께 객체의 동등성 비교나 컬렉션 내 검색에서 사용    
    // 따라서, 객체의 속성 값이 동일하면 동일한 해시 코드를 반환해야 합니다.
    // 이는 해시 테이블과 같은 자료구조에서 객체를 효율적으로 찾을 수 있도록 합니다.

    // 따라서, Equals() 메서드와 GetHashCode() 메서드는 항상 일관되게 동작해야 합니다.
    // 커스텀 클래스에서는 반드시 Equals()와 함께 재정의해야 일관성 유지
    public override int GetHashCode()
    {
        return HashCode.Combine(PlayerName, PlayCount, PlayTime);
    }
}