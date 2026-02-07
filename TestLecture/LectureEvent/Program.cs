
using System;
using System.Collections.Generic;
using static System.Console;

public class MyButton
{
    // 이벤트 선언
    public event EventHandler Clicked;
    // 클릭 메서드
    public void Click()
    {
        // Clicked 에 구독자가 있을 때만 호출
        /*if (Clicked != null)
        {
            Clicked.Invoke(this, EventArgs.Empty);
        }*/

        // 또는 null 조건부 연산자 사용
        Clicked?.Invoke(this, EventArgs.Empty);
    }

    public void ClearEventHandlers()
    {
        Clicked = null;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        // 인스턴스 생성   
        MyButton button = new MyButton();

        // 이벤트 구독
        button.Clicked += Button_Clicked;

        // 클릭 메서드 호출
        button.Click();

        WriteLine();

        button.Clicked += OnEventOccurred;

        // 다시 클릭 메서드 호출
        button.Click();

        WriteLine();

        button.Clicked -= OnEventOccurred;

        // 다시 클릭 메서드 호출
        button.Click();

        // 이벤트 핸들러 제거
        button.ClearEventHandlers();

        /////////////////////////////////////////////////////////
        ///

        WriteLine();

        var userProcessor = new UserProcessor();

        userProcessor.UserCreated += OnUserCreated;

        userProcessor.CreateUser("Alice", 30);
        userProcessor.CreateUser("Bob", 25);
        userProcessor.ListUsers();
    }

    private static void Button_Clicked(object? sender, EventArgs e)
    {
        WriteLine("Button was clicked!");
    }

    private static void OnEventOccurred(object? sender, EventArgs e)
    {
        WriteLine("OnEventOccurred method called.");
    }

    private static void OnUserCreated(object? sender, UserEventArgs e)
    {
        WriteLine($"User created: {e.Username}, Age: {e.Age}");
    }
}

public class UserEventArgs : EventArgs
{
    public string Username { get; set; }

    public int Age { get; }

    public UserEventArgs(string username, int age)
    {
        Username = username;
        Age = age;
    }
}

public class UserProcessor
{
    List<UserEventArgs> users = new List<UserEventArgs>();

    public event EventHandler<UserEventArgs> UserCreated;

    public void CreateUser(string username, int age)
    {
        // 사용자 생성 로직 (예: 데이터베이스에 저장 등)

        users.Add(new UserEventArgs(username, age));

        // UserCreated 이벤트 발생
        UserCreated?.Invoke(this, new UserEventArgs(username, age));
    }

    public void ListUsers()
    {
        foreach (var user in users)
        {
            WriteLine($"User: {user.Username}, Age: {user.Age}");
        }
    }
}