// Program.cs
// C# 자료형(기본형 + 자주 쓰는 타입) 강의용 예제
// - 목표: "자료형이 왜 필요한지", "값 형식/참조 형식 차이", "변환/파싱", "null/nullable"을
//         입문자 눈높이로 콘솔 출력으로 확인한다.
//
// 사용 방법:
// 1) .NET 8 콘솔앱 생성 후 Program.cs 전체를 이 코드로 교체
// 2) 실행(F5)하고 출력 보면서 섹션별로 설명

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;

PrintTitle("C# 자료형 강의용 데모");

// 각 섹션을 순서대로 실행
Demo_Basics();
Demo_NumericTypes();
Demo_CharAndString();
Demo_BoolAndComparison();
Demo_ValueTypeVsReferenceType();
Demo_NullAndNullable();
Demo_TypeInference_var();
Demo_Conversions_Casting_Parse_TryParse();
Demo_Overflow_Checked();
Demo_Enum();
Demo_Object_BoxingUnboxing();
Demo_ArraysAndList();
Demo_DateTime();

// 끝
PrintTitle("끝! (강의 팁/연습문제는 코드 아래 설명 참고)");
Console.WriteLine("아무 키나 누르면 종료합니다...");
Console.ReadKey();


// =========================
// 아래부터 "강의용 함수들"
// =========================

static void Demo_Basics()
{
    PrintSection("1) 자료형이 왜 필요할까? (메모리/표현/연산)");

    // 자료형(Type)은 "이 값이 어떤 형태의 데이터인지"를 컴퓨터에게 알려주는 규칙이다.
    // 예) 100은 정수(int)로 처리할 수도 있지만, 100.5는 실수(double)로 처리해야 한다.
    int age = 20;            // 정수
    double height = 175.5;   // 실수
    string name = "Alex";    // 문자열

    // 출력
    Console.WriteLine($"age   = {age} (Type: {age.GetType().Name})");
    Console.WriteLine($"height= {height} (Type: {height.GetType().Name})");
    Console.WriteLine($"name  = {name} (Type: {name.GetType().Name})");

    // 자료형을 정해두면:
    // 1) 메모리에 어떻게 저장할지(크기/형태) 결정된다.
    // 2) 어떤 연산이 가능한지 결정된다. (정수 나눗셈 vs 실수 나눗셈 등)
    // 3) 잘못된 값/연산을 컴파일 단계에서 많이 잡아준다.
}

static void Demo_NumericTypes()
{
    PrintSection("2) 숫자 자료형 (정수/실수/decimal)");

    // ---- 정수형: 음수/양수 범위가 다르고 크기(Byte)가 다르다 ----
    // int: 가장 흔히 쓰는 정수형(32-bit)
    int i = 100;

    // long: 더 큰 정수(64-bit)
    long big = 3_000_000_000L; // L을 붙이면 long 리터럴

    // byte: 0~255(8-bit). 색상 채널(RGB), 바이너리 데이터 등에 사용
    byte b = 255;

    Console.WriteLine($"int  i   = {i}");
    Console.WriteLine($"long big = {big}");
    Console.WriteLine($"byte b   = {b}");

    // ---- 실수형: float(32-bit), double(64-bit) ----
    float f = 0.1f;      // f를 붙이면 float 리터럴
    double d = 0.1;      // 기본 실수 리터럴은 double

    // 실수는 "이진 부동소수점"이라서 0.1 같은 값을 정확히 표현 못 하는 경우가 많다.
    // 그래서 비교할 때는 == 보다 "오차 범위"로 비교하는 습관이 필요하다.
    Console.WriteLine($"float  f = {f}");
    Console.WriteLine($"double d = {d}");

    // ---- decimal: 금융/돈 계산 등 "정확성"이 중요한 곳에서 사용 ----
    // decimal은 10진 기반으로 설계되어 "돈"에 유리하지만, double보다 느릴 수 있다.
    decimal money = 12.34m; // m을 붙이면 decimal 리터럴
    Console.WriteLine($"decimal money = {money}");

    // 정수 나눗셈 vs 실수 나눗셈 차이
    int a = 7;
    int c = 2;
    Console.WriteLine($"7 / 2 (int)    = {a / c}  // 정수 나눗셈: 소수점 버림");
    Console.WriteLine($"7 / 2 (double) = {(double)a / c}  // 실수 나눗셈");
}

static void Demo_CharAndString()
{
    PrintSection("3) char와 string (문자/문자열)");

    // char: 문자 1개 (작은따옴표 사용)
    char grade = 'A';

    // string: 문자들의 묶음(문자열) (큰따옴표 사용)
    string msg = "Hello";

    Console.WriteLine($"char grade = {grade}");
    Console.WriteLine($"string msg = {msg}");

    // 문자열은 + 로 합칠 수 있지만, 많은 결합이 반복되면 성능 이슈가 생길 수 있다.
    // (그럴 땐 StringBuilder가 유리)
    string combined = msg + " C#";
    Console.WriteLine($"combined = {combined}");

    // 문자열 보간(Interpolation): 가장 읽기 쉬운 출력 방식 중 하나
    int level = 5;
    Console.WriteLine($"Player Level = {level}");
}

static void Demo_BoolAndComparison()
{
    PrintSection("4) bool과 비교 연산");

    bool isAdult = true;
    int age = 17;

    // 비교 연산 결과는 bool이다.
    bool canEnter = age >= 19;

    Console.WriteLine($"isAdult = {isAdult}");
    Console.WriteLine($"age >= 19 ? {canEnter}");

    // if 문에서 bool 사용
    if (canEnter)
        Console.WriteLine("입장 가능합니다.");
    else
        Console.WriteLine("입장 불가(미성년).");
}

static void Demo_ValueTypeVsReferenceType()
{
    PrintSection("5) 값 형식(Value Type) vs 참조 형식(Reference Type)");

    // 핵심 요약:
    // - 값 형식: 값 자체가 복사됨 (int, float, bool, struct 등)
    // - 참조 형식: "주소(참조)"가 복사됨 (class, string, array, List 등)
    //
    // 그래서 "복사했는데도 같이 바뀌는지?"가 큰 차이를 만든다.

    // ---- 값 형식 예: int ----
    int x = 10;
    int y = x;   // 값이 복사됨
    y = 999;

    Console.WriteLine($"값 형식: x={x}, y={y}  // y만 바뀜");

    // ---- 참조 형식 예: class ----
    Person p1 = new Person("Kim");
    Person p2 = p1;       // 참조(주소)가 복사됨: p1과 p2는 같은 객체를 가리킴
    p2.Name = "Park";     // p2로 바꿨지만 사실 같은 객체를 수정한 것

    Console.WriteLine($"참조 형식: p1.Name={p1.Name}, p2.Name={p2.Name}  // 둘 다 바뀜");

    // string은 참조 형식이지만 "불변(immutable)"이라서 수정이 아니라 새 문자열을 만든다.
    string s1 = "ABC";
    string s2 = s1;
    s2 = "XYZ"; // 새 문자열로 교체

    Console.WriteLine($"string(불변): s1={s1}, s2={s2}  // 따로 바뀜");
}

static void Demo_NullAndNullable()
{
    PrintSection("6) null과 Nullable(T?)");

    // null:
    // - "아무것도 가리키지 않는다"는 의미
    // - 기본적으로 참조 형식은 null이 될 수 있다.
    string? nickname = null;
    Console.WriteLine($"nickname = {nickname} (null일 수 있음)");

    // 값 형식(int)은 기본적으로 null이 될 수 없다.
    // 하지만 "Nullable<int>" 혹은 "int?" 를 쓰면 null 허용 가능.
    int? score = null;
    Console.WriteLine($"score = {score} (int? 는 null 가능)");

    // null 병합 연산자(??): null이면 기본값 사용
    int safeScore = score ?? 0;
    Console.WriteLine($"safeScore = {safeScore}  // score가 null이면 0");

    // null 조건 연산자(?.): null이면 멈추고 null을 반환
    // nickname?.Length 는 nickname이 null이면 예외를 내지 않고 null이 된다.
    int? len = nickname?.Length;
    Console.WriteLine($"nickname length = {len} (null 가능)");
}

static void Demo_TypeInference_var()
{
    PrintSection("7) var (타입 추론) - '자료형을 안 쓰는 것'이 아니다");

    // var는 "컴파일러가 오른쪽 값을 보고 자료형을 결정"한다.
    // 즉, 실행 중에 바뀌는 것이 아니라 "컴파일 타임"에 타입이 고정된다.
    var number = 123;         // int로 결정됨
    var text = "Hello";       // string으로 결정됨
    var pi = 3.14159;         // double로 결정됨

    Console.WriteLine($"number Type = {number.GetType().Name}");
    Console.WriteLine($"text   Type = {text.GetType().Name}");
    Console.WriteLine($"pi     Type = {pi.GetType().Name}");

    // var는 보통 타입이 명확할 때(우측이 new로 확실할 때) 가독성이 좋아진다.
    // 예) var list = new List<int>();
}

static void Demo_Conversions_Casting_Parse_TryParse()
{
    PrintSection("8) 형 변환: 캐스팅 / Convert / Parse / TryParse");

    // 1) 암시적 변환(Implicit)
    // 작은 범위 -> 큰 범위는 안전해서 자동 변환되는 경우가 많다.
    int i = 100;
    long l = i; // OK
    Console.WriteLine($"implicit: int({i}) -> long({l})");

    // 2) 명시적 변환(Explicit) = 캐스팅(Casting)
    // 큰 범위 -> 작은 범위는 데이터 손실 위험이 있어 직접 캐스팅해야 한다.
    long big = 1_000_000_000_000;
    int cut = (int)big; // 위험: 범위를 넘으면 값이 깨질 수 있다.
    Console.WriteLine($"explicit: long({big}) -> int({cut})  // 범위 주의");

    // 3) Convert
    // 다양한 타입 변환을 도와주는 유틸리티.
    double d = 12.7;
    int rounded = Convert.ToInt32(d); // 반올림 규칙 적용(12.7 -> 13)
    Console.WriteLine($"Convert.ToInt32(12.7) = {rounded}  // 반올림");

    // 4) Parse / TryParse (문자열 -> 숫자)
    string s = "250";
    int parsed = int.Parse(s);
    Console.WriteLine($"int.Parse(\"250\") = {parsed}");

    // Parse는 실패하면 예외(Exception)가 발생한다.
    // 그래서 사용자 입력처럼 "실패 가능"이 있으면 TryParse를 권장한다.
    string userInput = "12O"; // 일부러 O(알파벳) 섞어서 실패 유도
    bool ok = int.TryParse(userInput, out int result);
    Console.WriteLine($"int.TryParse(\"{userInput}\") 성공? {ok}, result={result}");

    // (참고) 문화권에 따라 소수점 표기 방식이 달라질 수 있다.
    // 강의에서는 보통 InvariantCulture를 함께 소개하면 실무 사고가 줄어든다.
    string dotNumber = "3.14";
    bool ok2 = double.TryParse(dotNumber, NumberStyles.Float, CultureInfo.InvariantCulture, out double pi);
    Console.WriteLine($"double.TryParse(\"{dotNumber}\") 성공? {ok2}, pi={pi}");
}

static void Demo_Overflow_Checked()
{
    PrintSection("9) 오버플로우(Overflow)와 checked");

    // int의 최대값을 넘어가면(오버플로우) 값이 '돌아가'서 이상해질 수 있다.
    int max = int.MaxValue;
    Console.WriteLine($"int.MaxValue = {max}");

    // unchecked(기본 동작)에서는 오버플로우가 조용히 발생할 수 있다.
    // (프로젝트/환경에 따라 다를 수 있지만, 원리를 보여주기 위한 예시)
    unchecked
    {
        int overflowed = max + 1;
        Console.WriteLine($"unchecked: MaxValue + 1 = {overflowed}  // 값이 이상해짐(오버플로우)");
    }

    // checked를 쓰면 오버플로우 시 예외를 던져서 문제를 빨리 발견할 수 있다.
    try
    {
        checked
        {
            int boom = max + 1;
            Console.WriteLine(boom); // 여기까지 보통 못 옴
        }
    }
    catch (OverflowException)
    {
        Console.WriteLine("checked: 오버플로우 발생! (OverflowException)");
    }
}

static void Demo_Enum()
{
    PrintSection("10) enum (열거형) - 의미를 가진 숫자");

    // enum은 내부적으로 정수값을 가지지만, "의미"를 부여해 가독성을 높인다.
    Day today = Day.Wed;

    Console.WriteLine($"today = {today}");
    Console.WriteLine($"today (int) = {(int)today}");

    // switch로 분기하면 코드가 읽기 좋아진다.
    switch (today)
    {
        case Day.Mon:
            Console.WriteLine("월요일");
            break;
        case Day.Wed:
            Console.WriteLine("수요일");
            break;
        default:
            Console.WriteLine("그 외 요일");
            break;
    }
}

static void Demo_Object_BoxingUnboxing()
{
    PrintSection("11) object, Boxing/Unboxing");

    // object는 "모든 타입의 최상위 부모"처럼 동작한다.
    // 값 형식(int)을 object에 넣으면 Boxing(박싱): 값이 힙 객체로 포장됨
    int n = 123;
    object boxed = n; // Boxing
    Console.WriteLine($"boxed = {boxed}, Type = {boxed.GetType().Name}");

    // 다시 int로 꺼내려면 Unboxing(언박싱)을 해야 한다.
    // 타입이 다르면 예외가 날 수 있으니 주의.
    int unboxed = (int)boxed;
    Console.WriteLine($"unboxed = {unboxed}");

    // 실무에서는 불필요한 박싱이 성능에 영향을 줄 수 있어, 제네릭(List<int>) 등을 선호한다.
}

static void Demo_ArraysAndList()
{
    PrintSection("12) 배열(Array)과 List<T>");

    // 배열: 크기가 고정
    int[] scores = new int[3];
    scores[0] = 10;
    scores[1] = 20;
    scores[2] = 30;

    Console.WriteLine($"scores.Length = {scores.Length}");
    Console.WriteLine($"scores[1] = {scores[1]}");

    // List<T>: 크기가 가변(늘었다 줄었다 가능)
    var list = new List<int>();
    list.Add(10);
    list.Add(20);
    list.Add(30);
    list.Add(40);

    Console.WriteLine($"list.Count = {list.Count}");
    Console.WriteLine($"list[3] = {list[3]}");

    // foreach: 컬렉션 순회에 자주 사용
    Console.Write("list items: ");
    foreach (var item in list)
        Console.Write(item + " ");
    Console.WriteLine();
}

static void Demo_DateTime()
{
    PrintSection("13) 자주 쓰는 내장 타입: DateTime");

    // DateTime: 날짜/시간 표현
    DateTime now = DateTime.Now;
    DateTime utc = DateTime.UtcNow;

    Console.WriteLine($"Now = {now}");
    Console.WriteLine($"UTC = {utc}");

    // TimeSpan: 시간 차이(기간)
    TimeSpan oneHour = TimeSpan.FromHours(1);
    Console.WriteLine($"Now + 1 hour = {now + oneHour}");
}


// =========================
// 도우미(출력 포맷)
// =========================

static void PrintTitle(string title)
{
    Console.WriteLine();
    Console.WriteLine("========================================");
    Console.WriteLine(title);
    Console.WriteLine("========================================");
}

static void PrintSection(string title)
{
    Console.WriteLine();
    Console.WriteLine("----------------------------------------");
    Console.WriteLine(title);
    Console.WriteLine("----------------------------------------");
}

// =========================
// 데모용 타입들
// =========================

class Person
{
    // 프로퍼티(Property): 클래스의 데이터를 외부에 공개하는 표준 방식
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }
}

enum Day
{
    Mon = 1,
    Tue = 2,
    Wed = 3,
    Thu = 4,
    Fri = 5,
    Sat = 6,
    Sun = 7
}
