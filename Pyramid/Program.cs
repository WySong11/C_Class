using System;
using static System.Console;
using Common;
using Pyramid;
using static HalfPyramid.HalfPyramid;
using Pyramid.Test;

namespace Main
{
    // public : 다른 프로젝트에서도 접근 가능한 클래스입니다.
    public class Program
    {  
        public static void Main(string[] args)
        {
            ETypePyamid eTypePyamid = ETypePyamid.None;
            int Height = 0;
            bool bHalf = false;
            bool restart;

            // 클래스  Pyramid의 인스턴스를 생성하고 PrintPyramid 메서드를 호출합니다.
            Pyramid.Pyramid pyramid = new Pyramid.Pyramid();

            // TestDelegate 클래스의 인스턴스를 생성합니다.
            TestDelegate testDelegate = new TestDelegate();

            do
            {
                // ref : 이미 초기화된 변수를 참조로 전달. 메서드 내에서 초기화하지 않아도 됨.
                Selection(ref bHalf, ref eTypePyamid, ref Height);

                if (bHalf)
                {
                    // static : 클래스의 인스턴스 없이 호출할 수 있는 메서드로, HalfPyramid 클래스의 PrintHalfPyramid 메서드를 호출합니다.
                    PrintHalfPyramid(Height, eTypePyamid);
                    
                    //HalfPyramid.HalfPyramid.Draw(0, 5);
                }
                else
                {                    
                    pyramid.PrintPyramid(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);

                    //pyramid.Draw(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);

                    /*

                    // pyramidMethod : Pyramid 클래스의 PrintPyramid 메서드를 대리자에 할당합니다.
                    testDelegate.pyramidMethod += pyramid.PrintPyramid;

                    // pyramidMethod : 대리자에 람다식을 할당하여 PrintPyramid 메서드를 호출합니다.
                    testDelegate.pyramidMethod = (int h, bool r) => pyramid.PrintPyramid(h, r);

                    // pyramidMethod.Invoke : 대리자를 호출하여 PrintPyramid 메서드를 실행합니다.
                    // ? : 널 조건부 연산자로, pyramidMethod가 null이 아닐 때만 호출합니다.
                    testDelegate.pyramidMethod?.Invoke(Height, eTypePyamid == ETypePyamid.Increase || eTypePyamid == ETypePyamid.ReverseIncrease ? false : true);                    */
                }

                // out : 초기화되지 않은 변수를 참조로 전달. 메서드 내에서 반드시 초기화해야 함.
                Restart(out restart);

            } while (restart == true);

            // 대리자에서 메서드를 제거합니다.
            testDelegate.pyramidMethod -= pyramid.PrintPyramid;

            // 대리자를 null로 설정하여 메모리 해제
            testDelegate.pyramidMethod = null; 
        }

        // Restart 메서드는 프로그램을 재시작할지 여부를 묻는 기능을 수행합니다.
        public static void Restart(out bool restart)
        {
            restart = false;

            // try : 블록 내에서 예외가 발생할 수 있는 코드를 실행합니다.
            try
            {
                WriteLine("\nDo you want to restart? (Y/N)");
            }
            // catch : 예외가 발생했을 때 실행할 코드를 정의합니다.

            // catch(FormatException e) : 형식 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(ArgumentNullException e) : null 참조 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(ArgumentException e) : 잘못된 인수가 전달되었을 때 실행할 코드를 정의합니다.
            // catch(ArgumentOutOfRangeException e) : 인수가 범위를 벗어났을 때 실행할 코드를 정의합니다.
            // catch(OutOfMemoryException e) : 메모리 부족 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(IOException e) : 입출력 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(TimeoutException e) : 시간 초과 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(InvalidOperationException e) : 잘못된 작업 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(ArithmeticException e) : 산술 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(NullReferenceException e) : null 참조 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(InvalidCastException e) : 잘못된 형변환 오류가 발생했을 때 실행할 코드를 정의합니다.
            // catch(OverflowException e) : 오버플로우 오류가 발생했을 때 실행할 코드를 정의합니다.
            

            // FormatException은 잘못된 형식의 입력이 있을 때 발생합니다.
            catch (FormatException e)
            {
                WriteLine("Invalid input format: " + e.Message);

                switch(e.HResult)
                {
                    case -2146233033: // FormatException HResult
                        WriteLine("Please enter a valid format.");
                        break;
                    default:
                        WriteLine("An unexpected error occurred.");
                        break;
                }

                restart = false; // Set restart to false if an error occurs
            }
            // catch (Exception e) : 다른 모든 예외가 발생했을 때 실행할 코드를 정의합니다.
            // Exception은 모든 예외의 기본 클래스이므로, 이 블록은 모든 예외를 처리할 수 있습니다.
            catch (Exception e)
            {
                WriteLine("An error occurred: " + e.Message);

                switch (e.HResult)
                {
                    case -2146233088: // ArgumentNullException HResult
                        WriteLine("Input cannot be null. Please enter a valid value.");
                        break;
                    case -2146233089: // ArgumentOutOfRangeException HResult
                        WriteLine("Input is out of range. Please enter a valid value.");
                        break;
                    default:
                        WriteLine("An unexpected error occurred: ");
                        break;
                }
            }
            // finally : try 블록이 성공적으로 실행되었든 예외가 발생했든 항상 실행되는 코드를 정의합니다.
            finally
            {
                string? input = ReadLine()?.Trim().ToUpper(); // Read user input and trim whitespace
                if (input == "Y")
                {
                    restart = true; // Set restart to true if user wants to restart
                    WriteLine("Restarting the program...");
                }
                else if (input == "N")
                {
                    WriteLine("Exiting the program. Goodbye!");
                }
                else
                {
                    WriteLine("Invalid input. Please enter 'Y' or 'N'.");

                    // 다시 입력을 받기 위해 재귀적으로 Restart 메서드를 호출합니다. 
                    Restart(out restart);
                }
            }
        }

        // Selection 메서드는 피라미드의 높이, 유형 및 방향을 선택하는 기능을 수행합니다.
        // half : HalfPyramid 여부를 나타내는 bool 변수입니다.
        // type : ETypePyamid 열거형으로 피라미드의 유형을 나타냅니다.
        // height : 피라미드의 높이를 나타내는 int 변수입니다.
        public static void Selection(ref bool half, ref ETypePyamid type, ref int height )
        {
            height = 0;
            while(height <= 0 || height > Constants.MaxHeight)
            {
                WriteLine("\nEnter the height of the pyramid (1-{0}): ", Constants.MaxHeight);
                string? input = ReadLine();
                if (byte.TryParse(input, out byte h) && h > 0 && h <= Constants.MaxHeight)
                {
                    height = h;
                    break; // Exit loop if valid height is entered
                }
                else
                {
                    WriteLine($"Please enter a valid height between 1 and {Constants.MaxHeight}.");
                }
            }

            half = false; // Reset half to false for the next part of the program
            bool selection = true;
            do
            {
                WriteLine("\nPyramid or HalfPyramid? (1: Pyramid, 2: HalfPyramid)");
                string? choice = ReadLine();

                if (choice == "1")
                {
                    half = false;
                    WriteLine("You selected Pyramid.");
                }
                else if (choice == "2")
                {
                    half = true;
                    WriteLine("You selected HalfPyramid.");
                }
                else
                {
                    WriteLine("Invalid choice. Please select 1 or 2.");
                    selection = false;
                }

            } while(selection == false );

            type = ETypePyamid.None; // Initialize direction
            while (type == ETypePyamid.None)
            {
                WriteLine("\nSelect the direction of the pyramid (1: Increase, 2: Decrease, 3: Revers Increase, 4: Reverse Decrease): ");
                string? input = ReadLine();

                switch(input)
                {
                    case "1":
                        type = ETypePyamid.Increase; // Increase
                        WriteLine("You selected Increase direction.");
                        break;
                    case "2":
                        type = ETypePyamid.Decrease; // Decrease
                        WriteLine("You selected Decrease direction.");
                        break;
                    case "3":
                        type = ETypePyamid.ReverseIncrease; // Reverse Increase
                        WriteLine("You selected Reverse Increase direction.");
                        break;
                    case "4":
                        type = ETypePyamid.ReverseDecrease; // Reverse Decrease
                        WriteLine("You selected Reverse Decrease direction.");
                        break;
                    default:
                        WriteLine("Invalid selection. Please select 1, 2, 3, or 4.");
                        continue; // Continue the loop for valid input
                }
            } 

            return;
        }
    }
}
