using System;
using static System.Console;

namespace DrawDiamond
{
    public class DiamondDrawer
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
            Output.PrintDiamondLoop(height);
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
