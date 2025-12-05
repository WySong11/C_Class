using System;
using System.Collections.Generic;
using static System.Console;

public static class SaveLog
{
    // 로그 메시지 리스트
    static List<string> logMessages = new List<string>();

    // 로그 파일 이름
    const string LogFilePath = "game_log.txt";

    public static void ClearLogs()
    {
        logMessages.Clear();
    }

    public static void AddLog(string message)
    {
        string timestampedMessage = $"{DateTime.Now}: {message}";
        logMessages.Add(timestampedMessage);
    }

    public static void Write()
    {
        try
        {
            System.IO.File.WriteAllLines(LogFilePath, logMessages);
            WriteLine($"로그가 '{LogFilePath}' 파일에 저장되었습니다.");
        }
        catch (Exception ex)
        {
            WriteLine($"로그 저장 중 오류 발생: {ex.Message}");
        }
    }

    public static void WriteLog(string message, ConsoleColor color = ConsoleColor.White )
    {
        // 현재 콘솔 색상 저장
        ForegroundColor = color;

        // 메시지 출력
        WriteLine(message);

        // 로그에 추가
        AddLog(message);
    }
}

