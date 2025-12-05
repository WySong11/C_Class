using System;
using System.Threading.Tasks;

public class Program
{
    // 비동기 Main 메서드
    // 게임 이벤트를 비동기로 실행
    static async Task Main(string[] args)
    {
        var game = new Game();

        // 게임 비동기 실행 및 종료까지 대기
        await game.StartGameEventAsync();

        Console.WriteLine("아무 키나 누르면 종료합니다.");
        Console.ReadKey();
    }
}