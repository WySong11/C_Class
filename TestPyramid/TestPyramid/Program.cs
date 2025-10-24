using System;

namespace TestPyramid
{
    enum PyramidType
    {
        Normal,
        Reversed,
        

    }

    public class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("피라미드 단을 입력하시오 : ");
                int height = int.Parse(Console.ReadLine());

                Console.WriteLine();

                if( height <= 0 )
                {
                    break;
                }

                // height = 3

                // 중앙 정렬된 피라미드 출력
                for (int i = 0; i < height; i++)
                {
                    // 왼쪽 공백: height - i - 1개
                    for (int k = 0; k < height - i - 1; k++)
                    {
                        Console.Write(" ");
                    }

                    // 별: 2*i + 1개
                    for (int j = 0; j < 2 * i + 1; j++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                }

                // 다이아몬드 - 아래쪽 (중앙 바로 아래부터)
                for (int i = height - 2; i >= 0; i--)
                {
                    for (int s = 0; s < height - i - 1; s++)
                    {
                        Console.Write(" ");
                    }

                    for (int j = 0; j < 2 * i + 1; j++)
                    {
                        Console.Write("*");
                    }

                    Console.WriteLine();
                }

                // 한 줄 띄우기
                Console.WriteLine();

            } while (true);
        }
    }
}

/*

*       i == 0 , j = 0 , 0 <= 0  -> * -> j++ -> j = 1 -> 1 <= 0 X -> 줄바꿈
**      i == 1 , j = 0 , 0 <= 1  -> * -> j++ -> j = 1 -> 1 <= 1 -> * -> j++ -> j = 2 -> 2 <= 1 X -> 줄바꿈
***     i == 2 , j = 0 , 0 <= 2  -> * -> j++ -> j = 1 -> 1 <= 2 -> * -> j++ -> j = 2 -> 2 <= 2 -> * -> j++ -> j = 3 -> 3 <= 2 X -> 줄바꿈

  *
 **
***
 

 */

