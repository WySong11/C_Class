using System;
using static System.Console;


public class Program
{
    public delegate int PerformCalculation(int x, int y);

    public class Calculator
    {
        public int Add(int x, int y) => x + y;
        public int Multiply(int x, int y) => x * y;
    }    

    static void Main(string[] args)
    {
        Calculator cal = new Calculator();

        PerformCalculation addDel = new PerformCalculation(cal.Add);

        int result = addDel(10, 20);

        WriteLine($"Addition Result: {result}");

        WriteLine();

        /////////////////////////////////////////////////////////////

        PerformCalculation mulDel = new PerformCalculation(cal.Multiply);

        result = mulDel(10, 20);

        WriteLine($"Multiplication Result: {result}");

        ////////////////////////////////////////////////////////////        
        // Action
        // 반환값이 없는 메서드를 참조하는 데 사용되는 대리자입니다.
        // 매개 변수는 없거나 하나 이상일 수 있습니다.

        WriteLine();

        Action action = () => WriteLine("Hello from Action delegate!");
        action();

        WriteLine();

        Action<int , string> action1 = (num, str) => WriteLine($"Number: {num}, String: {str}");
        action1(42, "Test");

        WriteLine();

        ////////////////////////////////////////////////////////////
        // Func
        // 반환값이 있는 메서드를 참조하는 데 사용되는 대리자입니다.
        // 매개 변수는 없거나 하나 이상일 수 있습니다.

        Func<int, int, int> funcDel = (x, y) => x - y;
        int resultFunc = funcDel(30, 15);

        WriteLine($"Subtraction Result using Func delegate: {resultFunc}");

        WriteLine();

        Func<int, int, string> funcDel2 = (x, y) => $"The sum is: {x + y}";
        string resultFunc2 = funcDel2(5, 10);

        WriteLine(resultFunc2);
        WriteLine();

        ////////////////////////////////////////////////////////////
        // Predicate
        // bool 값을 반환하는 메서드를 참조하는 데 사용되는 대리자입니다.
        // 매개 변수는 하나만 있을 수 있습니다.

        Predicate<int> isEven = num => num % 2 == 0;

        int testNumber = 4;
        bool isTestNumberEven = isEven(testNumber);

        WriteLine($"{testNumber} is even: {isTestNumberEven}");
        WriteLine();

        ////////////////////////////////////////////////////////////
        // EventHandler
        // 이벤트를 처리하는 메서드를 참조하는 데 사용되는 대리자입니다.
        // 매개 변수는 두 개이며 반환값은 없습니다.


        ////////////////////////////////////////////////////////////
        // Delegate with Callback

        TestDelegate(OnDone);

    }

    public delegate void DoneCallback(string message);

    public static void TestDelegate(DoneCallback callback )
    {
        for(int i = 0; i < 5; i++)
        {
            if( i == 3 )
            {
                callback("중간 작업 완료!");
            }

            WriteLine($"TestDelegate Method: {i}");
        }

        callback("TestDelegate 작업이 완료되었습니다.");
    }

    static void OnDone(string message)
    {
        WriteLine($"DoneCallback invoked with message: {message}");
    }

}
