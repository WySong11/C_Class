using System;
using static System.Console;

public class LectureArray
{
    static void Main(string[] args)
    {
        int[] nums;

        int[] ints = new int[5] { 1, 2, 3, 4, 5 };

        int[] ints1 = { 1, 2, 3, 4, 5 };

        WriteLine("Lecture Array Start");
        // 배열 선언 및 초기화
        int[] numbers = new int[5]; // 길이가 5인 정수형 배열 생성
        numbers[0] = 10;
        numbers[1] = 20;
        numbers[2] = 30;
        numbers[3] = 40;
        numbers[4] = 50;
        // 배열 요소 출력
        for (int i = 0; i < numbers.Length; i++)
        {
            WriteLine($"numbers[{i}] = {numbers[i]}");
        }
        // 배열 초기화 시 값 지정
        string[] fruits = new string[] { "Apple", "Banana", "Cherry" };
        // foreach 문을 사용하여 배열 요소 출력
        foreach (string fruit in fruits)
        {
            WriteLine(fruit);
        }
        WriteLine("Lecture Array End");

        /////////////////////////////////////////////////////////////////////////////


    }

    static void UseGetLength()
    {
        int[,] numbers =
        {
            { 10, 20, 30 },
            { 40, 50, 60 }
        };

        for (int row = 0; row < numbers.GetLength(0); row++)
        {
            for (int column = 0; column < numbers.GetLength(1); column++)
            {
                Console.Write($"{numbers[row, column]} ");
            }

            Console.WriteLine();
        }
    }

    static void UseMultiDimensionalArray()
    {
        int[,] scores =
        {
            { 80, 90, 70 },
            { 90, 85, 95 },
            { 60, 75, 80 }
        };

        for (int student = 0; student < scores.GetLength(0); student++)
        {
            int total = 0;

            for (int subject = 0; subject < scores.GetLength(1); subject++)
            {
                total += scores[student, subject];
            }

            double average =
                (double)total / scores.GetLength(1);

            Console.WriteLine(
                $"학생 {student + 1} - 총점 : {total}, 평균 : {average:F1}");
        }

        foreach (int score in scores)
        {
            Console.Write($"{score} ");
        }
    }

    static void UseJaggedArray()
    {
        int[][] jaggedArray = new int[3][];
        jaggedArray[0] = new int[] { 1, 2, 3 };
        jaggedArray[1] = new int[] { 4, 5 };
        jaggedArray[2] = new int[] { 6, 7, 8, 9 };
        for (int i = 0; i < jaggedArray.Length; i++)
        {
            Console.Write($"Row {i}: ");
            for (int j = 0; j < jaggedArray[i].Length; j++)
            {
                Console.Write($"{jaggedArray[i][j]} ");
            }
            Console.WriteLine();
        }

        foreach (int[] row in jaggedArray)
        {
            Console.Write("Row: ");
            foreach (int num in row)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
        }
    }

    static void InputArray()
    {
        Console.WriteLine("Input Number : ");
        string? input = Console.ReadLine();

        int number = 0;

        if (int.TryParse(input, out number) == false)
        {
            Console.WriteLine("Invalid input. Please enter a valid integer.");
        }

        int[] array_num = new int[number];

        for (int i = 0; i < number; i++)
        {
            Console.WriteLine($"Input Number {i + 1} : ");
            string? input_num = Console.ReadLine();
            if (int.TryParse(input_num, out array_num[i]) == false)
            {
                Console.WriteLine("Invalid input. Please enter a valid integer.");
                i--; // Decrement i to repeat the current iteration
            }
            else
            {
                array_num[i] = int.Parse(input_num);
            }
        }

        foreach (int num in array_num)
        {
            Console.WriteLine($"Input Number : {num}");
        }
    }
}