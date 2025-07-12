
using System.Collections.Generic;

public class GameData
{
    public string? Word { get; set; }
    public List<string>? Hints { get; set; }

    GameData()
    {
        Hints = new List<string>();
    }

    ~GameData()
    {
        // Destructor to clean up resources if needed
        Hints?.Clear();
    }

    public GameData(string? word = null, List<string>? hints = null)
    {
        Word = word;
        Hints = hints ?? new List<string>();
    }

    void AddHint(string hint)
    {
        if (Hints == null)
        {
            Hints = new List<string>();
        }
        Hints.Add(hint);
    }

    string GetHint(int index)
    {
        if (Hints == null || index < 0 || index >= Hints.Count)
        {
            return string.Empty;
        }
        return Hints[index];
    }
}

public class SaveData
{
    public string? PlayerName { get; set; }

    public int PlayCount { get; set; }

    public string? Answer { get; set; }

    SaveData()
    {
        
    }

    ~SaveData()
    {
        // Destructor to clean up resources if needed
        PlayerName = null;
        Answer = null;
    }

    public SaveData(string? playerName = null, int playCount = 0, string? answer = null)
    {
        PlayerName = playerName;
        PlayCount = playCount;
        Answer = answer;
    }
}