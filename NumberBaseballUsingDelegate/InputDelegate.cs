using System;

public class InputDelegate
{
    public delegate int[] CreateQuestionDelegate();
    public CreateQuestionDelegate? CreateQuestionDelegateMethod = () =>
    {
        Random random = new Random();

        // 0~9 사이의 서로 다른 세 자리 숫자를 랜덤으로 생성
        int q1 = random.Next(0, 9);
        int q2 = random.Next(0, 9);
        int q3 = random.Next(0, 9);

        while (q2 == q1) q2 = random.Next(0, 9); // q2가 q1과 같으면 다시 생성
        while (q3 == q1 || q3 == q2) q3 = random.Next(0, 9); // q3가 q1 또는 q2와 같으면 다시 생성
        
        return new int[] { q1, q2, q3 }; // 생성된 숫자를 배열로 반환        
    };

    public Func<string, int[]>? SubmitAnswerActionMethod = inputAnswer =>
    {
        // 입력값이 null이거나 길이가 3이 아닌 경우 처리
        if (string.IsNullOrEmpty(inputAnswer) || inputAnswer.Length != 3)
        {
            Console.WriteLine("\n세 자리 숫자를 입력해야 합니다.");
            return Array.Empty<int>();
        }

        // 입력값이 숫자가 아니거나 범위를 벗어난 경우 처리
        if (!int.TryParse(inputAnswer, out int userInput) || userInput < 0 || userInput > 999)
        {
            Console.WriteLine("\n유효한 세 자리 숫자를 입력하세요.");
            return Array.Empty<int>();
        }

        return new int[]
        {
            userInput / 100, // 백의 자리
            (userInput / 10) % 10, // 십의 자리
            userInput % 10 // 일의 자리
        };
    };
}

