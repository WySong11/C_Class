using System;
using static System.Console;

public class Program
{
    public enum ETypePyamid
    {
        None = 0,
        Increase = 1,
        Decrease = 2,
        ReverseIncrease = 3,
        ReverseDecrease = 4,

        PyramidIncrease,
        PyramidDecrease,

        Diamond,

        Max,
    }

    static void Main(string[] args)
    {
        int height;
        ETypePyamid type;

        do
        {
            height = GetHeight();
            type = GetPyramidType();

            switch(type)
            {
                case ETypePyamid.Increase:
                case ETypePyamid.Decrease:
                case ETypePyamid.ReverseIncrease:
                case ETypePyamid.ReverseDecrease:
                    PrintHalfPyramid(height, type);
                    break;

                case ETypePyamid.PyramidIncrease:
                    PrintPyramid(height, false);                    
                    break;

                case ETypePyamid.PyramidDecrease:
                    PrintPyramid(height, true);
                    break;

                case ETypePyamid.Diamond:
                    //PrintDiamond(height);
                    //PrintDiamondOneLoop(height);
                    //PrintDiamondLoop_v0(height);
                    PrintDiamondLoop_v1(height);
                    break;
            }

        } while (RestartProgram());


    }

    public static int GetHeight()
    {
        Write("Enter the height of the diamond (odd number): ");
        int height;
        while (!int.TryParse(ReadLine(), out height) || height <= 0 || height % 2 == 0)
        {
            Write("Invalid input. Please enter a positive odd number: ");
        }
        return height;
    }

    public static ETypePyamid GetPyramidType()
    {
        WriteLine("Select the pyramid type:");
        WriteLine("1. Increase");
        WriteLine("2. Decrease");
        WriteLine("3. Reverse Increase");
        WriteLine("4. Reverse Decrease");
        WriteLine("5. Pyramid Increase");
        WriteLine("6. Pyramid Decrease");
        WriteLine("7. Diamond");
        Console.Write("Enter your choice (1-7): ");
        int choice;
        while (!int.TryParse(Console.ReadLine(), out choice) || choice <= (int)ETypePyamid.None || choice >= (int)ETypePyamid.Max)
        {
            Console.Write("Invalid choice. Please enter a number between 1 and 4: ");
        }
        return (ETypePyamid)choice;
    }

    public static bool RestartProgram()
    {
        Console.Write("Do you want to draw another diamond? (y/n): ");
        string? response = Console.ReadLine()?.Trim().ToLower();
        return response == "y" || response == "yes";
    }

    public static void Draw(int space, int star)
    {
        // Print leading spaces
        Write(new string(' ', space));
        // Print stars
        WriteLine(new string('*', star));
    }

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
    private static void DrawLoop(int space, int star)
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

    public static void PrintPyramid(int height, bool reverse)
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

    public static void PrintDiamond(int height)
    {
        int spaces = height / 2;
        int stars = 1;
        // Print the upper part of the diamond
        for (int i = 0; i < height / 2 + 1; i++)
        {
            Console.Write(new string(' ', spaces));
            Console.WriteLine(new string('*', stars));
            spaces--;
            stars += 2;
        }
        // Reset spaces and stars for the lower part
        spaces = 1;
        stars = height - 2;
        // Print the lower part of the diamond
        for (int i = 0; i < height / 2; i++)
        {
            Console.Write(new string(' ', spaces));
            Console.WriteLine(new string('*', stars));
            spaces++;
            stars -= 2;
        }
    }

    public static void PrintDiamondOneLoop(int height)
    {
        int spaces = height / 2;
        int stars = 1;
        // Print the diamond using one loop
        for (int i = 0; i < height; i++)
        {
            Console.Write(new string(' ', spaces));
            Console.WriteLine(new string('*', stars));
            if (i < height / 2)
            {
                spaces--;
                stars += 2;
            }
            else
            {
                spaces++;
                stars -= 2;
            }
        }
    }

    public static void PrintDiamondLoop_v0(int height)
    {
        if (height < 1) return;
        if (height % 2 == 0) height += 1;

        int halfHeight = height / 2;
        int center = halfHeight + 1;

        for (int i = 1; i <= height; i++)
        {
            int spaces = Math.Abs(center - i);
            int stars = height - 2 * spaces;

            Console.Write(new string(' ', spaces));
            Console.WriteLine(new string('*', stars));
        }
    }

    public static void PrintDiamondLoop_v1(int height)
    {
        if (height < 1) return;

        // 다이아몬드는 보통 홀수가 깔끔합니다. (짝수면 1 올려서 홀수로 보정)
        if (height % 2 == 0) height += 1;

        int half = height / 2; // 예: 7이면 3 (가운데 인덱스)

        for (int i = 0; i < height; i++)
        {
            int dist = Math.Abs(half - i);       // 가운데에서 얼마나 떨어졌는지
            int spaces = dist;                   // 공백
            int stars = height - 2 * dist;      // 별 (항상 1 이상)

            Console.Write(new string(' ', spaces));
            Console.WriteLine(new string('*', stars));
        }
    }
}

