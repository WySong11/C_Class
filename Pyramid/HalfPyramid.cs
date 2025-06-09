using Common;
using static System.Console;

namespace HalfPyramid
{
    internal class HalfPyramid
    {        
        public static void PrintHalfPyramid(int height, ETypePyamid type)
        {
            for (int i = 1; i <= height; i++)
            {
                switch(type)
                {
                    case ETypePyamid.Increase:
                        WriteLine(new string('*', i));
                        break;
                    case ETypePyamid.Decrease:
                        WriteLine(new string('*', height - i + 1));
                        break;
                    case ETypePyamid.ReverseIncrease:
                        Write(new string(' ', height - i)); // Print leading spaces
                        WriteLine(new string('*', i)); // Print stars
                        break;
                    case ETypePyamid.ReverseDecrease:
                        Write(new string(' ', i - 1)); // Print leading spaces
                        WriteLine(new string('*', height - i + 1)); // Print stars
                        break;
                    default:
                        WriteLine("Invalid pyramid type.");
                        return;
                }
            }
        }
    }
}
