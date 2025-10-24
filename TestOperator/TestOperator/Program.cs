using System;
using System.Runtime.CompilerServices;
using static System.Console;

namespace TestOperator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 5;
            int b = 10;
            int c = a + b;

            WriteLine($"{a} + {b} = {c}");

            a = 10;
            b = 5;
            c = a - b;

            WriteLine($"{a} - {b} = {c}");

            c = a * b;
            WriteLine($"{a} * {b} = {c}");

            c = a / b;
            WriteLine($"{a} / {b} = {c}");

            a = 11;
            b = 3;
            float d = (float)a / (float)b;
            WriteLine($"{a} / {b} = {d}");

            float e = 11.23f;
            float f = 3.1f;
            float g = e / f;

            WriteLine(g);

            // MathF.Round 
            // 가장 가까운 정수로 반올림
            WriteLine($"{e} / {f} = {MathF.Round(g)}");

            // MathF.Floor
            // 소수점 이하를 버리고 가장 가까운 작은 정수로 내림
            WriteLine($"{e} / {f} = {MathF.Floor(g)}");

            a = 15;
            b = 3;
            c = a % b;

            // 나머지 연산자
            // 11을 3으로 나눈 나머지 3 , 2
            WriteLine($"{a} % {b} = {c}");

            int item1 = 1001;
            int item2 = 1002;       

            WriteLine($"item1: {item1%1000}, item2: {item2%1000}");

            item1++;    // item1 = item1 + 1
            WriteLine(item1);

            item1--;    // item1 = item1 - 1
            WriteLine(item1);

            WriteLine();

            WriteLine(item2++); // 후위 연산자 1002 출력 후 1 증가

            WriteLine(item2);   // 1003 출력

            WriteLine(++item2); // 전위 연산자 1 증가 후 1004 출력

            WriteLine(item2);   // 1004 출력


            WriteLine();

            WriteLine(item2--); // 후위 연산자 

            WriteLine(item2);   // 

            WriteLine(--item2); // 전위 연산자 

            WriteLine(item2);   // 

            WriteLine();

            item1 += 10; // item1 = item1 + 10

            WriteLine(item1);

            item1 -= 5; // item1 = item1 - 5
            item1 *= 2; // item1 = item1 * 2
            item1 /= 3; // item1 = item1 / 3
            item1 %= 4; // item1 = item1 % 4

            WriteLine(item1);

            // 비교 연산자
            // ==, !=, >, <, >=, <=
            // 결과는 true 또는 false

            item1 = 1001;
            item2 = 1002;

            // == 같음을 비교
            WriteLine( item1 == 1001 );
            WriteLine(item1 == 2);

            // != 같지 않음을 비교
            WriteLine( item1 != 2 );

            // > 크다
            WriteLine($"{item1} < {item2} = {item1<item2}");
            WriteLine($"{item1} > {item2} = {item1>item2}");

            // <= 왼쪽이 오른쪽보다 작거나 같다
            // >= 왼쪽이 오른쪽보다 크거나 같다            
            WriteLine($"{item1} <= {item2} = {item1 <= item2}");
            WriteLine($"{item1} >= {item2} = {item1 >= item2}");

            WriteLine();

            // && 논리 AND : 둘 다 참이어야 참
            // || 논리 OR  : 둘 중 하나만 참이어도 참
            // ! 논리 NOT  : 부정, 참은 거짓으로, 거짓은 참으로
            bool condition1 = true;
            bool condition2 = false;

            WriteLine($"condition1 && condition1 : {condition1 && condition1}");
            WriteLine($"condition1 && condition2 : {condition1 && condition2}");

            WriteLine($"condition2 || condition2 : {condition2 || condition2}");
            WriteLine($"condition1 || condition2 : {condition1 || condition2}");

            WriteLine($"!condition1 : {!condition1}");
            WriteLine($"!condition2 : {!condition2}");


            WriteLine($"!(condition1 && condition2) : {!(condition1 && condition2)}");
            WriteLine($"!(condition1 || condition2) : {!(condition1 || condition2)}");

            WriteLine();

            // 비트 연산자
            // &, |, ^, ~, <<, >>

            int bitA = 3;  // 0000 0011
            int bitB = 5;  // 0000 0101

            WriteLine($"bitA & bitB : {bitA & bitB}"); // 0000 0001 => 1

            WriteLine($"bitA | bitB : {bitA | bitB}"); // 0000 0111 => 7

            // 1 bit : 공격 가능
            // 2 bit : 방어 가능
            // 3 bit : 아이템 사용 가능

            // 0111 : 7  (공격, 방어, 아이템 사용 가능)
            // 0010 : 2  (방어 가능)

            // ^  배타적 논리합
            // 둘 중 하나만 참일 때 참
            WriteLine($"bitA ^ bitB : {bitA ^ bitB}");  // 0000 0110 => 6

            // ~ 비트 부정 연산자
            // 각 비트를 반전
            WriteLine($"~bitA : {~bitA}"); // 1111 1100 => -4
            WriteLine($"~bitB : {~bitB}"); // 1111 1010 => -6

            // 비트 시프트 연산자
            // << 왼쪽 시프트
            // >> 오른쪽 시프트
            WriteLine($"bitA << 1 : {bitA << 1}"); // 0000 0110 => 6
            WriteLine($"bitB >> 1 : {bitB >> 1}"); // 0000 0010 => 2
        }
    }
}
