using System;
using static System.Console;

public class Diamond
{
    public static void StartDiamond()
    {
        WriteLine("\n다이아몬드 높이를 입력하세요 (홀수): ");
        string? input = ReadLine();
        if (int.TryParse(input, out int height) && height % 2 == 1)
        {
            WriteLine($"\n다이아몬드 높이: {height}\n");

            TimeRecorder.AddStartTimeDelegate(StartTimeEventListner);
            TimeRecorder.AddEndTimeDelegate(EndTimeEventListner);

            TimeRecorder.Reset();
            TimeRecorder.Start();

            Output.PrintDiamond(height);
            Output.PrintDiamondOneLoop(height);

            TimeRecorder.Stop();

            TimeRecorder.RemoveStartTimeDelegate(StartTimeEventListner);
            TimeRecorder.RemoveEndTimeDelegate(EndTimeEventListner);
        }
        else
        {
            WriteLine("잘못된 입력입니다. 홀수를 입력하세요.");
        }
    }

    public static void DrawDiamond()
    {
        WriteLine();

        int height = Input.GetHeight();

        WriteLine();
        Output.PrintDiamondOneLoop(height);
        WriteLine();

        if (Input.RestartProgram())
        {
            DrawDiamond();
        }
        else
        {
            WriteLine("Thank you for using the diamond drawing program!");
        }
    }

    private static void StartTimeEventListner(DateTime starttime) // Changed to static
    {
        // 여기에 시작 시간 이벤트 리스너 로직을 추가하세요.
        Console.WriteLine($"\n다이아몬드 게임 시작 시간: {starttime}\n");
    }

    private static void EndTimeEventListner(DateTime endtime) // Changed to static
    {
        // 여기에 종료 시간 이벤트 리스너 로직을 추가하세요.
        Console.WriteLine($"\n다이아몬드 게임 종료 시간: {endtime}\n");
    }
}