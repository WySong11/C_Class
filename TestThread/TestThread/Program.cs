using System;
using System.Threading;
using static System.Console;

// internal : 어셈블리 내에서만 접근 가능
// protected : 파생 클래스에서만 접근 가능
// public : 모든 곳에서 접근 가능
// private : 해당 클래스 내에서만 접근 가능

public class Program
{
    static void Main(string[] args)
    {
        /*        WriteLine("Main Thread Started");

                // 새 쓰레드를 만들 때 실행할 메서드를 넘긴다.
                Thread worker = new Thread(PrintStars);

                worker.Start(); // 새 쓰레드 시작

                for(int i = 0; i < 5; i++)
                {
                    WriteLine();
                    WriteLine("i => " + i);
                    Thread.Sleep(500); // 0.5초 대기
                }

                // 메인 쓰레드가 worker 쓰레드가 종료될 때까지 대기
                worker.Join();

                WriteLine("\nMain Thread Ended");*/

        /*        Thread fast = new Thread(FastWork);
                Thread slow = new Thread(SlowWork);

                fast.Start();
                slow.Start();

                fast.Join();
                slow.Join();

                WriteLine("All Work Completed");*/

        /*        Thread t1 = new Thread(() => 
                {
                    for (int i = 0; i < 5; i++)
                    {
                        WriteLine("Lambda Work 1: " + i);
                        Thread.Sleep(300);
                    }
                });

                t1.Start();

                t1.Join();*/

        StartWorker("다운로드 작업", 5);
        StartWorker("자동 저장", 3);

        WriteLine("메인 쓰레드에서 대기 중");
        Thread.Sleep(7000); // 7초 대기
        WriteLine("메인 쓰레드 종료");
    }

    static void StartWorker(string name, int count)
    {
        Thread worker = new Thread(() =>
        {
            DoJob(name, count);          
        });
        worker.IsBackground = true; // 백그라운드 쓰레드로 설정
        worker.Start();
    }

    static void DoJob(string name, int count)
    {
        for (int i = 0; i < count; i++)
        {
            WriteLine($"{name} - 작업 {i} 수행 중...");
            Thread.Sleep(700); 
        }
        WriteLine($"{name} 완료!");
    }

    static void PrintStars()
    {
        for (int i = 0; i < 50; i++)
        {
            Write("*");
            // MS 단위 대기
            // 1000 MS = 1 Second
            Thread.Sleep(300); // 0.3초 대기
        }
        WriteLine();
    }

    static void FastWork()
    {
        for (int i = 0; i < 5; i++)
        {
            WriteLine("Fast Work: " + i);
            Thread.Sleep(200); // 0.2초 대기
        }
    }

    static void SlowWork()
    {
        for (int i = 0; i < 5; i++)
        {
            WriteLine("Slow Work: " + i);
            Thread.Sleep(500); // 0.5초 대기
        }
    }
}