using System;
using static System.Console;
using BasePyramid;
using System.Transactions; // Assuming BasePyramid is in the same namespace or accessible

namespace Pyramid
{
    internal class Pyramid : BasePyrramid
    {
        /// 피라미드 출력 메서드
        /// height: 피라미드 높이
        /// reverse: true이면 역순으로 출력, false이면 정순으로 출력
        public override void PrintPyramid(int height, bool reverse)
        {
            WriteLine();

            if (height <= 0)
            {
                WriteLine("Height must be greater than 0.");
                return;
            }

            base.WriteClassName(); // Call the base class method to print the class name

            WriteClassName();

            for (int i = 1; i <= height; i++)
            {
                // 빈 공간 과 별의 개수를 계산합니다.
                int space = reverse ? i - 1 : height - i;
                int star = reverse ? (2 * height) - (i*2 -1) : 2 * i - 1;

                Draw(space, star);
            }

            WriteLine();
        }

        public override void WriteClassName()
        {
            base.WriteClassName();

            WriteLine($"Derived Class Name: {GetType().Name}");
        }

        /// 피라미드 출력 메서드
        /// space: 앞에 출력할 공백의 개수
        /// star: 출력할 별의 개수
        public override void Draw(int space, int star)
        {
            // Print leading spaces
            Write(new string(' ', space));
            // Print stars
            WriteLine(new string('*', star));   
        }
    }
}
