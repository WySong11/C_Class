using System;
using static System.Console;

namespace TestControl
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("숫자를 입려하세요 : ");
            string ans = Console.ReadLine();

            Console.WriteLine("입력한 숫자는 : " + ans );

            Console.WriteLine("숫자를 입력하세요 : ");
            string ans2 = Console.ReadLine();

            Console.WriteLine("입력한 숫자는 : " + ans2);


            // 문자열을 정수형으로 변환
            int num1 = int.Parse(ans);
            int num2 = int.Parse(ans2);

            Console.WriteLine($"{num1} + {num2} = {num1+num2}");

            // 실수형으로 변환
            float f1 = float.Parse(ans);
            float f2 = float.Parse(ans2);

            Console.WriteLine($"{f1} + {f2} = {f1 + f2}");


            WriteLine();

            int a = 5;
            int b = 10;

            // if
            if (a >= 5)
            {
                // True 일 때
                WriteLine("a is equal to 5");
            }
            else
            {
                // False 일 때
                WriteLine("a is not equal to 5");
            }

            if (b == 10)
            {
                WriteLine("b is equal to 10");

                WriteLine("b is equal to 10");

                if (a == 5)
                {
                    WriteLine("a is equal to 5");

                    if (a + b == 15)
                    {
                        WriteLine("a + b is equal to 15");
                    }
                    else
                    {
                        WriteLine("a + b is not equal to 15");
                    }
                }
                else
                {
                    WriteLine("a is not equal to 5");
                }
            }

            int c = 15;

            if ( b == 5 ) WriteLine("b is equal to 5");

            // and 
            // b == 10 true
            // a == 5 true
            // ( true && true ) => true 
            // c != 15 false
            // ( true && false ) => false
            // ( true || false ) => true

            else if ( ( b == 10 && a == 5) && c != 15 ) WriteLine("b is equal to 10");

            // or
            else if( b == 10 || a == 5 )
            {

            }
            else if (b == 15) WriteLine("b is equal to 15");

            else
            {
                switch(b)
                {
                    case 5:
                        WriteLine("b is 5");
                        break;
                    case 10:
                        WriteLine("b is 10");
                        break;
                    default:
                        WriteLine("b is not 5 or 10");
                        break;
                }

                WriteLine("???");
            }

            if( a == "+" )
            {
                WriteLine("a is +");
            }
            else if( a == "-" )
            {
                WriteLine("a is -");
            }
            else if( a == 1 )
            {
                WriteLine("a is 1");
            }
            else if( a == 2 )
            {
                WriteLine("a is 2");
            }
            else if( a == 3 )
            {
                WriteLine("a is 3");
            }
            else if( a == 4 )
            {
                WriteLine("a is 4");
            }
            else if( a == 5 )
            {
                WriteLine("a is 5");
            }
            else
            {
                WriteLine("a is not between 1 and 5");
            })


            // 맞는 값 찾기
            // case 문에서 맞는 값을 찾는 과정
            switch (a)
            {
                case "+":
                    WriteLine("a is +");
                    break;

                case "-":

                case 1:

                    if(a == 1)
                    {
                        WriteLine("a is 1");
                    }

                    WriteLine("a is 1");
                    break;

                case 2:
                    WriteLine("a is 2");
                    break;

                case 3:
                    WriteLine("a is 3");
                    break;
                case 4:
                    WriteLine("a is 4");
                    break;
                case 5:
                    WriteLine("a is 5");
                    break;

                default:
                    WriteLine("a is not between 1 and 5");
                    break;
            }
        }
    }
}
