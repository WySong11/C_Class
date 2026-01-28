using System;
using static System.Console;
using System.Collections.Generic;

public class NumberBaseball
{
    static void Main(string[] args)
    {
        Random random = new Random();

        // 0~9 사이의 랜덤 숫자 생성
        int q1 = random.Next(0, 10);
        int q2 = random.Next(0, 10);
        int q3 = random.Next(0, 10);

        WriteLine($"Before: {q1}, {q2}, {q3}");

        while ((q1 == q2) || (q1 == q3) || (q2 == q3))
        {
            if (q1 == q2)
                q2 = random.Next(0, 10);
            else if (q1 == q3)
                q3 = random.Next(0, 10);
            else if (q2 == q3)
                q3 = random.Next(0, 10);
        }

        WriteLine($"After: {q1}, {q2}, {q3}");

        WriteLine("숫자 야구 게임에 오신 것을 환영합니다!");
        WriteLine("서로 다른 세 자리 숫자를 맞춰보세요.");
        WriteLine("비밀 숫자가 생성되었습니다. 시작하세요!");
        WriteLine("정답 : {0}{1}{2}", q1, q2, q3);

        WriteLine();

        List<int> answerDigits = new List<int> { q1, q2, q3 };

        int strike = 0;
        int ball = 0;

        do
        {
            Write("세 자리 숫자를 입력하세요: ");
            string? userInput = ReadLine();

            if (userInput == null || userInput.Length != 3 ||
                int.Parse(userInput) < 0 || int.Parse(userInput) > 999 ||
                !char.IsDigit(userInput[0]) ||
                !char.IsDigit(userInput[1]) ||
                !char.IsDigit(userInput[2]))
            {
                WriteLine("잘못된 입력입니다. 세 자리 숫자를 입력해주세요.");
                continue;
            }

            CompareDigits(answerDigits, userInput, out strike, out ball);

            WriteLine("{0} 스트라이크, {1} 볼", strike, ball);
            if (strike == 3)
            {
                WriteLine("축하합니다! 세 자리 숫자를 모두 맞추셨습니다!");
            }

        } while (strike < 3);

    }

    static void CompareDigits(List<int> answerDigits, string userInput, out int strike, out int ball)
    {
        strike = 0;
        ball = 0;

        //char myChar = (char)(n + '0');
        /*        int temp = int.Parse(userInput);

                List<int> userDigits = new List<int>
                {
                    temp / 100 % 10,
                    temp / 10 % 10,
                    temp % 10
                };

                for (int i = 0; i < 3; i++)
                {
                    if (userDigits[i] == answerDigits[i])
                    {
                        strike++;
                    }
                    else if (answerDigits.Contains(userDigits[i]))
                    {
                        ball++;
                    }
                }*/

        List<int> userDigits = new List<int>();

        for (int i=0; i<userInput.Length; i++)
        {
            userDigits.Add(userInput[i] - '0');
        }        

        for (int i = 0; i < 3; i++)
        {
            if (userDigits[i] == answerDigits[i])
            {
                strike++;
            }
            else if (answerDigits.Contains(userDigits[i]))
            {
                ball++;
            }
        }
    }
}