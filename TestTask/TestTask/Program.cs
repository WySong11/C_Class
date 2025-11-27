using System;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    static async Task Main(string[] args)
    {
        /*        Console.WriteLine("[메인] 프로그램 시작");

                // 별 출력 작업을 별도의 Task로 백그라운드에서 실행
                Task starTask = Task.Run(() =>
                {
                    for(int i=0; i<5; i++)
                    {
                        Console.WriteLine("[Task] *****");
                        Thread.Sleep(300); // 0.3초 대기
                    }
                });

                // 메인 스레드에서 숫자 출력 작업 수행
                for(int i=0; i<5; i++)
                {
                    Console.WriteLine("[메인] {0}", i);
                    Thread.Sleep(500); // 0.5초 대기
                }

                // 별 출력 작업이 완료될 때까지 대기
                starTask.Wait();

                Console.WriteLine("[메인] 프로그램 종료");*/

        ////////////////////////////////////////////////////////////////////////////////////////

        /*        Task fastTask = FastWorkAsync();
                Task slowTask = SlowWorkAsync();

                Console.WriteLine("[메인] 두 작업이 시작되었습니다.");

                // 두 작업이 모두 완료될 때까지 대기
                await Task.WhenAll(fastTask, slowTask);

                Console.WriteLine("[메인] 모든 작업이 완료되었습니다.");*/

        ////////////////////////////////////////////////////////////////////////////////////////

        /*        Console.WriteLine("몬스터 처히 후 경험치 계산 시작");

                // 경험치를 비동기로 계산
                Task<int> expTask = CalculateExpAsync(3); // 몬스터 3마리 처치

                Console.WriteLine("다른 작업 수행 중...");
                await Task.Delay(1000); // 다른 작업에 1초 소요

                // 경험치 계산이 완료될 때까지 대기하고 결과 받기
                int exp = await expTask;

                Console.WriteLine("획득한 경험치: {0}", exp);
                Console.WriteLine("프로그램 종료");*/

        ////////////////////////////////////////////////////////////////////////////////////////

        Console.WriteLine("씬 전환 준비 중..");

        Task autoSaveTask = Task.Run(() =>
        {
            Console.WriteLine("자동 저장 시작...");
            Thread.Sleep(1000); // 1초 소요. 파일 저장 작업 시뮬레이션
            Console.WriteLine("자동 저장 완료!");
        });

        Task updateRankTask = Task.Run(() =>
        {
            Console.WriteLine("랭킹 업데이트 시작...");
            Thread.Sleep(1500); // 1.5초 소요. 랭킹 서버와 통신 시뮬레이션
            Console.WriteLine("랭킹 업데이트 완료!");
        });

        Console.WriteLine("다른 씬 전환 작업 수행 중...");

        await Task.WhenAll(autoSaveTask, updateRankTask);

        Console.WriteLine("씬 전환 준비 완료!");
    }

    static async Task<int> CalculateExpAsync(int monsterCount)
    {
        int totalExp = 0;

        for(int i=1; i<=monsterCount; i++)
        {
            Console.WriteLine("몬스터 {0} 처치 중...", i);

            await Task.Delay(500); // 몬스터 처치에 0.5초 소요

            totalExp += 100; // 몬스터 한 마리당 100 경험치 획득

            Console.WriteLine("몬스터 {0} 처치 완료!", i);
        }

        return totalExp;
    }

    static async Task FastWorkAsync()
    {
        for(int i=0; i<10; i++)
        {
            Console.WriteLine("[FastWork] 빠른 작업 {0}", i);
            await Task.Delay(100); // 0.1초 대기
        }
    }

    static async Task SlowWorkAsync()
    {
        for(int i=0; i<10; i++)
        {
            Console.WriteLine("[SlowWork] 느린 작업 {0}", i);
            await Task.Delay(300); // 0.3초 대기
        }
    }
}