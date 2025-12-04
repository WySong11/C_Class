using System;
using System.Threading.Tasks;

namespace DiceGameUseTimer
{
    /// <summary>
    /// 프로그램의 진입점(Entry Point)을 담당하는 클래스입니다.
    /// </summary>
    class Program
    {
        /// <summary>
        /// 비동기 Main 메서드.
        /// DiceGameUseTimer 게임을 생성하고, 게임 이벤트를 비동기로 실행합니다.
        /// </summary>
        /// <param name="args">명령줄 인자(사용하지 않음)</param>
        static async Task Main(string[] args)
        {
            // DiceGameUseTimer 인스턴스 생성
            var game = new DiceGameUseTimer();

            // 게임 이벤트(전투, 입력 등) 비동기 실행 및 게임 종료까지 대기
            await game.StartGameEventAsync();

            // 게임이 완전히 끝난 후 콘솔이 바로 닫히지 않도록 사용자 입력을 대기
            Console.WriteLine("아무 키나 누르면 종료합니다.");
            Console.ReadKey();
        }
    }
}