using System;

public class OutpuDelegate
{
    public Comparison<int[]>? CompareQuestionAndAnswer = (question, answer) =>
    {
        // 스트라이크와 볼을 계산하는 로직
        int strikeCount = 0;
        int ballCount = 0;
        for (int i = 0; i < question.Length; i++)
        {
            if (question[i] == answer[i])
            {
                strikeCount++;
            }
            else if (Array.Exists(answer, x => x == question[i]))
            {
                ballCount++;
            }
        }
        // 결과 출력
        Console.WriteLine($"\n{strikeCount} 스트라이크, {ballCount} 볼\n");

        // 게임 종료 조건: 스트라이크가 3개인 경우
        return strikeCount == 3 ? 1 : 0;
    };
}