using System;
using static System.Console;

namespace MyClass_Library
{
    public static class Pyramid
    {
        public static void DrawPyramid(int height, bool reverse)
        {
            WriteLine();

            if (height <= 0)
            {
                WriteLine("Height must be greater than 0.");
                return;
            }

            for (int i = 1; i <= height; i++)
            {
                // 빈 공간 과 별의 개수를 계산합니다.
                int space = reverse ? i - 1 : height - i;
                int star = reverse ? (2 * height) - (i * 2 - 1) : 2 * i - 1;

                Draw(space, star);
            }

            WriteLine();
        }

        public static void Draw(int space, int star)
        {
            // Print leading spaces
            Write(new string(' ', space));
            // Print stars
            WriteLine(new string('*', star));
        }
    }
}
