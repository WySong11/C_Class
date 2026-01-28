using System;

public class Program
{
    static void Main(string[] args)
    {
        string name = "Alice";
        string tt = "is";

        // 연결 연산자 사용
        name += tt;

        string message = name + tt;

        // 작은 따옴표는 문자 하나를 나타냄
        // char는 단일 문자 타입
        char a = 'a';
        // string은 큰 따옴표를 사용
        string b = "a";

        char[] letters = { 'a', 'b', 'c' };

        for (int i = 0; i < letters.Length; i++)
        {
            Console.Write(letters[i]);
        }

        Console.WriteLine();

        string c = "abc";
        for (int i = 0; i < c.Length; i++)
        {
            Console.Write(c[i]);
        }

        string s1 = null;   // 문자열이 없음
        string s2 = "";     // 빈 문자열

        Console.WriteLine();

        for (int i = 0; i < 5; i++)
        {
            int spaces = 5 - i;
            int stars = i * 2 + 1;

            Console.Write(new string(' ', spaces));
            Console.Write(new string('*', stars));
            Console.WriteLine();
        }

        // join : 문자열 배열을 특정 구분자로 연결
        string join = string.Join("-", new string[] { "2024", "06", "12" });
        Console.WriteLine(join);

        // split : 문자열을 특정 구분자로 나누어 배열로 반환
        // 예: "2024-06-12" -> ["2024", "06", "12"]
        string[] split = join.Split('-');

        foreach (var item in split)
        {
            Console.WriteLine(item);
        }

        // indexOf : 특정 문자열이 처음으로 나타나는 위치 반환
        int index = join.IndexOf("06");
        Console.WriteLine(index);

        // substring : 특정 위치부터 문자열을 잘라서 반환. 0부터 시작
        string substring = join.Substring(5, 2); // "06"

        // toLower, toUpper : 문자열을 소문자/대문자로 변환
        string upper = "hello world".ToUpper(); // "HELLO WORLD"
        string lower = "HELLO WORLD".ToLower(); // "hello world"

        // trim : 문자열의 앞뒤 공백 제거
        string padded = "   hello   ";
        string trimmed = padded.Trim(); // "hello"

        // replace : 특정 문자열을 다른 문자열로 대체
        string replaced = join.Replace("-", "/"); // "2024/06/12"

        string testA = "TestA";
        string testB = "TestA";

        // == 연산자 사용
        if (testA == testB)
        {
            Console.WriteLine("Equal");
        }
        else
        {
            Console.WriteLine("Not Equal");
        }
        
        // Equals 메서드 사용
        if (testA.Equals(testB))
        {
            Console.WriteLine("Equal");
        }
        else
        {
            Console.WriteLine("Not Equal");
        }

        // StringComparison 옵션 사용
        // 대소문자 구분 없이 비교
        // 옵션 종류
        // CurrentCulture : 현재 문화권의 규칙을 따름
        // CurrentCultureIgnoreCase : 현재 문화권의 대소문자 규칙을 따름
        // InvariantCulture : 문화권에 상관없이 규칙을 따름
        // InvariantCultureIgnoreCase : 문화권에 상관없이 대소문자 규칙을 따름
        // Ordinal : 이진 비교 (대소문자 구분)
        // OrdinalIgnoreCase : 이진 비교에서 대소문자 구분 안함

        string testC = "testa";

        if (testA.Equals(testC, StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Equal");
        }
        else
        {
            Console.WriteLine("Not Equal");
        }

        string ui = $"User Interface : {testA}";
        string ui1 = "User Interface : " + testA;
        //string ui4 = "User Interface : {0}", testA; // 오류

        string ui2 = string.Format("User Interface : {0}", testA);
        string ui3 = string.Format($"User Interface : {testA}");

        // \n : 줄바꿈
        string line = "ABC\nDEF";
        Console.WriteLine(line);

        // \\ : 역슬래시
        string path = "C:\\Program Files\\MyApp";
        Console.WriteLine(path);

        // \t : 탭
        string tabbed = "Column1\tColumn2\tColumn3";
        Console.WriteLine(tabbed);

        // \" : 큰 따옴표
        string quoted = "He said, \"Hello!\"";
        Console.WriteLine(quoted);

        // @ : 문자열 내의 이스케이프 시퀀스를 무시
        string verbatimPath = @"C:\Program Files\MyApp";
        Console.WriteLine(verbatimPath);

        string s = "   ";

        // IsNullOrEmpty : 문자열이 null이거나 빈 문자열인지 확인
        if (string.IsNullOrEmpty(s))
        {
            Console.WriteLine("String is null or empty");
        }

        // IsNullOrWhiteSpace : 문자열이 null이거나 공백 문자로만 이루어졌는지 확인
        if (string.IsNullOrWhiteSpace(s))
        {
            Console.WriteLine("String is null or whitespace");
        }

        // C# 12.0부터는 null 패턴 매칭과 함께 사용 가능
        if (s == null || string.IsNullOrWhiteSpace(s))
        {
            Console.WriteLine("String is null or whitespace");
        }

        int x1 = 10;
        // 형변환 메서드 사용
        string x2 = x1.ToString(); // "10"

        float y1 = 3.14f;
        string y2 = y1.ToString(); // "3.14"


        string z1 = "123";
        int z2 = int.Parse(z1); // 123

        string w1 = "3.14";
        float w2 = float.Parse(w1); // 3.14

        // TryParse 메서드 사용
        // 문자열을 숫자로 안전하게 변환
        // 변환에 실패해도 예외가 발생하지 않음
        // 성공하면 true, 실패하면 false 반환
        if ( int.TryParse("123", out int result))
        {
            Console.WriteLine($"Parsing succeeded: {result}");
        }
        else
        {
            Console.WriteLine("Parsing failed");
        }

        if( float.TryParse("3.14", out float fResult))
        {
            Console.WriteLine($"Parsing succeeded: {fResult}");
        }
        else
        {
            Console.WriteLine("Parsing failed");
        }

        // 출력 형식 지정

        double pi = 3.14159265359;
        Console.WriteLine(pi.ToString("F2")); // 소수점 둘째 자리까지: "3.14"
        Console.WriteLine(pi.ToString("F4")); // 소수점 넷째 자리까지: "3.14"

        Console.WriteLine(pi.ToString("E2")); // 지수 표기법: "3.14E+00"

        Console.WriteLine($"{pi:F3}"); // 보간 문자열 사용: "3.142"
        Console.WriteLine($"{pi:0.0000}"); // 사용자 지정 형식 사용: "3.1416"
        Console.WriteLine($"{pi:#.###}"); // 사용자 지정 형식 사용: "3.142"

        pi = 3.14001;
        Console.WriteLine($"{pi:#.###########################}"); // 사용자 지정 형식 사용: "3.14159265359" # 은 있는 자리만 표시

        int money = 123456789;
        Console.WriteLine(money.ToString("N0")); // 천 단위 구분기호 포함: "123,456,789"
        Console.WriteLine(money.ToString("N2")); // 천 단위 구분기호 포함 및 소수점 둘째 자리까지: "123,456,789.00"

        Console.WriteLine(money.ToString("C"));   // 통화 형식: "$123,456,789.00" (지역 설정에 따라 다름)

        float percent = 0.256f;
        Console.WriteLine(percent.ToString("P1")); // 백분율 형식: "25.6 %"

        percent = 0.25678f;
        Console.WriteLine(percent.ToString("P3")); // 백분율 형식: "25.678 %"


        // 시스템 날짜와 시간 가져오기
        // DateTime.Now : 현재 날짜와 시간
        DateTime now = DateTime.Now;

        // 다양한 형식으로 출력
        // yyyy : 4자리 연도
        // yy : 2자리 연도
        // MM : 2자리 월
        // dd : 2자리 일
        // MMMM : 월 이름
        // dddd : 요일 이름
        // MM : 2자리 월
        // dd : 2자리 일
        // HH : 24시간제 시
        // hh : 12시간제 시
        // mm : 분
        // ss : 초
        // fff : 밀리초
        Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss")); // "2024-06-12 14:30:45"
        Console.WriteLine(now.ToString("dddd, MMMM dd, yyyy")); // "Wednesday, June 12, 2024"
        Console.WriteLine(now.ToString("MM/dd/yyyy")); // "06/12/2024"
        Console.WriteLine(now.ToString("hh:mm tt")); // "02:30 PM"
        Console.WriteLine(now.ToString("yyyy-MM-ddTHH:mm:ss.fff")); // "2024-06-12T14:30:45.123"

    }
}
