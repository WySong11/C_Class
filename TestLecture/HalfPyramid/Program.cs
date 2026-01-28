using System;

public class HalfPyramid
{
    public enum E_TYPE
    {
        HalfPyramid_Increase,
        HalfPyramid_Decrease,
        HalfPyramid_Increase_Reverse,
        HalfPyramid_Decrease_Reverse,

        Pyramid,
        Reverse_Pyramid,

        Diamond,
    }

    public static void Main(string[] args)
    {
        E_TYPE eType = E_TYPE.Diamond;
        int space = 0;
        int star = 0;

        do
        {
            Console.Write("Input Height : ");
            int Height = int.Parse(Console.ReadLine());

            Console.Write("Input Type ( 1 ~ 7 ) : ");
            eType = Console.ReadLine() switch
            {
                "1" => E_TYPE.HalfPyramid_Increase,
                "2" => E_TYPE.HalfPyramid_Decrease,
                "3" => E_TYPE.HalfPyramid_Increase_Reverse,
                "4" => E_TYPE.HalfPyramid_Decrease_Reverse,
                "5" => E_TYPE.Pyramid,
                "6" => E_TYPE.Reverse_Pyramid,
                "7" => E_TYPE.Diamond,                
            };





            for(int i = 1; i <= Height; i++)
            {
                switch (eType)
                {
                    case E_TYPE.HalfPyramid_Increase:
                        Draw(0, i);
                        break;

                    case E_TYPE.HalfPyramid_Decrease:
                        break;

                    case E_TYPE.HalfPyramid_Increase_Reverse:

                        break;

                    case E_TYPE.HalfPyramid_Decrease_Reverse:
                        break;

                    case E_TYPE.Pyramid:
                        break;

                    case E_TYPE.Reverse_Pyramid:
                        break;

                    case E_TYPE.Diamond:
                        break;
                }
            }


            // Diamond

/*            for (int i= 1; i <= Height; i++)
            {
                // Height = 5
                // i = 1 , space = 4 , star = 1
                // i = 2 , space = 2 , star = 3
                // i = 3 , space = 0 , star = 5
                // i = 4 , space = 2 , star = 3
                // i = 5 , space = 4 , star = 1

                // half = 3
                int half = (Height / 2) + 1;

                // i = 1 , 3 - 1 = 2
                // i = 2 , 3 - 2 = 1
                // i = 3 , 3 - 3 = 0
                // i = 4 , 3 - 4 = -1 => 1
                // i = 5 , 3 - 5 = -2 => 2

                // Math.Abs : 절대값 함수 ( 모든 수를 양수로 변환 )
                int space = Math.Abs( half - i );

                // 각 행에서 별을 출력
                // i = 1 , star = 5 - ( 2 * 2 ) = 1
                // i = 2 , star = 5 - ( 1 * 2 ) = 3
                // i = 3 , star = 5 - ( 0 * 2 ) = 5
                // i = 4 , star = 5 - ( 1 * 2 ) = 3
                // i = 5 , star = 5 - ( 2 * 2 ) = 1
                int star = Height - ( space * 2 );

                for (int j = 0; j < space; j++)
                {
                    Console.Write(" ");
                }
                
                for (int j = 0; j < star; j++)
                {
                    Console.Write("*");
                }     
                
                Console.WriteLine();
            }*/

            // Reverse Pyramid
            /*            for(int i = 1; i <= Height; i++)
                        {
                            // 공백을 출력
                            // Height = 5
                            // i = 1 , space = 0
                            // i = 2 , space = 1
                            // i = 3 , space = 2
                            int space = i - 1;

                            // 별을 출력
                            // Height = 5
                            // i = 1 , star = 9
                            // i = 2 , star = 7
                            // i = 3 , star = 5
                            // ( Height * 2 ) - ( i * 2 - 1)
                            // i = 1 , ( 5 * 2 ) - ( 1 * 2 - 1) = 10 - 1 = 9
                            // i = 2 , ( 5 * 2 ) - ( 2 * 2 - 1) = 10 - 3 = 7

                            int star = (Height * 2) - (i * 2 - 1);

                            // 공백을 출력
                            for (int k = 0; k < space; k++)
                            {
                                Console.Write(" ");
                            }
                            // 각 행에서 별을 출력
                            for (int j = 0; j < star; j++)
                            {
                                Console.Write("*");
                            }
                            // 각 행이 끝난 후 줄 바꿈
                            Console.WriteLine();
                        }
            */

            // Pyramid
            /*            for(int i = 1; i <= Height; i++)
                        {
                            // 공백을 출력
                            // Height = 5
                            // i = 1 , space = 4
                            // i = 2 , space = 3
                            int space = Height - i;

                            for(int j=0; j < space; j++)
                            {
                                Console.Write(" ");
                            }

                            // 각 행에서 별을 출력
                            // Height = 5
                            // i = 1 , star = 1
                            // i = 2 , star = 3
                            // i = 3 , star = 5
                            int star = i * 2 - 1;

                            for(int j = 0; j < star; j++)
                            {
                                Console.Write("*");
                            }


                            // 각 행이 끝난 후 줄 바꿈
                            Console.WriteLine();
                        }*/


            // i = 1 : j 1번 실행
            // i = 2 : j 2번 실행
            // i = 3 : j 3번 실행
            // ...
            // i = n : j n번 실행

            /*
                        for (int i = 1; i <= Height; i++)
                        {
                            // Height = 3
                            // i = 1 : k 2번 실행 ( 3 - 1 ) , j 1번 실행 ( 1 )
                            // i = 2 : k 1번 실행 ( 3 - 2 ) , j 2번 실행 ( 2 )
                            // i = 3 : k 0번 실행 ( 3 - 3 ) , j 3번 실행 ( 3 )

                            // 공백을 출력
                            for (int k = 0; k< Height - i; k++)
                            {
                                Console.Write(" ");
                            }

                            // 각 행에서 별을 출력
                            for (int j = 0; j < i; j++)
                            {
                                Console.Write("*");
                            }
                            // 각 행이 끝난 후 줄 바꿈
                            Console.WriteLine();
                        }*/

            /*            for (int i = Height; i > 0; i--)
                        {
                            // 각 행에서 별을 출력
                            for (int j = 1; j <= i; j++)
                            {
                                Console.Write("*");
                            }
                            // 각 행이 끝난 후 줄 바꿈
                            Console.WriteLine();
                        }*/

        } while (true);
    }

    public static void Draw(int space, int star)
    {
        for (int i = 0; i < space; i++)
        {
            Console.Write(" ");
        }
        for (int j = 0; j < star; j++)
        {
            Console.Write("*");
        }
        Console.WriteLine();
    }
}