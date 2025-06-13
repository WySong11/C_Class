using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Console;

namespace BasePyramid
{
    // abstract : 추상 클래스는 인스턴스를 생성할 수 없으며, 파생 클래스에서 반드시 구현해야 하는 메서드를 정의합니다.
    abstract class BasePyrramid
    {
        // 추상화 피라미드 출력 메서드
        public abstract void PrintPyramid(int height, bool reverse);

        // virtual 메서드로 클래스 이름을 출력하는 메서드
        public virtual void WriteClassName()
        {
            WriteLine($"Base Class Name: {GetType().Name}");

            PrintCurrentMethod();

            WriteProtected();

            WriteLine();
        }

        /// <summary>
        /// 피라미드 출력 메서드
        /// </summary>
        /// <param name="space">앞에 출력할 공백의 개수</param>
        /// <param name="star">출력할 별의 개수</param>
        public virtual void Draw(int space, int star)
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

        // private 메서드는 클래스 내부에서만 호출할 수 있는 메서드입니다.
        private void WriteProtected()
        {
            // string? :  null 허용, null이 될 수 있는 문자열 변수 선언
            // MethodBase.GetCurrentMethod() : 현재 메서드의 정보를 가져옵니다.
            string? methodName = MethodBase.GetCurrentMethod()?.Name;
            if (methodName != null)
            {
                Console.WriteLine("현재 함수 이름: " + methodName);
            }
            else
            {
                Console.WriteLine("현재 함수 이름을 가져올 수 없습니다.");
            }
        }

        // protected 메서드는 파생 클래스에서만 호출할 수 있는 메서드입니다.
        // CallerMemberName 특성을 사용하여 현재 메서드의 이름을 자동으로 가져옵니다.
        protected void PrintCurrentMethod([CallerMemberName] string memberName = "")
        {
            Console.WriteLine("현재 함수 이름: " + memberName);
        }
    }
}