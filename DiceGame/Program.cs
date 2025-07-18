using System;

namespace DiceGame
{
    public class Program
    {
        static void Main(string[] args)
        {
            DiceGame diceGame = new DiceGame();
            diceGame.StartGameAsync();
        }
    }
}
