using System;

public class LecturStruct
{
    struct PlayerData
    {
        public int Id;
        public string Name;

        public int AttackPower;
        public int Health;

        // 기본 생성자
        public PlayerData() { }

        public PlayerData(int id, string name)
        {
            Id = id;
            this.Name = name;
        }

        public static bool operator >(PlayerData a, PlayerData b)
        {
            if (a.AttackPower > b.AttackPower && a.Health > b.Health)
            {
                return true;
            }

            return false;
        }

        public static bool operator <(PlayerData a, PlayerData b)
        {
            if (a.AttackPower < b.AttackPower && a.Health < b.Health)
            {
                return true;
            }

            return false;
        }

        public static bool operator <=(PlayerData a, PlayerData b)
        {
            return false;
        }

        public static bool operator >=(PlayerData a, PlayerData b)
        {
            return false;
        }

        public static bool operator ==(PlayerData a, PlayerData b)
        {
            return false;
        }

        public static bool operator !=(PlayerData a, PlayerData b)
        {
            return false;
        }
    }

    static void Main(string[] args)
    {
        PlayerData tempPlayer1 = new PlayerData();

        tempPlayer1.Id = 1001;
        tempPlayer1.Name = "Illidan";
        tempPlayer1.AttackPower = 100;
        tempPlayer1.Health = 100;

        PlayerData tempPlayer2 = new PlayerData(1002, "Arthus");
        tempPlayer2.Health = 120;
        tempPlayer2.AttackPower = 90;

        if (tempPlayer1.Id > tempPlayer2.Id)
        {
            Console.WriteLine($"{tempPlayer1.Id} > {tempPlayer2.Id}");
        }

        if (tempPlayer1 < tempPlayer2)
        {
            Console.WriteLine($"{tempPlayer1.Id} < {tempPlayer2.Id}");
        }
    }
}
