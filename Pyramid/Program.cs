using System;
using static System.Console;
using Common;
using Pyramid;
using static HalfPyramid.HalfPyramid;

namespace Main
{
    internal class Program
    {
        const byte MaxHeight = 20; // Maximum height for the pyramid

        static void Main(string[] args)
        {
            ETypePyamid eTypePyamid = ETypePyamid.None;
            int Height = 0;
            bool bHalf = false;

            do
            {
                Selection(ref bHalf, ref eTypePyamid, ref Height);

                if (bHalf)
                {
                    PrintHalfPyramid(Height, eTypePyamid);
                }
                else
                {
                    // If not HalfPyramid, print the full pyramid
                    Pyramid.Pyramid pyramid = new Pyramid.Pyramid();
                    pyramid.PrintPyramid(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);
                }
            } while (Height > 0);
        }

        public static void Selection(ref bool half, ref ETypePyamid type, ref int height )
        {
            height = 0;
            do
            {
                WriteLine("\nEnter the height of the pyramid (1-{0}): ", MaxHeight);
                string? input = ReadLine();
                if (byte.TryParse(input, out byte h) && h > 0 && h <= MaxHeight)
                {
                    height = h;
                    break; // Exit loop if valid height is entered
                }
                else
                {
                    WriteLine($"Please enter a valid height between 1 and {MaxHeight}.");
                }
            } while (height > 0);

            half = false; // Reset half to false for the next part of the program
            bool selection = true;
            do
            {
                WriteLine("\nPyramid or HalfPyramid? (1: Pyramid, 2: HalfPyramid)");
                string? choice = ReadLine();

                if (choice == "1")
                {
                    half = false;
                    WriteLine("You selected Pyramid.");
                }
                else if (choice == "2")
                {
                    half = true;
                    WriteLine("You selected HalfPyramid.");
                }
                else
                {
                    WriteLine("Invalid choice. Please select 1 or 2.");
                    selection = false;
                }

            } while(selection == false );

            type = ETypePyamid.None; // Initialize direction
            do
            {
                WriteLine("\nSelect the direction of the pyramid (1: Increase, 2: Decrease, 3: Revers Increase, 4: Reverse Decrease): ");
                string? input = ReadLine();

                switch(input)
                {
                    case "1":
                        type = ETypePyamid.Increase; // Increase
                        WriteLine("You selected Increase direction.");
                        break;
                    case "2":
                        type = ETypePyamid.Decrease; // Decrease
                        WriteLine("You selected Decrease direction.");
                        break;
                    case "3":
                        type = ETypePyamid.ReverseIncrease; // Reverse Increase
                        WriteLine("You selected Reverse Increase direction.");
                        break;
                    case "4":
                        type = ETypePyamid.ReverseDecrease; // Reverse Decrease
                        WriteLine("You selected Reverse Decrease direction.");
                        break;
                    default:
                        WriteLine("Invalid selection. Please select 1, 2, 3, or 4.");
                        continue; // Continue the loop for valid input
                }

            } while (type == ETypePyamid.None);

            return;
        }
    }
}
