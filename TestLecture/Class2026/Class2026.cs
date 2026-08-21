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



        WarriorClass warrior1 = new WarriorClass(1003, "Tauren", 150, 150, 50);
        WizardClass wiazrd1 = new WizardClass(1004, "Guldan", 80, 80, 100);
        ArcherClass archer1 = new ArcherClass(1005, "Windrunner", 50, 50, 120);

        /*        GameManager.Instance.AddPlayer(warrior1);
                GameManager.Instance.AddPlayer(wiazrd1);*/

        warrior1.SetTargetId(wiazrd1.GetID());
        wiazrd1.SetTargetId(warrior1.GetID());
        archer1.SetTargetId(wiazrd1.GetID());

        Console.WriteLine();

        // Singleton 은 인스턴스를 생성하지 않는다.
        //GameManager tt = new GameManager();

        // Instance 를 통해서만 사용 가능
        GameManager.Instance.PrintPlayerList();


        GameManager.Instance.AttackPlayer(warrior1, wiazrd1);

        GameManager.Instance.AttackPlayer(wiazrd1, warrior1);

        GameManager.Instance.UseSkill();

        GameManager.Instance.UseItem();
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