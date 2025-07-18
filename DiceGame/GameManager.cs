using CSVFileManager;
using JSONFileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class GameDataManager
    {
        private static readonly Lazy<GameDataManager> _instance = new(() => new GameDataManager());
        public static GameDataManager Instance => _instance.Value;
        private GameDataManager() { }
    }
}
