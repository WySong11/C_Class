// This code is a simple console application that implements a number guessing game similar to "Baseball".

using System;
using static System.Console;

internal class NumberBaseBall
{
    private static void Main(string[] args)
    {
        WriteLine("I want to play a game~!!\n");

        // 랜덤 클래스 오브젝트 선언
        Random random = new Random();

        // 0~9 사이의 서로 다른 세 자리 숫자를 랜덤으로 생성
        int q1 = random.Next(0, 9);
        int q2 = random.Next(0, 9);
        int q3 = random.Next(0, 9);

        // q1, q2, q3가 서로 다른 숫자가 될 때까지 반복
        while (((q1 != q2) && (q2 != q3) && (q1 != q3)) == false)
        {
            // q1 , q2 가 다른 숫자가 되도록 재설정
            if (q1 == q2)
            {
                q2 = random.Next(0, 9);
            }
            // q2 , q3 가 다른 숫자가 되도록 재설정 
            else if (q2 == q3)
            {
                q3 = random.Next(0, 9);
            }
            // q1 , q3 가 다른 숫자가 되도록 재설정
            else if (q1 == q3)
            {
                q3 = random.Next(0, 9);
            }
        }

        WriteLine($"정답 : {q1} , {q2} , {q3}");

        // 정답 숫자를 배열에 저장
        int[] answerDigits = new int[3] { q1, q2, q3 };

        // 스트라이크와 볼을 저장할 변수 선언
        int strike = 0;
        int ball = 0;
        bool result = false;

        // 사용자 입력을 받아서 스트라이크와 볼을 계산하는 반복문
        do
        {
            WriteLine("\n세 자리 숫자를 입력하세요: ");
            string? answer = Console.ReadLine();

            // 입력값이 null이거나 길이가 3이 아닌 경우 처리
            if (string.IsNullOrEmpty(answer) || answer.Length != 3)
            {
                WriteLine("\n세 자리 숫자를 입력해야 합니다.");
                continue;
            }

            // 입력값이 숫자가 아니거나 범위를 벗어난 경우 처리
            if (!int.TryParse(answer, out int userInput) || userInput < 0 || userInput > 999)
            {
                WriteLine("\n유효한 세 자리 숫자를 입력하세요.");
                continue;
            }

            // 입력값 검증
            CompareDigits(answerDigits, userInput, out strike, out ball);

            // 입력값이 정답과 일치하는지 확인
            //bool result = ProduceResult(answerDigits, userInput, out strike, out ball);

            WriteLine($"\n{userInput} : {strike} Strike, {ball} Ball");

        } while (strike < 3);

        WriteLine("\nStrike Out~!!! Game Over.");
    }

    // 숫자를 비교하여 스트라이크와 볼을 계산하는 메서드
    /*
     * answerDigits: 정답 숫자를 담고 있는 배열
     * userInput: 사용자가 입력한 숫자
     * strike: 스트라이크의 개수 (출력 파라미터)
     * ball: 볼의 개수 (출력 파라미터)
     */
    public static void CompareDigits(int[] answerDigits, int userInput, out int strike, out int ball)
    {
        // 스트라이크와 볼 초기화
        strike = 0;
        ball = 0;

        // 사용자가 입력한 숫자를 세 자리로 분리
        int[] userDigits = new int[3]
        {
            userInput / 100 % 10,
            userInput / 10 % 10,
            userInput % 10
        };

        // 스트라이크와 볼 계산
        for (int i = 0; i < 3; i++)
        {
            // 같은 자리의 숫자가 일치하면 스트라이크 증가
            if (userDigits[i] == answerDigits[i])
            {
                strike++;
            }
            // 다른 자리의 숫자가 정답에 존재하면 볼 증가
            else if (Array.Exists(answerDigits, element => element == userDigits[i]))
            {
                ball++;
            }
        }
    }

    // 숫자를 비교하여 스트라이크와 볼을 계산하고 결과를 반환하는 메서드
    /*
     * answerDigits: 정답 숫자를 담고 있는 배열
     * userInput: 사용자가 입력한 숫자
     * strike: 스트라이크의 개수 (출력 파라미터)
     * ball: 볼의 개수 (출력 파라미터)
     * strike가 3이면 true, 아니면 false를 반환
     */
    public static bool ProduceResult(int[] answerDigits, int userInput, out int strike, out int ball)
    {
        // 스트라이크와 볼 초기화
        strike = 0;
        ball = 0;

        // 사용자가 입력한 숫자를 세 자리로 분리
        int[] userDigits = new int[3]
        {
            userInput / 100 % 10,
            userInput / 10 % 10,
            userInput % 10
        };

        // 스트라이크와 볼 계산
        for (int i = 0; i < 3; i++)
        {
            // 같은 자리의 숫자가 일치하면 스트라이크 증가
            if (userDigits[i] == answerDigits[i])
            {
                strike++;
            }
            // 다른 자리의 숫자가 정답에 존재하면 볼 증가
            else if (Array.Exists(answerDigits, element => element == userDigits[i]))
            {
                ball++;
            }
        }

        return strike == 3 ? true : false;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////
    /// String 사용 버전
    /// 

    public static void CompareDigits(string answer, string user, out int strike, out int ball)
    {
        strike = 0;
        ball = 0;

        // strike: 같은 인덱스(자리)에서 같은 문자면 증가
        for (int i = 0; i < 3; i++)
        {
            if (user[i] == answer[i])
                strike++;
        }

        // ball: 자리는 다르지만(answer[i]와는 다름), 정답 문자열 안에 존재하면 증가
        for (int i = 0; i < 3; i++)
        {
            if (user[i] != answer[i] && answer.Contains(user[i]))
                ball++;
        }
    }

    public static bool ProduceResult(string answer, string user, out int strike, out int ball)
    {
        CompareDigits(answer, user, out strike, out ball);
        return strike == 3;
    }

    /*
     // 정답을 배열 대신 문자열로 저장
string answerStr = $"{q1}{q2}{q3}";
WriteLine($"정답 : {answerStr}");

int strike = 0;
int ball = 0;

do
{
    WriteLine("\n세 자리 숫자를 입력하세요: ");
    string? userStr = Console.ReadLine();

    if (string.IsNullOrEmpty(userStr) || userStr.Length != 3)
    {
        WriteLine("\n세 자리 숫자를 입력해야 합니다.");
        continue;
    }

    // 문자열이 숫자 3개인지 검사 (int.TryParse 대신)
    if (!char.IsDigit(userStr[0]) || !char.IsDigit(userStr[1]) || !char.IsDigit(userStr[2]))
    {
        WriteLine("\n유효한 세 자리 숫자를 입력하세요.");
        continue;
    }

    // 문자열 비교 버전 호출
    CompareDigits(answerStr, userStr, out strike, out ball);

    WriteLine($"\n{userStr} : {strike} Strike, {ball} Ball");

} while (strike < 3);

WriteLine("\nStrike Out~!!! Game Over.");
    */

}