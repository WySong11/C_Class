using System;
using System.Collections.Generic;
using System.Diagnostics;

public static class SaveLog
{
    static List<string> m_logMessages = new List<string>();

    const string LogFilePath = "game_log.txt";

    public static void ClearLogs()
    {
        m_logMessages.Clear();
    }

    public static void AddLog(string message)
    {
        string timestampedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
        m_logMessages.Add(timestampedMessage);        
    }

    public static void Write()
    {
        try
        {
            // 로그 메시지를 파일에 저장
            // AppendAllLines은 파일이 없으면 새로 만들고, 있으면 이어쓰기
            System.IO.File.AppendAllLines(LogFilePath, m_logMessages);
            m_logMessages.Clear();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Log - Failed to save logs: {ex.Message}");
        }
    }

    public static void WriteLog(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);

        m_logMessages.Add(message);
    }
}
