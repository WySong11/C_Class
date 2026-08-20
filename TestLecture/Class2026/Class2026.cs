using System;
using System.Collections.Generic;
using TestClass;

public class Class2026
{
    static void Main(string[] args)
    {
        // 클래스의 인스턴스를 생성
        // 인스턴스의 이름은 Player1
        /*        PlayerClass Player1 = new PlayerClass();

                Player1.PrintData();

                // Private 에는 직접 접근해서, 값을 대입할 수 없음.
                //Player1.m_id = 1001;

                // Public 변수에는 직접 값을 넣을수 있다.
                Player1.m_moveSpeed = 50;

                Player1.InitPlayerData(1001, "DarthVader", 200, 200, 100);

                Player1.PrintData();

                // 생성자를 통한 클래스 인스턴스 생성
                PlayerClass Player2 = new PlayerClass(1002, "Skywalker", 100, 100, 75);

                Player2.PrintData();

                // Private 함수에는 직접 접근할 수 없음.
                //Player2.SetHp(0);

                Player1.TakeDamage(50);

                Player1.TakeDamage(70);

                Player1.TakeHeal(250);

                Player1.TakeDamage(200);*/

        // Player List
        List<PlayerClass> PlayerList = new List<PlayerClass>();

        WarriorClass warrior1 = new WarriorClass(1003, "Tauren", 150, 150, 50);
        WizardClass wiazrd1 = new WizardClass(1004, "Guldan", 80, 80, 100);

        // 생성된 Player 들을 List 에 추가
        PlayerList.Add(warrior1);
        PlayerList.Add(wiazrd1);

        Console.WriteLine();

        foreach (PlayerClass playerClass in PlayerList)
        {
            playerClass.PrintInfo();
        }
    }

    static void Create()
    {

        //PlayerClass player1 = new PlayerClass(1002, "Skywalker", 100, 100, 75);

        WarriorClass warrior1 = new WarriorClass(1003, "Tauren", 150, 150, 50);

        WizardClass wiazrd1 = new WizardClass(1004, "Guldan", 80, 80, 100);

        Console.WriteLine();

        //player1.TakeDamage(10);

        warrior1.TakeDamage(20);

        warrior1.Attack();

        wiazrd1.Attack();
    }
}