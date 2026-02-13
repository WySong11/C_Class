using System;
using static System.Console;
using System.Timers;

public class program
{
    public static void Main()
    {
        Timer timer = new Timer(3000); // 1초 간격으로 타이머 설정

        timer.Elapsed += OnTimedEvent; // 타이머 이벤트 핸들러 등록

        // AutoReset : 타이머가 자동으로 재설정되어 반복 실행될지 여부를 설정합니다.
        // 기본값은 true입니다. false로 설정하면 타이머가 한 번만 실행됩니다.      
        //timer.AutoReset = false; // 타이머가 반복되도록 설정

        // Enabled : 타이머가 활성화되어 있는지 여부를 설정합니다.
        // 기본값은 false입니다.        
        //timer.Enabled = true; // 타이머 시작

        // Start : 타이머를 시작하는 메서드입니다.        
        // Start 와 enabled 속성은 동일한 기능을 수행합니다.
        timer.Start();

        WriteLine("Press the Enter key to exit the program... ");
        ReadLine();

        // Stop : 타이머를 중지하는 메서드입니다.
        timer.Stop();

        // Dispose : 타이머 리소스를 해제하는 메서드입니다.
        timer.Dispose();
    }


    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {
        WriteLine($"Current Time: {DateTime.Now:HH:mm:ss}");
    }
}

