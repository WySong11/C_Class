using System;

public class Output
{
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

    public static void PrintDiamondLoop(int height)
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
}