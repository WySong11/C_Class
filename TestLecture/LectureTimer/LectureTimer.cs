using System;
using System.Timers;
using static System.Console;

public class LectureTimer
{
    // Timer 인스턴스. 1초마다 이벤트를 발생시킴
    private static Timer? m_timer;

    // 타이머가 몇 번 동작했는지 카운트하는 변수
    private static int m_tickCount = 0;

    public static void Main()
    {
        Timer timer = new Timer(3000); // 1초 간격으로 타이머 설정

        timer.Elapsed += OnTimedEvent1; // 타이머 이벤트 핸들러 등록

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

        //////////////////////////////////////////////////////////
        ///

        // Timer 객체를 생성하고, 1000ms(1초)마다 이벤트 발생하도록 설정
        m_timer = new Timer(1000);

        // 타이머가 경과할 때마다 호출될 이벤트 핸들러 등록
        m_timer.Elapsed += OnTimedEvent2;

        // 타이머가 자동으로 반복 실행되도록 설정 (true: 반복, false: 1회만 실행)
        m_timer.AutoReset = true;

        // 타이머를 활성화 (Enabled 속성을 true로 설정)
        m_timer.Enabled = true;

        // 타이머를 시작 (Start 메서드 호출)
        m_timer.Start();

        // 타이머가 시작된 시각을 콘솔에 출력
        Console.WriteLine("타이머 시작: " + DateTime.Now.ToString("HH:mm:ss"));
        Console.WriteLine("10초 후 타이머가 자동으로 멈춥니다...\n");

        // 메인 스레드가 종료되지 않도록 대기 (없으면 프로그램이 바로 종료됨)
        Console.ReadLine(); // 필요시 주석 해제
    }


    private static void OnTimedEvent1(Object source, ElapsedEventArgs e)
    {
        WriteLine($"Current Time: {DateTime.Now:HH:mm:ss}");
    }

    private static void OnTimedEvent2(object? source, ElapsedEventArgs e)
    {
        // tickCount를 1 증가시킴 (몇 번째 타이머 이벤트인지 카운트
        m_tickCount++;

        // 현재 tickCount와 시각을 콘솔에 출력
        Console.WriteLine($"[{m_tickCount}] 현재 시간: {e.SignalTime:HH:mm:ss}");

        if (m_tickCount == 5)
        {
            if (m_timer != null)
            {
                m_timer.Interval = 500; // 0.5초로 간격 변경
            }
        }

        if (m_tickCount >= 10)
        {
            m_timer?.Stop();
            m_timer?.Dispose();
            Console.WriteLine("\n타이머 종료: " + DateTime.Now.ToString("HH:mm:ss"));
        }
    }
}

