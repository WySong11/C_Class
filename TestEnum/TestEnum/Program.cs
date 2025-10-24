using System;
using static System.Console;

namespace TestEnum
{
    // 투수 1
    // 포수 2
    // 1루수 3
    // 2루수 4
    // 3루수 5
    // 유격수 6

    // 1번 변수
    // 2번 변수

    enum BaseBallPosition
    {
        Pitcher = 1,
        Catcher,
        FirstBase,
        SecondBase,
        ThirdBase,
        ShortStop,
        LeftField,
        CenterField,
        RightField
    }

    enum FootballPosition : int
    {
        GoalKeeper = 1,
        Defender,
        MidFielder,
        Forward
    }

    enum ItemType
    {
        None = 0,

        Consumable = 1000,

        Equipment = 2000,        

        Quest = 3000,

        Item = 4000,

        Item_Weapon = 4100,

        Item_Weapon_Sword = 4110,

        Item_Weapon_Sword_Fire = 4111,

        Item_Armor = 4200,

        Item_Armor_Sword = 4210,
    }

    enum Color : byte
    {
        Red = 1,
        Green = 2,
        Blue = 3,
        Black = 4,

        White = 255,

        //Orange = 256,
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            BaseBallPosition position = BaseBallPosition.Catcher;

            WriteLine("Input Position : ");
            string pos = ReadLine();

            if( Enum.TryParse<BaseBallPosition>(pos, out BaseBallPosition result) )
            {
                WriteLine($"{pos} : { result }");
            }

            if(Enum.TryParse<FootballPosition>(pos, out FootballPosition result2) )
            {
                WriteLine($"{pos} : { result2 }");
            }

            WriteLine("-----");

            BaseBallPosition p = BaseBallPosition.ShortStop;
            p = BaseBallPosition.FirstBase;

            WriteLine( p );
            WriteLine( (int)p );

            WriteLine("-----");

            // 암시적 변환
            p = (BaseBallPosition)1;

            WriteLine(p);
            WriteLine("-----");

            int t = (int)BaseBallPosition.RightField;

            WriteLine(t);
            
            WriteLine((BaseBallPosition)t);

            WriteLine("-----");


            switch (Enum.Parse(typeof(BaseBallPosition), pos) )
            {
                case BaseBallPosition.Pitcher:
                    WriteLine("Pitcher");
                    break;

                case BaseBallPosition.Catcher:
                    WriteLine("Catcher");
                    break;

                case BaseBallPosition.FirstBase:
                    WriteLine("FirstBase");
                    break;

                case BaseBallPosition.SecondBase:
                    WriteLine("SecondBase");
                    break;

                case BaseBallPosition.ThirdBase:
                    WriteLine("ThirdBase");
                    break;

                case BaseBallPosition.ShortStop:
                    WriteLine("ShortStop");
                    break;

                case BaseBallPosition.LeftField:
                    WriteLine("LeftField");
                    break;

                case BaseBallPosition.CenterField:
                    WriteLine("CenterField");
                    break;

                case BaseBallPosition.RightField:
                    WriteLine("RightField");
                    break;

                default:
                    break;

            }
        }
    }
}
