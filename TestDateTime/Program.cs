using System;

namespace TestDateTime
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 현재 날짜와 시간
            DateTime now = DateTime.Now;

            // 현재 날짜와 시간 (UTC)
            // UTC는 협정 세계시(Universal Coordinated Time)를 의미합니다.
            DateTime utcNow = DateTime.UtcNow;

            // 오늘 날짜 (시간은 00:00:00)
            DateTime today = DateTime.Today;

            // 2025년 6월 21일 00:00:00
            DateTime customDate = new DateTime(2025, 6, 21);

            // 2025년 6월 21일 14시 30분 0초
            DateTime customDateTime = new DateTime(2025, 6, 21, 14, 30, 0);

            int year = now.Year;
            int month = now.Month;
            int day = now.Day;
            int hour = now.Hour;
            int minute = now.Minute;
            int second = now.Second;
            DayOfWeek dayOfWeek = now.DayOfWeek; // 요일

            // AddDays : 현재 날짜에 일수를 더합니다.
            DateTime tomorrow = now.AddDays(1);
            DateTime yesterday = now.AddDays(-1);

            // AddHours, AddMinutes, AddSeconds : 현재 시간에 각각 시간, 분, 초를 더합니다.
            DateTime nextMonth = now.AddMonths(1);
            DateTime nextYear = now.AddYears(1);
            DateTime nextHour = now.AddHours(1);
            DateTime nextMinute = now.AddMinutes(1);
            DateTime nextSecond = now.AddSeconds(1);

            // DateTime 간의 차이 계산
            // 두 날짜 간의 차이를 구합니다.
            TimeSpan difference = tomorrow - now;

            // TimeSpan의 속성
            int days = difference.Days;          // 일수 차이
            int hours = difference.Hours;        // 시간 차이
            int minutes = difference.Minutes;    // 분 차이
            int seconds = difference.Seconds;    // 초 차이
            int totalDays = difference.Days;     // 전체 일수 차이
            int totalHours = (int)difference.TotalHours; // 전체 시간 차이
            int totalMinutes = (int)difference.TotalMinutes; // 전체 분 차이
            int totalSeconds = (int)difference.TotalSeconds; // 전체 초 차이

            // DateTime을 문자열로 변환
            string dateString = now.ToString(); // "2025-06-21 15:45:30"

            // DateTime을 문자열로 포맷팅
            string formatted = now.ToString("yyyy-MM-dd HH:mm:ss"); // 2025-06-21 14:30:00

            /*          포맷  문자열     의미                      예시(2025 - 06 - 21 15:45:30 기준)
                        "yyyy"          4자리 연도                  2025
                        "yy"            2자리 연도                  25
                        "MM"            2자리 월(01~12)	            06
                        "M"             월(1~12)                    6
                        "dd"            2자리 일(01~31)	            21
                        "d"             일(1~31)                    21
                        "HH"            24시간 형식 시(00~23)       15
                        "hh"            12시간 형식 시(01~12)       03
                        "mm"            분(00~59)                   45
                        "ss"            초(00~59)                   30
                        "tt"            오전 / 오후                 오후 또는 PM(문화권에 따라 다름)
                        "fff"           밀리초(000~999)             123
                        "dddd"          요일 이름(전체)             Saturday
                        "ddd"           요일 약어                   Sat
            */

            string a = now.ToString("yyyy-MM-dd");           // 2025-06-21
            string b = now.ToString("yyyy.MM.dd HH:mm:ss");  // 2025.06.21 15:45:30
            string c = now.ToString("MM/dd/yyyy hh:mm tt");  // 06/21/2025 03:45 PM
            string d = now.ToString("dddd, MMMM dd, yyyy");  // Saturday, June 21, 2025

            /*
            포맷 코드   설명                   예시
            "d"         짧은 날짜              6 / 21 / 2025
            "D"         긴 날짜                Saturday, June 21, 2025
            "t"         짧은 시간              3:45 PM
            "T"         긴 시간                3:45:30 PM
            "f"         긴 날짜 +짧은 시간     Saturday, June 21, 2025 3:45 PM
            "F"         긴 날짜 +긴 시간       Saturday, June 21, 2025 3:45:30 PM
            */

            string shortDate = now.ToString("d");  // 6/21/2025
            string longTime = now.ToString("T");   // 3:45:30 PM
        }
    }
}
