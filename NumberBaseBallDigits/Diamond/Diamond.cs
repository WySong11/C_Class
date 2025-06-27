using System;
using static System.Console;

// 일반 클래스 + static 메서드로 구성된 다이아몬드 출력 프로그램
// 일부 함수는 static으로 변경하여 인스턴스 생성 없이 호출 가능
public class Diamond
{
    // 인스턴스 변수로 선언된 bool 변수
    public bool TestBool;

    // static 메서드로 변경하여 인스턴스 생성 없이 호출 가능
    public static bool TestStaticBool;

    public static void StartDiamond()
    {
        Clear();
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

    // Static 메서드가 아니라서 인스턴스를 생성해야 하는 경우
    public void ClearGame()
    {
        TimeRecorder.RemoveStartTimeDelegate(StartTimeEventListner);
        TimeRecorder.RemoveEndTimeDelegate(EndTimeEventListner);
    }
}