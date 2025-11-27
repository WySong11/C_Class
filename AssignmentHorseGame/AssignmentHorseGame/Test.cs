/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Horse
{
    // TODO: 말 이름을 Name 에 저장해 보세요.
    // TODO: Position, Finished, Rank 를 적절한 초기값으로 설정해 보세요.
}

class Program
{
    // 콘솔 출력이 서로 섞이지 않도록 잠그는 용도
    private static readonly object consoleLock = new object();

    // 결승선 도착 순서 계산용
    private static int nextRank = 1;

    static void Main(string[] args)
    {
        int trackLength = 100; // 결승선 거리

        // TODO: 말 이름을 바꿔 보거나 개수를 바꿔 보세요.
        List<Horse> horses = new List<Horse>
        {
            new Horse("말1"),
            new Horse("말2"),
            new Horse("말3"),
            new Horse("말4")
        };

        List<Thread> threads = new List<Thread>();

        Console.WriteLine("=== 말 달리기 경기 시작 ===");

        // TODO: 각 말을 위해 스레드를 만들고 RunHorse 메서드를 실행하도록 해 보세요.
        foreach (var horse in horses)
        {

        }

        // TODO: 모든 말 스레드가 끝날 때까지 기다려 보세요.
        foreach (var t in threads)
        {

        }

        Console.WriteLine();
        Console.WriteLine("=== 경기 종료 ===");
        Console.WriteLine();

        // TODO: Rank 기준으로 정렬해서 결과를 출력해 보세요.
        //var result = ;

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
            // TODO: 1~5 사이의 랜덤 이동량을 생성해서 step 변수에 넣어 보세요.
            int step = ;

            // TODO: step 만큼 말을 전진시키는 코드를 작성해 보세요.
            horse.Position = ;

            // TODO: lock 을 사용해서 콘솔 출력을 한 번에 출력되도록 보호해 보세요.
            lock (consoleLock)
            {
                int clampedPos = Math.Min(horse.Position, trackLength);
                Console.WriteLine($"{horse.Name} 전진 +{step}, 현재 거리 {clampedPos}/{trackLength}");
            }

            // TODO: 결승선에 도착했는지 확인하고 Rank 를 지정해 보세요.
            if (horse.Position >= trackLength)
            {
                lock (consoleLock)
                {

                }

                break;
            }

            // TODO: Random 을 사용해서 0.1초에서 0.3초 사이로 잠깐 쉬게 해 보세요.
            int sleepMs = ;
        }
    }
}
*/