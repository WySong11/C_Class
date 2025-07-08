using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

public enum LogLevel
{
    Debug,
    Info,
    Warning,
    Error,
    Critical
}

public static class Logger
{
    static bool isInitialized = false;

    public static void Initialize()
    {
        if (isInitialized) return;
        // 초기화 로직이 필요하다면 여기에 추가

        Trace.Listeners.Clear(); // 기존 리스너 제거

        string logFileName = $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
        Trace.Listeners.Add(new TextWriterTraceListener(logFileName));
        Trace.AutoFlush = true; // 로그가 즉시 출력되도록 설정

        Trace.Listeners.Add(new ConsoleTraceListener()); // 콘솔에도 출력
        Trace.Listeners.Add(new DefaultTraceListener()); // 기본 리스너 추가

        // 콘솔 색상 설정
        Console.ForegroundColor = ConsoleColor.White; // 기본 색상 설정
        Console.BackgroundColor = ConsoleColor.Black; // 배경색 설정
        Console.Clear(); // 콘솔 초기화

        isInitialized = true;
    }

    public static void Log(LogLevel level, string message, 
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string filePath = "",
        [CallerLineNumber] int lineNumber = 0)
    {
        Initialize(); // Logger가 초기화되지 않았다면 초기화

        string fileName = System.IO.Path.GetFileName(filePath);
        string logLevel = level.ToString().ToUpper();
        string messagePrefix = $"[{logLevel}] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{fileName}:{lineNumber} - {memberName}] [{message}]";

        switch (level)
        {
            case LogLevel.Debug:
                Console.ForegroundColor = ConsoleColor.White;                
                break;

            case LogLevel.Warning:
                Console.ForegroundColor = ConsoleColor.Yellow;
                break;

            case LogLevel.Error:
                Console.ForegroundColor = ConsoleColor.Red;
                break;

        }        
        
        Trace.WriteLine(messagePrefix);
        Console.ResetColor();
    }
}