
public class LevelData
{
    public int Level { get; set; }
    
    public int Exp { get; set; }

    public LevelData() { }

    public LevelData(int level, int exp)
    {
        Level = level;
        Exp = exp;
    }
}