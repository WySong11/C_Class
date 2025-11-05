using System;
using static System.Console;

namespace TestAsIs
{
    public class Program
    {
        static void Main(string[] args)
        {
            int number = 42;

            if (number is 42)
            {
                WriteLine("The number is 42");
            }
            else
            {
                WriteLine("The number is not 42");
            }

            WriteLine();

            if (number is int)
            {
                WriteLine("The number is a positive integer");
            }
            else
            {
                WriteLine("The number is not a positive integer");
            }


            object obj = "Hello, World!";

            if (obj is string s)
            {
                WriteLine($"The object is a string: {s}");
            }
            else
            {
                WriteLine("The object is not a string");
            }

            //////////////////////////////////////////////////////

            TestClass testClass = new TestClass();

            if (testClass is TestClass)
            {
                WriteLine("testClass is an instance of TestClass");
            }
            else
            {
                WriteLine("testClass is NOT an instance of TestClass");
            }

            Class1 class1 = new Class1();

            if (class1 is TestClass)
            {
                WriteLine("class1 is an instance of TestClass");
            }
            else
            {
                WriteLine("class1 is NOT an instance of TestClass");
            }

            Class2 class2 = new Class2();

            if (class2 is Class2)
            {
                WriteLine("class2 is an instance of Class2");
            }
            else
            {
                WriteLine("class2 is NOT an instance of Class2");
            }

            if (class2 is TestClass)
            {
                WriteLine("class2 is an instance of TestClass");
            }
            else
            {
                WriteLine("class2 is NOT an instance of TestClass");
            }

            string? nullableString = null;

            if (nullableString is null)
            {
                WriteLine("nullableString is null");
            }
            else
            {
                WriteLine("nullableString is not null");
            }

            //////////////////////////////////////////

            object obj2 = "Hello, Pattern Matching!";

            string str = obj as string;

            if (str != null)
            {
                WriteLine($"The object is a string: {str}");
            }
            else
            {
                WriteLine("The object is not a string");
            }

            TestClass testClass2 = new TestClass();

            if (testClass2 as TestClass != null)
            {
                WriteLine("testClass2 is an instance of TestClass");
            }
            else
            {
                WriteLine("testClass2 is NOT an instance of TestClass");
            }

            Class2 testClass3 = new Class2();

            if (testClass3 as TestClass != null)
            {
                WriteLine("testClass3 is an instance of TestClass");
            }
            else
            {
                WriteLine("testClass3 is not an instance of TestClass");
            }

            // Is 로 검사후 as 로 변환
            if ( testClass3 is TestClass)
            {
                TestClass? ttt = testClass3 as TestClass;
            }
            else
            {
                WriteLine("testClass3 is NOT an instance of Class1");
            }
        }
    }
}
