using System;
using System.Diagnostics;
using System.IO;
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

/*        if (File.Exists("log.txt"))
            File.Delete("log.txt");*/

        Trace.Listeners.Clear(); // 기존 리스너 제거

        string logFileName = $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
        Trace.Listeners.Add(new TextWriterTraceListener(logFileName));
        Trace.AutoFlush = true; // 로그가 즉시 출력되도록 설정

        // consoleTraceListener는 콘솔에 출력하는 리스너입니다.
        // 콘솔에 출력하려면 ConsoleTraceListener를 추가합니다.
        Trace.Listeners.Add(new ConsoleTraceListener()); // 콘솔에도 출력

        // DefaultTraceListener는 디버깅 도구나 출력 창에 로그를 출력합니다.
        // 별도로 추가하지 않아도 됩니다.
        // 하지만 필요하다면 아래와 같이 추가할 수 있습니다.
        Trace.Listeners.Add(new DefaultTraceListener()); // 기본 리스너 추가

        // 콘솔 색상 설정
        Console.ForegroundColor = ConsoleColor.White; // 기본 색상 설정
        Console.BackgroundColor = ConsoleColor.Black; // 배경색 설정
        Console.Clear(); // 콘솔 초기화

        isInitialized = true;
    }

    // / <summary>
    // 로그 메시지를 기록합니다.
    // / </summary>
    // / <param name="level">로그 레벨</param>
    // / <param name="message">로그 메시지</param>
    // / <param name="memberName">호출한 메서드 이름</param>
    // / <param name="filePath">호출한 파일 경로</param>
    // / <param name="lineNumber">호출한 줄 번호</param>
    /// <summary>
    ///     
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