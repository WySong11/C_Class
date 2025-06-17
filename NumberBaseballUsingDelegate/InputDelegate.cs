using System;

public class InputDelegate
{
    // CreateQuestionDelegateMethod 델리게이트는 int 배열을 반환하는 메서드
    public int[] CreateQuestionDelegateMethod()
    {
        Random random = new Random();

        // 0~9 사이의 서로 다른 세 자리 숫자를 랜덤으로 생성
        int q1 = random.Next(0, 9);
        int q2 = random.Next(0, 9);
        int q3 = random.Next(0, 9);

        while (q2 == q1) q2 = random.Next(0, 9); // q2가 q1과 같으면 다시 생성
        while (q3 == q1 || q3 == q2) q3 = random.Next(0, 9); // q3가 q1 또는 q2와 같으면 다시 생성
        
        return new int[] { q1, q2, q3 }; // 생성된 숫자를 배열로 반환        
    }

    // SubmitAnswerActionMethod 델리게이트는 string을 입력받아 int 배열을 반환하는 메서드
    public Func<string, int[]>? SubmitAnswerActionMethod;
}

