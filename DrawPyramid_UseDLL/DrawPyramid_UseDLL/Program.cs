using System;
using static System.Console;
using MyClass_Library;

public class Program
{
    static void Main(string[] args)
    {
        Write("Enter the number of rows for the pyramid: ");
        if (int.TryParse(ReadLine(), out int rows) && rows > 0)
        {
            Pyramid.DrawPyramid(rows, true);            
        }
        else
        {
            WriteLine("Please enter a valid positive integer.");
        }

    }
}