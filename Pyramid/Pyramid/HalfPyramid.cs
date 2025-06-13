using Common;
using static System.Console;

namespace HalfPyramid
{
     internal class HalfPyramid
    {
        // static : 클래스의 인스턴스 없이 호출할 수 있는 메서드로, HalfPyramid 클래스의 PrintHalfPyramid 메서드를 호출합니다.
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

        /// static 메서드
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
