public class NumberBaseballUsingDelegate
{
    static void Main(string[] args)
    {
        PlayTheGame();
    }

    private static void PlayTheGame()
    {
        // Create a new instance of the game
        var game = new NumberBaseballGame();
        // Start the game
        game.StartGame();

        Console.WriteLine("Press any key to exit...");
    }
}

