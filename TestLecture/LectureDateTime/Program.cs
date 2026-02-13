using System;
using static System.Console;

public class program
{
    public static void Main()
    {
        // DateTime : 날짜와 시간을 나타내는 구조체입니다.
        // Now : 현재 날짜와 시간을 반환하는 정적 속성입니다.
        DateTime now = DateTime.Now;
        Console.WriteLine($"Current Date and Time: {now}");
        Console.WriteLine($"Year: {now.Year}");
        Console.WriteLine($"Month: {now.Month}");
        Console.WriteLine($"Day: {now.Day}");
        Console.WriteLine($"Hour: {now.Hour}");
        Console.WriteLine($"Minute: {now.Minute}");
        Console.WriteLine($"Second: {now.Second}");

        // UtcNow : 현재 날짜와 시간을 UTC(협정 세계시)로 반환하는 정적 속성입니다.
        DateTime utcNow = DateTime.UtcNow;
        Console.WriteLine($"Current UTC Date and Time: {utcNow}");

        // 오늘 날짜
        DateTime today = DateTime.Today;
        Console.WriteLine($"Today's Date: {today}");

        // Min Time : DateTime 구조체가 표현할 수 있는 가장 이른 날짜와 시간을 나타내는 정적 속성입니다.
        DateTime minTime = DateTime.MinValue;
        Console.WriteLine($"Minimum Date and Time: {minTime}");

        // Max Time : DateTime 구조체가 표현할 수 있는 가장 늦은 날짜와 시간을 나타내는 정적 속성입니다.
        DateTime maxTime = DateTime.MaxValue;
        Console.WriteLine($"Maximum Date and Time: {maxTime}");

        // Custom Date and Time : 특정 날짜와 시간을 나타내는 DateTime 객체를 생성하는 방법입니다.
        DateTime dt = new DateTime(2024, 6, 1, 14, 30, 0);
        Console.WriteLine($"Custom Date and Time: {dt}");

        dt = dt.AddYears(1); // 날짜에 1년을 더하는 메서드입니다.
        dt = dt.AddMonths(1); // 날짜에 1개월을 더하는 메서드입니다.

        // AddDays : 날짜에 특정 일 수를 더하는 메서드입니다. 음수를 전달하면 날짜에서 일 수를 뺍니다.
        dt = dt.AddDays(7); // 날짜에 7일을 더하는 메서드입니다.
        Console.WriteLine($"Date and Time after adding 7 days: {dt}");

        // AddHours : 날짜에 특정 시간 수를 더하는 메서드입니다. 음수를 전달하면 날짜에서 시간 수를 뺍니다.
        dt = dt.AddHours(-2); // 날짜에서 2시간을 빼는 메서드입니다.
        Console.WriteLine($"Date and Time after subtracting 2 hours: {dt}");

        // AddMinutes : 날짜에 특정 분 수를 더하는 메서드입니다. 음수를 전달하면 날짜에서 분 수를 뺍니다.
        dt = dt.AddMinutes(15); // 날짜에 15분을 더하는 메서드입니다.
        Console.WriteLine($"Date and Time after adding 15 minutes: {dt}");

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

        ///////////////////////////////////////////
        ///

        DateTime dt1 = new DateTime(2024, 6, 1, 10, 0, 0);
        DateTime dt2 = new DateTime(2024, 6, 11, 15, 30, 0);

        TimeSpan difference = dt2 - dt1; // 두 날짜 간의 차이를 계산하는 연산자입니다.

        Console.WriteLine($"Difference between {dt2} and {dt1}:");
        Console.WriteLine($"Days: {difference.TotalDays}");
        Console.WriteLine($"Hours: {difference.TotalHours}");
        Console.WriteLine($"Minutes: {difference.TotalMinutes}");

        // Save , Load Ticks 을 사용해서 저장하거나 비교할 때 유용합니다.
        //difference.Ticks; // TimeSpan 구조체가 표현할 수 있는 가장 작은 시간 단위인 틱 단위로 차이를 반환합니다.
        //dt1.Ticks; // DateTime 구조체가 표현할 수 있는 가장 작은 시간 단위인 틱 단위로 날짜와 시간을 반환합니다.

        /////////////////////////////////////////////////
        ///

        /*        DateTime start = DateTime.Now;

                Console.WriteLine("아무 키나 누르세요");
                Console.ReadKey();

                DateTime end = DateTime.Now;

                TimeSpan elapsed = end - start;

                Console.WriteLine($"Elapsed Time: {elapsed.TotalSeconds} seconds");*/

        /////////////////////////////////////////////////
        ///

        Clear();
        Write("목표 날짜를 입력하세요 (예: 2024-12-31): ");
        string input = ReadLine();

        // TryParse : 문자열을 DateTime으로 변환하려고 시도하는 메서드입니다.
        // 변환에 성공하면 true를 반환하고, 실패하면 false를 반환합니다.
        // out 매개변수는 변환된 DateTime 값을 저장하는 데 사용됩니다.
        // 2024-12-31
        // 2024/12/31
        // December 31, 2024
        // 2030-12-31 23:59:59
        if (DateTime.TryParse(input, out DateTime targetDate))
        {
            WriteLine($"입력한 날짜: {targetDate}");

            DateTime currentDate = DateTime.Now;
            if (targetDate > currentDate)
            {
                TimeSpan timeRemaining = targetDate - currentDate;
                WriteLine($"남은 시간: {timeRemaining.Days}일 {timeRemaining.Hours}시간 {timeRemaining.Minutes}분 {timeRemaining.Seconds}초");
            }
            else
            {
                WriteLine("입력한 날짜는 현재 날짜보다 이전입니다.");
            }
        }
        else
        {
            WriteLine("유효한 날짜 형식이 아닙니다.");
        }
    }
}