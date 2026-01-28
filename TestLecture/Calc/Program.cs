using System;
using static System.Console;
using System.Collections.Generic;

public class Program
{
    static void Main(string[] args)
    {
        List<string> recordList = new List<string>();

        // 반복문 : 조건이 만족할 때까지 반복 실행

        bool repeat = true;

        /*        while (repeat)
                {
                    // 첫 번째 숫자 입력 받기
                    Write("Input First Number : ");

                    // Nullable : 값 형식에 null 허용
                    // ? : Nullables
                    // ReadLine() : 콘솔에서 한 줄 입력 받기
                    string? stringA = ReadLine();

                    // int? : Nullable int
                    // stirng.IsNullOrEmpty() : 문자열이 null 이거나 빈 문자열인지 확인
                    // int.Parse() : 문자열을 정수로 변환
                    int numA = int.Parse(stringA);

                    //WriteLine($"stringA: {stringA}, numA: {numA}");

                    // 연산자 입력 받기
                    Write("Input Operation (+, -, *, /, %) : ");
                    string? operation = ReadLine();

                    // 두 번째 숫자 입력 받기
                    Write("Input Second Number : ");
                    string? stringB = ReadLine();

                    int numB = int.Parse(stringB);

                    // numA, numB, operation 중 하나라도 null 이면 오류 메시지 출력
                    // || : 논리 연산자 OR
                    if (string.IsNullOrEmpty(operation))
                    {
                        WriteLine("Invalid input. Please enter valid numbers and operation.");
                        return;
                    }

                    float result = 0f;

                    switch (operation)
                    {
                        case "+":
                            result = numA + numB;
                            break;
                        case "-":
                            result = numA - numB;
                            break;
                        case "*":
                            result = numA * numB;
                            break;
                        case "/":
                            // 0으로 나누기 방지
                            if (numB == 0)
                            {
                                WriteLine("Error: Division by zero is not allowed.");
                                return;
                            }
                            // 정수형 / 정수형 = 정수형
                            // float로 변환하여 실수형 나누기 수행
                            result = (float)numA / numB;
                            break;

                        case "%":
                            result = numA % numB;
                            break;

                        default:
                            WriteLine("Invalid operation. Please use +, -, *, or /.");
                            return;
                    }

                    WriteLine($"{numA} {operation} {numB} = {result}");


                    // 계속할지 여부 묻기
                    Write("\n계속하시겠습니까? (y/n) : ");
                    string? continueInput = ReadLine();

                    // string ToLower() => 소문자로 변환
                    // string ToUpper() => 대문자로 변환
                    // y 가 아니면 반복 종료
                    if (continueInput == null || continueInput.ToLower() != "y")
                    {
                        repeat = false;
                    }

                    WriteLine(); // 빈 줄 출력
                }*/

        // while 문은 조건이 먼저 평가됨
        // 시작할 때 조건을 만족하지 않으면 한 번도 실행되지 않을 수 있음

        // do ~ while 문 예제
        // 일단 한 번은 실행해야 할 때 사용
        // 조건이 나중에 평가됨
        do
        {
/*            // 첫 번째 숫자 입력 받기
            Write("Input First Number : ");

            // Nullable : 값 형식에 null 허용
            // ? : Nullables
            // ReadLine() : 콘솔에서 한 줄 입력 받기
            string? stringA = ReadLine();

            // int? : Nullable int
            // stirng.IsNullOrEmpty() : 문자열이 null 이거나 빈 문자열인지 확인
            // int.Parse() : 문자열을 정수로 변환
            int numA = int.Parse(stringA);

            //WriteLine($"stringA: {stringA}, numA: {numA}");

            // 연산자 입력 받기
            Write("Input Operation (+, -, *, /, %) : ");
            string? operation = ReadLine();

            // 두 번째 숫자 입력 받기
            Write("Input Second Number : ");
            string? stringB = ReadLine();

            int numB = int.Parse(stringB);

            // numA, numB, operation 중 하나라도 null 이면 오류 메시지 출력
            // || : 논리 연산자 OR
            if (string.IsNullOrEmpty(operation))
            {
                WriteLine("Invalid input. Please enter valid numbers and operation.");
                return;
            }*/

            int numA = 0;
            int numB = 0;
            string operation = "";

            InputOperation(ref numA, ref numB, ref operation);

            float result = 0f;

            switch (operation)
            {
                case "+":
                    result = numA + numB;
                    break;
                case "-":
                    result = numA - numB;
                    break;
                case "*":
                    result = numA * numB;
                    break;
                case "/":
                    // 0으로 나누기 방지
                    if (numB == 0)
                    {
                        WriteLine("Error: Division by zero is not allowed.");
                        return;
                    }
                    // 정수형 / 정수형 = 정수형
                    // float로 변환하여 실수형 나누기 수행
                    result = (float)numA / numB;
                    break;

                case "%":
                    result = numA % numB;
                    break;

                default:
                    WriteLine("Invalid operation. Please use +, -, *, or /.");
                    return;
            }

            string string_result = $"{numA} {operation} {numB} = {result}";

            WriteLine(string_result);

            recordList.Add(string_result);

            // 계속할지 여부 묻기
            Write("\n계속하시겠습니까? (y/n/l/d) : ");
            string? continueInput = ReadLine();

            // string ToLower() => 소문자로 변환
            // string ToUpper() => 대문자로 변환
            // y 가 아니면 반복 종료
            if (continueInput == null || continueInput.ToLower() != "y")
            {
                repeat = false;
            }

            switch(continueInput)
            {
                case "l":
                case "L":
                    PrintList(recordList);
                    repeat = true;
                    break;
                case "d":
                case "D":
                    Console.WriteLine("삭제할 항목의 인덱스를 입력하세요:");
                    string? indexInput = Console.ReadLine();

                    DeleteList(int.Parse(indexInput),ref recordList);

                    repeat = true;
                    break;
            }


            WriteLine(); // 빈 줄 출력

        } while (repeat); // 일단 한 번은 실행

        PrintList(recordList);
    }

    public static void PrintList(List<string> records)
    {
        foreach (var record in records)
        {
            WriteLine(record);
        }
    }

    public static void DeleteList(int index, ref List<string> records )
    {
        if( index < 0 || index >= records.Count)
        {
            WriteLine("Invalid index. No item deleted.");
            return;
        }

        // 특정 인덱스 항목 삭제
        records.RemoveAt(index);
    }

    public static void InputOperation(ref int a, ref int b, ref string op)
    {
        Write("Input Operation : ");
        string? operation = ReadLine();

        if (string.IsNullOrEmpty(operation))
        {
            WriteLine("Invalid input. Please enter valid operation.");
            return;
        }

        // split : 특정 구분자로 문자열을 나누어 배열로 반환
        string[] strings = operation.Split(' ');

        if( strings.Length != 3 )
        {
            WriteLine("Invalid input. Please enter in format: a + b");
            return;
        }

        // replace : 특정 문자열을 다른 문자열로 대체
        a = int.Parse(strings[0].Replace(",", ""));
        op = strings[1];
        b = int.Parse(strings[2].Replace(",", ""));
    }    
}