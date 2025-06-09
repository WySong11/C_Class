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
                switch (type)
                {
                    case ETypePyamid.Increase:
                        WriteLine(new string('*', i));
                        break;
                    case ETypePyamid.Decrease:
                        WriteLine(new string('*', height - i + 1));
                        break;
                    case ETypePyamid.ReverseIncrease:
                        Draw(height - i, i); // Draw the half pyramid
                        break;
                    case ETypePyamid.ReverseDecrease:                        
                        Draw(i - 1, height - i + 1); // Draw the half pyramid
                        break;
                    default:
                        WriteLine("Invalid pyramid type.");
                        return;
                }
            }
        }

        /// 피라미드 출력 메서드
        /// space: 앞에 출력할 공백의 개수
        /// star: 출력할 별의 개수
        private static void Draw(int space, int star)
        {
            Write(new string(' ', space)); // Print leading spaces
            WriteLine(new string('*', star)); // Print stars
        }
    }
}
