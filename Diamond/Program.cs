using System;
using static System.Console;

namespace Diamond
{
    public class Program
    {
        static void Main(string[] args)
        {
            DrawDiamond();
        }

        public static void DrawDiamond()
        {
            WriteLine();

            int height = Input.GetHeight();

            WriteLine();
            Output.PrintDiamondOneLoop(height);
            WriteLine();

            if (Input.RestartProgram())
            {
                DrawDiamond();
            }
            else
            {
                WriteLine("Thank you for using the diamond drawing program!");
            }
        }
    }
}
