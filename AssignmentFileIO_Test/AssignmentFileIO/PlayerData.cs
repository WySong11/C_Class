
public class PlayerData
{
    public string Name { get; set; }
    public int Level { get; set; }
    public int Exp { get; set; }
    
    public PlayerData() { }

    public PlayerData(string name, int level, int exp)
    {
        Name = name;
        Level = level;
        Exp = exp;
    }
}

