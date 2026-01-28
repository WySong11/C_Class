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
    }
}

public class  TestClass
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