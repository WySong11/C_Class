using System;
using static System.Console;
using System.Threading;
using System.Diagnostics.Metrics;

class program
{
    public static int Count = 0;

    // Lock 객체 생성
    public static readonly object _lock = new object();

    static void Main()
    {
        WriteLine("Lecture Lock Start");

        // 두 개의 스레드가 동시에 Increase 메서드를 실행
        Thread thread1 = new Thread(Increase);
        Thread thread2 = new Thread(Increase);

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        // 최종 Count 값 출력
        // 예상 값은 200000이지만, 동기화가 없으면 다를 수 있음

        // 두 스레드가 Increase 메서드를 실행하는 동안
        // Count 변수에 동시에 접근하여 값이 꼬일 수 있음

        // thread1 이 Count 를 100001 로 읽어올 때
        // thread2 가 Count 를 100002 로 갱신하면
        // thread1 이 100001 에 1을 더한 100002 를 다시 Count 에 저장

        WriteLine($"Final Count: {Count}");
        WriteLine("Lecture Lock End");
    }

    public static void Increase()
    {
        for (int i = 0; i < 100000; i++)
        {
            lock (_lock)
            {
                // Critical Section
                // 이 영역은 한 번에 하나의 스레드만 접근 가능
                Count++;
            }
            //Count++;
        }
    }
}