using System;
using static System.Console;
using BasePyramid;
using System.Transactions; // Assuming BasePyramid is in the same namespace or accessible

namespace Pyramid
{
    // BasePyrramid 클래스를 상속받는 Pyramid 클래스입니다.
    // internal : 같은 프로젝트 내에서만 접근할 수 있는 클래스입니다.
    internal class Pyramid : BasePyrramid
    {
        /// 피라미드 출력 메서드
        /// height: 피라미드 높이
        /// reverse: true이면 역순으로 출력, false이면 정순으로 출력
        /// 추상환된 PrintPyramid 메서드를 구현합니다.
        public override void PrintPyramid(int height, bool reverse)
        {
            WriteLine();

            if (height <= 0)
            {
                WriteLine("Height must be greater than 0.");
                return;
            }

            // Base 클래스의 WriteClassName 메서드를 호출하여 클래스 이름을 출력합니다.
            base.WriteClassName();

            // 파생 클래스의 WriteClassName 메서드를 호출하여 파생 클래스 이름을 출력합니다.
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

        // Base 클래스의 WriteClassName 메서드를 오버라이드하여 파생 클래스의 이름을 출력합니다.
        public override void WriteClassName()
        {
            base.WriteClassName();

            WriteLine($"Derived Class Name: {GetType().Name}");

            //base.WriteProtected();

            base.PrintCurrentMethod();

            WriteLine();
        }

        /// 피라미드 출력 메서드
        /// space: 앞에 출력할 공백의 개수
        /// star: 출력할 별의 개수
        /// Base 클래스의 Draw 메서드를 오버라이드하여 피라미드를 출력합니다.
        public override void Draw(int space, int star)
        {
            // Print leading spaces
            Write(new string(' ', space));
            // Print stars
            WriteLine(new string('*', star));   
        }
    }
}
