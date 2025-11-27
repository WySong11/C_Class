using System;
using System.Threading;

public class Program
{
    static int counter = 0;

    static readonly object _lock = new object();

    static void IncrementCounter()
    {
        for (int i = 0; i < 100000; i++)
        {
            /*lock (_lock)
            {
                counter++;
            }            */

            // 정수 같은 단순한 값을 원자적으로 증가시키기 위해 Interlocked 클래스를 사용할 수 있습니다.
            Interlocked.Increment(ref counter);

            //counter++;
        }
    }

    static void Main(string[] args)
    {
        Thread t1 = new Thread(IncrementCounter);
        Thread t2 = new Thread(IncrementCounter);
        Thread t3 = new Thread(IncrementCounter);

        t1.Start();
        t2.Start();
        t3.Start();

        t1.Join();
        t2.Join();
        t3.Join();

        Console.WriteLine($"Final counter value: {counter}");
    }
}
