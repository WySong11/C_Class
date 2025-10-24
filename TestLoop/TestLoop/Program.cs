using System;
using System.Runtime.InteropServices;
using static System.Console;

namespace TestLoop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // 반복문  
            // break : 반복문 종료
            // continue : 다음 반복으로 넘어감

            // for문
            // 첫번째 부분: 초기화 (int i = 0)
            // 두번째 부분: 조건식 (i < 5)
            // 세번째 부분: 증감식 (i++) i = i + 1 , i += 1

            // i = 0 -> i++ -> i = 1 -> i < 5 참 -> 반복
            // i = 5 -> i < 5 거짓 -> 종료
            for (int i = 0; i < 5; i++)
            {
                // i가 1일 때는 출력하지 않고 다음 반복으로 넘어감
                if (i == 1)
                {
                    continue;
                }

                // i가 3일 때 반복문 종료   
                if (i == 3)
                {
                    break;
                }

                WriteLine($"for loop iteration: {i}");
            }

            WriteLine();


            // while문
            int j = 0;
            while (j < 5)
            {
                if (j == 1)
                {
                    j++;
                    continue;
                }

                if (j == 3)
                {
                    break;
                }

                WriteLine($"while loop iteration: {j}");

                j++;
            }

            WriteLine();

            // do ~ while문
            int k = 0;

            do
            {
                if (k == 1)
                {
                    k++;
                    continue;
                }
                if (k == 3)
                {
                    break;
                }
                WriteLine($"do while loop iteration: {k}");
                k++;

            } while (k < 5);

            WriteLine();

            // foreach문

            string[] fruits = { "Apple", "Banana", "Cherry", "Date", "Elderberry" };

            int[] numberlist = { 1, 2, 3, 4, 5 , 6, 7 };
            // 배열, 컬렉션의 요소를 하나씩 꺼내서 반복
            // numberlist.length : 배열의 길이(요소의 개수)
            // numberlist.length = 5

            for (int index = 0; index < numberlist.Length; index++)
            {
                // numberlist[index] : index 위치의 요소
                // numberlist[0] : 1
                // numberlist[1] : 2
                // numberlist[4] : 5

                WriteLine($"for loop fruit: {numberlist[index]}");
            }

            WriteLine();

            // foreach문
            // 배열, 컬렉션의 요소를 하나씩 꺼내서 반복
            // 배열, 컬렉션의 길이 만큼 자동으로 반복
            // numberlist.length = 5
            // var : numberlist 배열, 컬렉션의 요소 타입을 자동으로 인식
            foreach (var fruit in numberlist)
            {
                WriteLine($"foreach loop fruit: {fruit}");
            }

            WriteLine();

            foreach (var fruit in fruits)
            {
                WriteLine($"foreach loop fruit: {fruit}");
            }
        }
    }
}
