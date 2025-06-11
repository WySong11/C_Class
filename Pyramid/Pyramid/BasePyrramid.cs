using System;
using static System.Console;

namespace BasePyramid
{
    abstract class BasePyrramid
    {
        // 추상화 피라미드 출력 메서드
        public abstract void PrintPyramid(int height, bool reverse);

        public virtual void WriteClassName()
        {
            WriteLine($"Base Class Name: {GetType().Name}");
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

            for(int i = 0; i < star; i++)
            {
                Write('*'); // Print stars
            }

            WriteLine(); // Move to the next line after printing stars

            // Print leading spaces
            //Write(new string(' ', space));
            // Print stars
            //WriteLine(new string('*', star));
        }
    }
}
