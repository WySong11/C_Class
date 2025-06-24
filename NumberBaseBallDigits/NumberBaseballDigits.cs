using System;
using System.Collections.Generic;
using System.Linq;

public class NumberBaseballDigits
{
    public void StartGame()
    {
        Console.Clear();
        Console.WriteLine("숫자 야구 자리수 입력하세요 (1~10): ");
        string? input = Console.ReadLine();

        if (int.TryParse(input, out int digits) && digits >= 1 && digits <= 10)
        {
            Console.WriteLine($"게임 시작! 자리수: {digits}");
            // 게임 로직을 여기에 추가하세요.
        }
        else
        {
            Console.WriteLine("잘못된 입력입니다. 1에서 10 사이의 숫자를 입력하세요.");
        }
        Console.WriteLine();

        // 게임 시작 시간과 종료 시간을 기록하기 위한 TimeRecorder 클래스 사용
        TimeRecorder.SetStartTimeDelegate(StartTimeEventListner1);
        TimeRecorder.SetEndTimeDelegate(EndTimeEventListner1);
        
        TimeRecorder.SetStartTimeDelegate(StartTimeEventListner2);
        TimeRecorder.SetEndTimeDelegate(EndTimeEventListner2);

        // 게임 시작 시간 기록
        TimeRecorder.Reset();
        TimeRecorder.Start();

        // 자리수 입력
        int PlayCount = PlayGame(digits);

        // 게임 종료 시간 기록
        TimeRecorder.Stop();

        Console.Write($"\n게임 기록 : {PlayCount}회 시도\n{TimeRecorder.PrintTime()}");

        Console.WriteLine("\n다시 하시겠습니까? (Y/N)");
        string? playAgain = Console.ReadLine()?.ToUpper();
        if (playAgain == "Y")
        {
            StartGame();
        }
        else
        {
            Console.WriteLine("게임을 종료합니다.");
        }
    }

    private void StartTimeEventListner1(DateTime starttime)
    {
        // 여기에 시작 시간 이벤트 리스너 로직을 추가하세요.
        Console.WriteLine($"\n1. 게임 시작 시간: {starttime}\n");
    }

    private void EndTimeEventListner1(DateTime endtime)
    {
        // 여기에 종료 시간 이벤트 리스너 로직을 추가하세요.
        Console.WriteLine($"\n1. 게임 종료 시간: {endtime}\n");
    }

    private void StartTimeEventListner2(DateTime starttime)
    {
        // 여기에 시작 시간 이벤트 리스너 로직을 추가하세요.
        Console.WriteLine($"\n2. 게임 시작 시간: {starttime}\n");
    }

    private void EndTimeEventListner2(DateTime endtime)
    {
        // 여기에 종료 시간 이벤트 리스너 로직을 추가하세요.
        Console.WriteLine($"\n2. 게임 종료 시간: {endtime}\n");
    }

    public int PlayGame(int InDigits)
    {
        List<int> Quest = new();

        while (Quest.Count < InDigits)
        {
            Quest.Add(GetQuestNumber(Quest));
        }

        Console.WriteLine("생성된 숫자: " + string.Join(", ", Quest));
        Console.WriteLine();

        return InputAnswer(InDigits, Quest);
    }

    public int GetQuestNumber(List<int> InQuest)
    {
        Random random = new();
        int number;
        do
        {
            number = random.Next(0, 10);
        } while (InQuest.Contains(number));
        return number;
    }

    public int InputAnswer(int InDigits, List<int> InQuest)
    {
        int count = 0;
        int strike = 0;
        int ball = 0;
        while (strike < InDigits)
        {
            Console.WriteLine("숫자를 입력하세요: ");
            string? input = Console.ReadLine();

            // 입력이 null이거나 길이가 InDigits가 아니거나 숫자가 아닌 경우
            if (input == null || input.Length != InDigits || !input.All(char.IsDigit))
            {
                Console.WriteLine($"잘못된 입력입니다. {InDigits}자리 숫자를 입력하세요.");
                Console.WriteLine();
                continue;
            }
            Console.WriteLine();

            strike = 0;
            ball = 0;
            for (int i = 0; i < InDigits; i++)
            {
                // 입력된 숫자를 정수로 변환하고 비교
                int userNumber = int.Parse(input[i].ToString());
                if (userNumber == InQuest[i])
                {
                    strike++;
                }
                else if (InQuest.Contains(userNumber))
                {
                    ball++;
                }
            }
            Console.WriteLine($"{strike} 스트라이크, {ball} 볼");
            Console.WriteLine();

            count++;
        }
        Console.WriteLine("축하합니다! 정답을 맞추셨습니다.");
        return count;
    }
}
