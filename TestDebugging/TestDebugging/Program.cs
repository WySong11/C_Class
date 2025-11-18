using System;
using static System.Console;


public class Program
{
    static void Main(string[] args)
    {
        for (int i = 0; i < 100; i++)
        {
            DoSomething(i);
        }
    }

    static void DoSomething(int value)
    {
        WriteLine($"Processing value: {value}");
    }
}