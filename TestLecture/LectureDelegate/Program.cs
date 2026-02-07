using System;
using static System.Console;

public class Program
{
    // 대리자(Delegate) 선언
    public delegate int Calculate(int a, int b);

    public class Calculator
    {
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Subtract(int a, int b)
        {
            return a - b;
        }

        public float Divide(int a, int b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Denominator cannot be zero.");
            }
            return (float)a / b;
        }

        public int Multiply(float a, int b)
        {
            return (int)(a * b);
        }
    }

    // 프로그램 시작점(Entry Point)
    static void Main(string[] args)
    {
        /////////////////////////////////////////////////////////////
        // 대리자(Delegate) 사용 예제

        Calculator calculator = new Calculator();

        // 대리자 인스턴스 생성 및 메서드 할당
        Calculate addDelegate = new Calculate(calculator.Add);
        Calculate subtractDelegate = new Calculate(calculator.Subtract);
        
        //Calculate divideDelegte = new Calculate(calculator.Divide); // 오류 발생: 반환형 불일치

        //Calculate multiplyDelegate = new Calculate(calculator.Multiply); // 오류 발생: 매개변수 불일치

        // 대리자 호출
        int sum = addDelegate(10, 5);
        int difference = subtractDelegate(10, 5);

        WriteLine($"Sum: {sum}");
        WriteLine($"Difference: {difference}");

        WriteLine();
        /////////////////////////////////////////////////////////////
        // Action
        // Action 대리자는 반환 값이 없는 메서드를 참조하는 데 사용됩니다.
        // Action 대리자는 최대 16개의 매개변수를 가질 수 있습니다.
        // 예: Action<int, int>는 두 개의 int 매개변수를 받는 메서드를 참조합니다.

        Action action = () => WriteLine("Action delegate called.");
        action();

        WriteLine();

        Action<int, int> actionWithParams = (x, y) => WriteLine($"Action with parameters called: {x}, {y}");
        actionWithParams(10, 20);

        WriteLine();

        /////////////////////////////////////////////////////////////
        // Func
        // Func 대리자는 반환 값이 있는 메서드를 참조하는 데 사용됩니다.
        // Func 대리자는 최대 16개의 매개변수를 가질 수 있으며, 마지막 매개변수는 반환 형식입니다.
        // 예: Func<int, int, int>는 두 개의 int 매개변수를 받고 int를 반환하는 메서드를 참조합니다.

        Func<int, int, int> func = (x, y) => x + y;
        int result = func(10, 20);

        WriteLine($"Func delegate called. Result: {result}");

        WriteLine();

        Func<int, int , string> funcWithStringReturn = (x, y) => $"Sum is: {x + y}";
        string stringResult = funcWithStringReturn(10, 20);
        WriteLine($"Func with string return called. Result: {stringResult}");
        
        WriteLine();
        /////////////////////////////////////////////////////////////
        // Predicate
        // Predicate 대리자는 단일 매개변수를 받고 bool을 반환하는 메서드를 참조하는 데 사용됩니다.
        // 예: Predicate<int>는 int 매개변수를 받고 bool을 반환하는 메서드를 참조합니다.

        Predicate<int> isEven = (x) => x % 2 == 0;
        bool checkEven = isEven(10);

        WriteLine($"Predicate delegate called. Is 10 even? {checkEven}");
    
        WriteLine();

        Predicate<string> isNullOrEmpty = (str) => string.IsNullOrEmpty(str);
        bool checkString = isNullOrEmpty("");

        WriteLine($"Predicate delegate called. Is string null or empty? {checkString}");

        WriteLine();

        /////////////////////////////////////////////////////////////
        // EventHandler
        // EventHandler 대리자는 이벤트를 처리하는 메서드를 참조하는 데 사용됩니다.
        // 일반적으로 두 개의 매개변수를 가지며, 첫 번째는 이벤트의 발신자이고 두 번째는 이벤트 데이터입니다.

        EventHandler eventHandler = (sender, e) => WriteLine("EventHandler delegate called.");

        // 이벤트 발생 시 대리자 호출
        eventHandler(null, EventArgs.Empty);

        // null 조건부 연산자를 사용하여 대리자 호출
        // Invoke 메서드 사용
        eventHandler?.Invoke(null, EventArgs.Empty);

        // 대리자에 추가 메서드 할당
        eventHandler += (sender, e) => WriteLine("Second EventHandler delegate called.");

        WriteLine();

        // 이벤트 발생 시 대리자 호출
        eventHandler(null, EventArgs.Empty);

        WriteLine();

        // 대리자에 기존 메서드 할당
        eventHandler += OnEventOccurred;

        // 이벤트 발생 시 대리자 호출
        eventHandler(null, EventArgs.Empty);

        WriteLine();

        // 대리자에서 메서드 제거
        eventHandler -= OnEventOccurred;

        // 이벤트 발생 시 대리자 호출
        eventHandler(null, EventArgs.Empty);

        // 대리자 모두 해제
        eventHandler = null;

        eventHandler?.Invoke(null, EventArgs.Empty);
    }

    public static void OnEventOccurred(object sender, EventArgs e)
    {
        WriteLine("Event occurred!");
    }
}
