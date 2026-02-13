using System;
using static System.Console;
using System.Threading;

public class Program
{
    public static void Main()
    {
        SingleThread singleThread = new SingleThread();
        singleThread.Start();

        MultiThread multiThread = new MultiThread();
        multiThread.Start();

        TaskWorker worker = new TaskWorker();
        worker.Start();
    }
}

public class SingleThread
{
    public void Start()
    {
        WriteLine("Lecture Thread Start");

        // 새로운 스레드 생성 및 시작
        // CountNumbers 메서드를 스레드에서 실행
        Thread thread = new Thread(new ThreadStart(CountNumbers));

        // 스레드 시작
        thread.Start();


        // 메인 스레드에서 다른 작업 수행
        for (int i = 0; i < 5; i++)
        {
            WriteLine($"Main Thread: {i}");

            // Sleep : 메인 스레드를 일정 시간 동안 일시 중지
            Thread.Sleep(500); // 0.5초 대기
        }

        // Join : 메인 스레드가 작업을 마치고
        // 스레드가 종료될 때까지 대기
        thread.Join();

        WriteLine("Lecture Thread End");
    }
    public static void CountNumbers()
    {
        for (int i = 0; i < 10; i++)
        {
            WriteLine($"Worker Thread: {i}");
            Thread.Sleep(300); // 0.3초 대기
        }
    }
}

public class MultiThread
{
    public void Start()
    {
        WriteLine("Lecture Thread Start");

        // 새로운 스레드 생성 및 시작
        // CountNumbers 메서드를 스레드에서 실행
        Thread thread1 = new Thread(new ThreadStart(FastWork));
        Thread thread2 = new Thread(new ThreadStart(SlowWork));

        // 스레드 시작
        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        WriteLine("Lecture Thread End");
    }

    public void FastWork()
    {
        for (int i = 0; i < 10; i++)
        {
            WriteLine($"Fast Worker Thread: {i}");
            Thread.Sleep(200); // 0.2초 대기
        }
    }

    public void SlowWork()
    {
        for (int i = 0; i < 10; i++)
        {
            WriteLine($"Slow Worker Thread: {i}");
            Thread.Sleep(500); // 0.5초 대기
        }
    }
}

public class LambdaThread
{
    public void Start()
    {
        WriteLine("Lecture Thread Start");
        // 람다식을 사용하여 스레드 생성 및 시작
        Thread thread = new Thread(() =>
        {
            for (int i = 0; i < 10; i++)
            {
                WriteLine($"Lambda Worker Thread: {i}");
                Thread.Sleep(300); // 0.3초 대기
            }
        });

        // 스레드 시작
        thread.Start();

        // 메인 스레드에서 다른 작업 수행
        for (int i = 0; i < 5; i++)
        {
            WriteLine($"Main Thread: {i}");
            Thread.Sleep(500); // 0.5초 대기
        }

        thread.Join();
        WriteLine("Lecture Thread End");
    }
}

public class ParameterizedThread
{
    public void Start()
    {
        WriteLine("Lecture Thread Start");

        // 매개변수를 받는 스레드 생성 및 시작
        Thread thread = new Thread(new ParameterizedThreadStart(PrintMessage));

        // 스레드 시작, 매개변수 전달
        thread.Start("Hello from Parameterized Thread!");

        thread.Join();
        WriteLine("Lecture Thread End");
    }
    public void PrintMessage(object? message)
    {
        for (int i = 0; i < 5; i++)
        {
            WriteLine($"Parameterized Worker Thread: {message}");
            Thread.Sleep(400); // 0.4초 대기
        }
    }
}

public class TaskWorker
{
    public void Start()
    {
        WriteLine("Lecture Thread Start");

        StartWorker("Download Task", 10);
        StartWorker("Upload Task", 5);

        Thread.Sleep(1000); // 메인 스레드가 종료되지 않도록 대기
    }

    public void StartWorker(string taskName, int iterations)
    {
        Thread thread = new Thread(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                WriteLine($"{taskName} - Step {i + 1} of {iterations}");
                Thread.Sleep(400); // 0.4초 대기
            }
        });
        thread.Start();
        thread.Join();
    }
}