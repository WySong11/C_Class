using System;
using static System.Console;

public class Program
{  
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // TestClass의 인스턴스 생성
        TestClass testClass1 = new TestClass();

        TestClass testClass2 = new TestClass(11);

        TestClass testClass3 = new TestClass("MyTest");

        // public 필드에 접근 가능
        testClass1.id = 1;

        // 필드에 직접 접근 불가(컴파일 오류)
        // private 필드이기 때문
        //testClass1.health = 111;
        //testClass1.maxHealth = 888;

        // protected 필드이기 때문에 접근 불가
        //testClass1.shield = 50;

        // internal 필드이기 때문에 접근 가능
        testClass1.speed = 10;

        testClass1.id = 100;

        // Property는 public이기 때문에 접근 가능
        // 읽기 가능. Get 접근자
        int attackPower = testClass1.AttackPower;

        // 쓰기 불가. Set 접근자가 private이기 때문에 컴파일 오류
        //testClass1.AttackPower = 50; // set 접근자가 private이기 때문에 컴파일 오류

        // static class는 인스턴스 생성 불가
        //TestClassExtensions tempclass = new TestClassExtensions();

        // 확장 메서드 호출
        TestClassExtensions.PrintInfo(testClass1);

        testClass1.CreateMonsters(5);

        Goblin goblin1 = new Goblin(1);

        //GC.Collect(); // 강제 가비지 컬렉션 호출
        //GC.WaitForPendingFinalizers(); // 소멸자 호출 대기        

        Character ch1 = new Character();
        Player p1 = new Player();

        ch1.UseItem();
        p1.UseItem();



        if( ch1 is IGetName )
        {
            ch1.GetName();
        }

        // 인터페이스 상속 가능
        if( p1 is IGetName )
        {
            p1.GetName();
        }

        if( testClass1 is IGetName )
        {
            testClass1.GetName();
        }

        Monster mm = new Monster();

        if(mm is IGetName)
        {
            // interface 구현 안됨
            //mm.GetName();
        }

        Character ch2 = p1 as Character;

        if (ch2 != null)
        {
            WriteLine("as 연산자 성공");
        }


        Player p2 = ch1 as Player;

        if (p2 == null)
        {
            WriteLine("as 연산자 실패");
        }

        
    }
}

public interface IGetName
{
    void GetName();
}

public class Character : IGetName
{
    // 가상 메서드
    // 파생 클래스에서 재정의(오버라이드) 가능
    // 파생 클래승에서 재정의하지 않으면 이 구현이 사용됨
    // 기본적으로 virtual 메서드는 비어있는 구현을 제공하는 경우가 많음
    public virtual void UseItem()
    {
        Console.WriteLine("Character UseItem 호출");
    }

    public void GetName()
    {
        Console.WriteLine("Character GetName 호출");
    }
}

public class Player : Character
{
    // 오버라이드 메서드
    // base 클래스의 가상 메서드를 재정의
    // 반드시 virtual 메서드와 동일한 시그니처여야 함
    public override void UseItem()
    {
        //base.UseItem(); // base 클래스의 UseItem 호출

        Console.WriteLine("Player UseItem 호출");
    }
}

public class  TestClass : IGetName
{
    List<Monster> monsters = new List<Monster>();

    public int id;

    // 필드. 기본 private
    int health;

    private int maxHealth = 100;

    // protected 필드: 상속받은 클래스에서 접근 가능
    protected int shield;

    // internal 필드: 동일 어셈블리 내에서 접근 가능
    // 동일 프로젝트 내에서 접근 가능
    internal int speed;

    // Property
    // get; private set; : 외부에서는 읽기만 가능, 내부에서만 쓰기 가능
    public int AttackPower { get; private set; } = 10;

    // 생성자
    public TestClass()
    {
        WriteLine("TestClass 생성자 호출");
    }

    public TestClass(int inHealth)
    {
        health = inHealth;
        WriteLine("TestClass 매개변수 생성자 호출");
    }

    public TestClass(string name)
    {
        WriteLine("TestClass 매개변수 생성자 호출 : " + name);
    }


    // 소멸자
    ~TestClass()
    {
        WriteLine("TestClass 소멸자 호출");
    }

    // health 필드에 대한 Getter 메서드
    // health 필드는 private이기 때문에 외부에서 직접 접근 불가
    // 이 메서드를 통해 값을 읽을 수 있음
    public void SetHealth(int inHealth)
    {
        if (inHealth < 0)
        {
            health = 0;
        }
        else if (inHealth > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health = inHealth;
        }
    }

    public void CreateMonsters(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Monster monster = new Monster();
            monster.id = i;
            monsters.Add(monster);
        }
    }

    public void GetName()
    {
        Console.WriteLine("TestClass GetName 호출");
    }
}

public static class TestClassExtensions
{
    // 확장 메서드
    public static void PrintInfo(this TestClass testClass)
    {
        Console.WriteLine($"TestClass Info - ID: {testClass.id}, Speed: {testClass.speed}");
    }
}

public class Monster
{

    public int id;

    int health;

    protected int maxHealth;

    public Monster()
    {
        Console.WriteLine("Monster 생성자 호출");
    }

    public Monster(int inId)
    {
        id = inId;
        Console.WriteLine("Monster 매개변수 생성자 호출");
    }
}

public class Goblin : Monster
{
    public Goblin()
    {
        Console.WriteLine("Goblin 생성자 호출");
    }

    public Goblin(int inId) : base(inId)
    {
        Console.WriteLine("Goblin 매개변수 생성자 호출 => " + id);

        // 접근 불가: Monster의 private 필드이기 때문
        //Console.WriteLine("Goblin Health => " + health);

        // 접근 가능: Monster의 protected 필드이기 때문
        Console.WriteLine("Goblin MaxHealth => " + maxHealth);
    }
}

