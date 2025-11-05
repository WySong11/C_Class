using System;
using static System.Console;

public class MyButton
{
    public event EventHandler Clicked;

    public void MouseButtonDown()
    {
        if (Clicked != null)
        {
            Clicked.Invoke(this, EventArgs.Empty);
        }

        // ? : nullable 
        Clicked?.Invoke(this, EventArgs.Empty);
    }

    public void ClearAllEventHandlers()
    {
        Clicked = null;
    }

    // 사용자 정의 EventArgs 클래스
    public class UserEventArgs : EventArgs
    {
        public string Name { get; set; }
        public int Age { get; }

        public UserEventArgs(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public class UserProcessor
    {
        public event EventHandler<UserEventArgs> UserCreated;

        public void CreateUser(string name, int age)
        {
            Console.WriteLine($"Creating user: {name}, Age: {age}");
            
            UserCreated?.Invoke(this, new UserEventArgs(name, age));
        }

    }


    public class Program
    {
        static void Main(string[] args)
        {
            // 인스턴스 생성
            MyButton button = new MyButton();

            // 이벤트 핸들러 등록
            button.Clicked += Button_Clicked;

            // 또 다른 이벤트 핸들러 등록
            button.Clicked += Test;

            // 마우스 버튼 다운 시뮬레이션
            button.MouseButtonDown();

            WriteLine();

            // 이벤트 핸들러 해제
            button.Clicked -= Test;

            button.MouseButtonDown();

            WriteLine();

            // 이벤트 핸들러 해제
            button.Clicked -= Button_Clicked;

            WriteLine();

            button.MouseButtonDown();


            // 이벤트 핸들러 등록
            button.Clicked += Button_Clicked;

            // 또 다른 이벤트 핸들러 등록
            button.Clicked += Test;

            button.ClearAllEventHandlers();

            WriteLine();

            button.MouseButtonDown();

            ////////////////////////////////////////////////////////////////////////////////
            // UserProcessor와 UserEventArgs 사용 예제

            WriteLine();

            var userProcessor = new UserProcessor();

            // UserCreated 이벤트 핸들러 등록
            userProcessor.UserCreated += OnUserCreated;

            // 사용자 생성
            userProcessor.CreateUser("Alice", 30);

        }

        static void OnUserCreated(object? sender, UserEventArgs e)
        {
            WriteLine($"User created: Name = {e.Name}, Age = {e.Age}");
        }

        private static void Button_Clicked(object? sender, EventArgs e)
        {
            WriteLine("Button was clicked!");
        }

        public static void Test(object? sender, EventArgs e)
        {
            WriteLine("Test method called.");
        }
    }
}
