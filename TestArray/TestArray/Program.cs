using System;
using static System.Console;

namespace TestArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 1, 3, 6, 4, 5 };

            PrintArray(numbers);

            WriteLine(numbers[1]);

            WriteLine(new string('-', 5));

            WriteLine(numbers[2]);

            numbers[2] = 10;

            WriteLine(numbers[2]);

            WriteLine(new string('-', 5));

            int index = 6;

            // 중요~!!! : 배열의 인덱스가 범위를 벗어나는지 확인하는 코드
            if (index < numbers.Length)
            {
                WriteLine(numbers[index]);
            }
            else
            {
                WriteLine("Index out of bounds");
            }

            // 정렬
            Array.Sort(numbers);

            PrintArray(numbers);

            // 역순
            Array.Reverse(numbers);

            PrintArray(numbers);

            // 복사
            int[] another = new int[10];
            Array.Copy(numbers, another, numbers.Length);

            Array.Clear(numbers, 0, numbers.Length);

            PrintArray(numbers);

            Array.Clear(another, 0, 3);

            PrintArray(another);
        }

        static void PrintArray(int[] arr)
        {
            WriteLine(new string('-', 5));

            foreach (var item in arr)
            {
                WriteLine(item);
            }

            WriteLine(new string('-', 5));
        }
    }
}
