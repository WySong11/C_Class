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
                        Draw(0, i);
                        break;

                    case ETypePyamid.Decrease:
                        Draw(0, height - i + 1);
                        break;

                    case ETypePyamid.ReverseIncrease:
                        Draw(height - i, i);
                        break;

                    case ETypePyamid.ReverseDecrease:                        
                        Draw(i - 1, height - i + 1);
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
            for (int i = 0; i < space; i++)
            {
                Write(' '); // Print leading spaces
            }

            for (int i = 0; i < star; i++)
            {
                Write('*'); // Print stars
            }

            WriteLine(); // Move to the next line after printing stars
        }
    }
}
