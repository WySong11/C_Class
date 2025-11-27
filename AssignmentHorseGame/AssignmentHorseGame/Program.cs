using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Horse
{
    public string Name;
    public int Position;
    public bool Finished;
    public int Rank;

    public Horse(string name)
    {
        Name = name;
        Position = 0;
        Finished = false;
        Rank = 0;
    }
}

class Program
{
    // 콘솔 출력이 섞이지 않게 잠그는 용도
    private static readonly object consoleLock = new object();

    // 결승선 도착 순서 계산용
    private static int nextRank = 1;

    static void Main(string[] args)
    {
        int trackLength = 100;

        List<Horse> horses = new List<Horse>
        {
            new Horse("말1"),
            new Horse("말2"),
            new Horse("말3"),
            new Horse("말4")
        };

        List<Thread> threads = new List<Thread>();

        Console.WriteLine("=== 말 달리기 경기 시작 ===");

        foreach (var horse in horses)
        {
            Thread t = new Thread(() => RunHorse(horse, trackLength));
            t.Start();
            threads.Add(t);
        }

        foreach (var t in threads)
        {
            t.Join();
        }

        Console.WriteLine();
        Console.WriteLine("=== 경기 종료 ===");
        Console.WriteLine();

        var result = horses
            .OrderBy(h => h.Rank)
            .ToList();

        foreach (var horse in result)
        {
            Console.WriteLine($"{horse.Rank}등: {horse.Name} (거리 {horse.Position})");
        }

        Console.WriteLine();
        Console.WriteLine("아무 키나 누르면 종료됩니다.");
        Console.ReadKey();
    }

    static void RunHorse(Horse horse, int trackLength)
    {
        // 각 스레드마다 다른 시드로 Random 생성
        Random rand = new Random(Guid.NewGuid().GetHashCode());

        while (!horse.Finished)
        {
            int step = rand.Next(1, 6); // 1~5칸 사이 이동
            horse.Position += step;

            lock (consoleLock)
            {
                // 현재 말의 위치를 간단하게 표시
                int clampedPos = Math.Min(horse.Position, trackLength);
                Console.WriteLine(
                    $"{horse.Name} 전진 +{step}, 현재 거리 {clampedPos}/{trackLength}");
            }

            if (horse.Position >= trackLength)
            {
                lock (consoleLock)
                {
                    if (!horse.Finished)
                    {
                        horse.Finished = true;
                        horse.Rank = nextRank;
                        nextRank++;

                        Console.WriteLine(
                            $"*** {horse.Name} 결승선 통과! 현재 {horse.Rank}등 ***");
                    }
                }

                break;
            }

            Thread.Sleep(rand.Next(100, 300));
        }
    }
}
