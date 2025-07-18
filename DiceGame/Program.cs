using System;

namespace DiceGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // 예시: 플레이어 캐릭터 생성 및 정보 출력
            var player = new Character.PlayerCharacter("Hero", 100, 20);
            Console.WriteLine($"Player Name: {player.Name}, Health: {player.Health}, Attack Power: {player.AttackPower}");

            player.Attack(new Character.EnemyCharacter("Goblin", 50, 5));
        }
    }
}
