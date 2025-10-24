using System;
using static System.Console;

namespace TestMethod
{
    public class Program
    {
        // Main 시작 함수
        static void Main(string[] args)
        {
            // 1
            WriteLine("Hello, World!");

            // 2
            Test();

            // 6
            InternalMethod();

            int x = 3;
            int y = 4;

            WriteLine($"Before AddNumbers: x = {x}, y = {y}");

            AddNumbers(x, y);


            WriteLine($"After AddNumbers: x = {x}, y = {y}");


            int t = GetAddNumbers(3, 4);

            WriteLine(t);

            float s = GetAddNumbers(2.7f, 4.5f);

            WriteLine(s);

            bool b = GetAddNumbers(-1.0, 2.0);

            WriteLine(b);

            string str = GetAddNumbers("1", "23");

            WriteLine(str);

            int testRef = 0;

            WriteLine($"Befroe : {testRef}");

            UseRef (ref testRef);

            WriteLine($"After : {testRef}");

            int testOut;

            UseOut (out testOut);

            // 매개변수가 2개인 경우, 기본값을 모두 사용
            int aa = testParam(3, 1);

            WriteLine(aa);

            // Tutple 반환
            // 7.2 이상에서 지원

            (int Id, string Name) person = (1, "testName" );

            WriteLine($"Id : {person.Id}, Name : {person.Name}");

            (int Id, string Name) person2 = GetPerson();

            WriteLine($"Id : {person2.Id}, Name : {person2.Name}");
        }


        static public (int, string) GetPerson()
        {
            return (2, "testBody");
        }

        static public int testParam( int a, int b, int c = 0 )
        {
            return a + b;
        }

        // ref : 매개변수를 참조로 전달
        // 매개변수를 전달 전에 반드시 초기화 해야 함
        // 큰 구조체를 전달할 때 유용
        static public void UseRef(ref int number)
        {
            number += 10;
        }

        // out : 매개변수를 출력으로 전달
        // 매개변수를 함수 내에서 반드시 초기화 해야 함
        static public void UseOut(out int number)
        {
            number = 30;
            number += 10;
        }

        // 누구나 접근 가능한 함수
        static public void Test()
        {
            // 3
            WriteLine("This is a test method.");

            // 4
            PrivateMethod();
        }

        // 클래스 내부에서만 접근 가능한 함수
        static private void PrivateMethod()
        {
            // 5
            WriteLine("This is a private method.");
        }

        // 같은 어셈블리 내에서 접근 가능한 함수
        // (예: 동일 프로젝트 내에서 접근 가능)
        static internal void InternalMethod()
        {
            WriteLine("This is an internal method.");
        }

        // 상속받은 클래스에서 접근 가능한 함수
        static protected void ProtectedMethod()
        {
            WriteLine("This is a protected method.");

            return;
        }

        static public void AddNumbers(int a, int b)
        {
            a = 10;
            b = 20;

            int sum = a + b;
            WriteLine($"The sum of {a} and {b} is {sum}.");
        }

        static public int GetAddNumbers(int a, int b)
        {
            return a + b;
        }

        static public float GetAddNumbers(float a, float b)
        {
            return a + b;
        }

        static public bool GetAddNumbers(double a, double b)
        {
            return a + b > 0;
            //return a + b;
        }

        static public string GetAddNumbers(string a, string b)
        {
            return a + b;

            //WriteLine(a + b);
        }
    }
}
