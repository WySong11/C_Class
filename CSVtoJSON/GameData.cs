
public class GameData
{
    public int level { get; set; }
    public int hp { get; set; }
    public int atk { get; set; }

    public GameData()
    {
        // Default constructor
    }

    public GameData(int level, int hp, int atk)
    {
        this.level = level;
        this.hp = hp;
        this.atk = atk;
    }
}

public class SaveData
{
    public string? playerName { get; set; }
    public int level { get; set; }

    public SaveData()
    {
        // Default constructor
    }

    public SaveData(string playerName, int level)
    {
        this.playerName = playerName;
        this.level = level;
    }
}