namespace DiceGame
{
    public class GameData
    {
        public int level;
        public int hp;
        public int attackPower;
        public int attackSpeed;
        public GameData(int level = 0, int hp = 0, int atkpwr = 0, int atkspd = 0)
        {
            this.level = level;
            this.hp = hp;
            this.attackPower = atkpwr;
            this.attackSpeed = atkspd;
        }
    }
}
