using System;
using static System.Console;

namespace TestDataType
{
    public class Program
    {
        static void Main(string[] args)
        {
            // 줄바꿈이 있습니다.
            // 다음 출력이 다른 줄에서 나옵니다.
            WriteLine("Hello, World!");

            // 1byte = 8bit
            // 11111111
            byte m_byte = 0;

            // 줄바꿈이 없이 출력합니다.
            // 다음 출력이 같은 줄에 이어서 나옵니다.
            Write(m_byte);

            // 줄바꿈
            WriteLine();

            // btye의 최소값 ~ 최대값
            WriteLine($"byte : {byte.MinValue} ~ {byte.MaxValue}");

            // 4byte = 32bit 0 111 1111 .... 1111 1111
            int m_int = 0;

            // int 의 최소값 ~ 최대값
            WriteLine($"int : {int.MinValue} ~ {int.MaxValue}");

            // -2147483648 ~ 2147483647
            int m_minInt = int.MinValue; // -2147483648
            int m_maxInt = int.MaxValue; // 2147483647

            WriteLine($"Min Int : {m_minInt - 1} ~ {m_maxInt + 1}");

            // unsigned integer
            uint m_uint = 0;

            // uint 의 최소값 ~ 최대값
            WriteLine($"uint : {uint.MinValue} ~ {uint.MaxValue}");

            // sbyte, byte, short, ushort, int, uint, long, ulong


            // 실수형. 소수점
            float m_float = 3.14159265359f;

            WriteLine($"{m_float}");
            WriteLine($"{m_float.ToString("0.00")}");
            WriteLine($"{m_float.ToString("n3")}");

            WriteLine($"float : {float.MinValue} ~ {float.MaxValue}");

            WriteLine($"double : {double.MinValue} ~ {double.MaxValue}");

            WriteLine($"decimal : {decimal.MinValue} ~ {decimal.MaxValue}");

            double m_value1 = 0.1 + 0.2;


            WriteLine("\n" + m_value1);

            // double 은 이진수로 저장됨
            if (m_value1 == 0.3)
            {
                WriteLine("m_value1 는 0.3과 같다");
            }
            else
            {
                WriteLine("m_value1 는 0.3과 같지 않다");
            }

            // 정교한 계산을 요할 때만 쓴다
            decimal m_value2 = 0.1m + 0.2m;

            WriteLine("\n" + m_value2);

            // 고정 소수점 방식으로 10진수로 저장됨
            if (m_value2 == 0.3m)
            {
                WriteLine("m_value2 는 0.3과 같다");
            }
            else
            {
                WriteLine("m_value2 는 0.3과 같지 않다");
            }

            // bool
            bool m_bool = false; // true , false 

            WriteLine(m_bool);

            m_bool = true;

            WriteLine(m_bool);

            if (m_bool)
            {
                WriteLine("bool is true");
            }
            else
            {
                WriteLine("bool is false");
            }

            // Char ' '
            char m_char = 'a';

            WriteLine(m_char);

            // string " "
            string m_string = "abcdefg";

            WriteLine(m_string);

            // var

            var m_var1 = 123;
            
            WriteLine(m_var1);

            var m_var2 = 3.1455;

            WriteLine(m_var2);

            var m_var3 = 'b';

            WriteLine(m_var3);

            var m_var4 = "xyz";

            WriteLine(m_var4);

            var m_var5 = true;

            WriteLine(m_var5);
        }                 
    }
}
