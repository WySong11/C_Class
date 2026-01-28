using System;
using System.Security.Cryptography.X509Certificates;
using static System.Console;

public class Program
{
    public enum DaysOfWeek
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    static void Main(string[] args)
    {
        int a = 103;

        // if 문을 사용하여 음수 입력 처리
        // true 
        // else : false

        // a > 0 양수 : WriteLine("a is positive.");
        // false 일 때만 else if ( a < 0 ) 
        // false 일 때만 else
        if ( a > 0 ) WriteLine("a is positive.");
        else if ( a < 0 ) WriteLine("a is negative.");
        else WriteLine("a is zero.");

        if (a > 0)
        {
            // 1. 0 보다 크면 여기서부터 실행
            WriteLine("a is positive.");
        }
        // a > 0 보다 크지 않으면 else if 문 실행
        else if (a < 0)
        {
            // 1. a < 0 보다 작으면 여기서부터 실행
            WriteLine("a is negative.");
        }
        // a > 0, a < 0 둘 다 아니면 else 문 실행
        else // if( a == 0)
        {
            // 1. a == 0 일 때 여기서부터 실행
            WriteLine("a is zero.");
        }

        // 2. a > 0 보다 크면 여기서부터 실행


        if ( a >= 0)
        {
            if( a > 50 )
            {
                WriteLine("a is greater than 50.");
            }
            else if( a < 30)
            {
                WriteLine("a is less than 30.");
            }
        }
        else
        {
            if( a < -50)
            {
                WriteLine("a is less than -50.");
            }
            else
            {
                WriteLine("a is greater than -50.");
            }

            if (a > -30)
            {
                if( a < -10)
                {
                    WriteLine("a is between -30 and -10.");
                }
            }
        }

        int b = -10;

        // and 연산자 (&&)
        // a > 0 : true
        // b > 0 : false
        // a > 0 && b > 0 : false
        if ( a > 0 && b > 0)
        {
            WriteLine("Both a and b are positive.");
        }

        // or 연산자 (||)
        // a > 0 : true
        // b > 0 : false
        // a > 0 || b > 0 : true
        if ( a > 0 || b > 0)
        {
            WriteLine("At least one of a or b is positive.");
        }

        // not 연산자 ( ! )
        // a > 0 : true
        // b > 0 : false
        // !( b > 0 ) : true
        // a > 0 && !( b > 0 ) : true
        if ( a > 0 &&  !( b > 0 ) )
        {
            WriteLine("a is positive and b is not positive.");
        }

        int c = 0;

        // a > 0 : true
        // b > 0 : false
        // c > 0 : false
        // a > 0 && b > 0 : false
        // a > 0 && b > 0 || c > 0 : false
        if ( a > 0 && b > 0 || c > 0)
        {
            WriteLine("a, b, and c are all positive.");
        }

        // 괄호를 사용하여 연산자 우선순위 지정
        if (a > 0 && ( b > 0 || c > 0) )
        {
            WriteLine("a, b, and c are all positive.");
        }

        /////////////////////////////////////////////////////
        /// Switch 문
        /// 

        int day = 3;

        switch(day)
        {
            case 1:
                WriteLine("Monday");
                break;
            case 2:
                WriteLine("Tuesday");
                break;
            case 3:
                WriteLine("Wednesday");
                break;
            case 4:
                WriteLine("Thursday");
                break;
            case 5:
                WriteLine("Friday");
                break;
            case 6:
                WriteLine("Saturday");
                break;
            case 7:
                WriteLine("Sunday");
                break;
            default:
                WriteLine("Invalid day.");
                break;
        }

        DayOfWeek today = DayOfWeek.Wednesday;

        switch (today)
        {
            case DayOfWeek.Monday:
                WriteLine("It's Monday!");
                break;
            case DayOfWeek.Tuesday:
                WriteLine("It's Tuesday!");
                break;

/*            case DayOfWeek.Wednesday:
                WriteLine("It's Wednesday!");
                break;*/

            case DayOfWeek.Thursday:
                WriteLine("It's Thursday!");
                break;

            case DayOfWeek.Friday:
                WriteLine("It's Friday!");
                break;

            // 여러 case 문을 하나로 묶기
            case DayOfWeek.Saturday:
            case DayOfWeek.Sunday:
                WriteLine("It's Hollyday!");
                break;

            // default 문은 선택 사항. 없어도 됨
            // 모든 case 문에 해당하지 않을 때 실행
            default:
                WriteLine("Invalid day.");
                break;
        }
    }
}