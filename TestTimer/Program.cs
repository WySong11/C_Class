using System;
using System.Timers;

public class Program
{
    // Timer 인스턴스. 1초마다 이벤트를 발생시킴
    private static Timer? timer;

    // 타이머가 몇 번 동작했는지 카운트하는 변수
    private static int tickCount = 0;

    static void Main(string[] args)
    {
        // Timer 객체를 생성하고, 1000ms(1초)마다 이벤트 발생하도록 설정
        timer = new Timer(1000);

        // 타이머가 경과할 때마다 호출될 이벤트 핸들러 등록
        timer.Elapsed += OnTimedEvent;

        // 타이머가 자동으로 반복 실행되도록 설정 (true: 반복, false: 1회만 실행)
        timer.AutoReset = true; 

        // 타이머를 활성화 (Enabled 속성을 true로 설정)
        timer.Enabled = true;

        // 타이머를 시작 (Start 메서드 호출)
        timer.Start();

        // 타이머가 시작된 시각을 콘솔에 출력
        Console.WriteLine("타이머 시작: " + DateTime.Now.ToString("HH:mm:ss"));
        Console.WriteLine("10초 후 타이머가 자동으로 멈춥니다...\n");

        // 메인 스레드가 종료되지 않도록 대기 (없으면 프로그램이 바로 종료됨)
        Console.ReadLine(); // 필요시 주석 해제
    }

    // 타이머가 경과할 때마다 호출되는 이벤트 핸들러
    private static void OnTimedEvent(object? source, ElapsedEventArgs e)
    {
        // tickCount를 1 증가시킴 (몇 번째 타이머 이벤트인지 카운트
        tickCount++;

        // 현재 tickCount와 시각을 콘솔에 출력
        Console.WriteLine($"[{tickCount}] 현재 시간: {e.SignalTime:HH:mm:ss}");

        if (tickCount == 5)
        {
            if (timer != null)
            {
                timer.Interval = 500; // 0.5초로 간격 변경
            }
        }

        if (tickCount >= 10)
        {
            timer?.Stop();
            timer?.Dispose();
            Console.WriteLine("\n타이머 종료: " + DateTime.Now.ToString("HH:mm:ss"));
        }
    }
}