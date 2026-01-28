using System;


public class Class2
{
    // 필드
    public int field = 20;
    private int a = 10;
    public int b;

    // 상수 필드
    //public const int b; // 오류: 상수는 반드시 선언과 동시에 초기화해야 함
    // const 필드는 컴파일 타임에 값이 결정되어야 함
    // 따라서 런타임에 값을 할당할 수 없음
    public const int ConstField = 30;

    // static 필드
    // static 필드는 클래스에 속하며 인스턴스와 무관하게 하나만 존재
    // 모든 인스턴스가 동일한 static 필드를 공유
    public static int StaticField = 50;

    // readonly 필드
    // readonly 필드는 선언 시 또는 생성자에서만 값을 할당할 수 있음
    // 이후에는 값을 변경할 수 없음
    public static readonly int ReadonlyField = 100;

    // const vs readonly
    // const: 컴파일 타임에 값이 결정, 변경 불가, 암시적 static
    // readonly: 런타임에 값이 결정 가능, 생성자에서 할당 가능, 인스턴스별로 다를 수 있음

    void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        field = 20;

        //ConstField = 40; // 오류: 상수 필드는 값을 변경할 수 없음

        //test_a = 10; // 오류: 지역 변수는 선언된 메서드 내에서만 접근 가능
    }

    void Test()
    {
        // 지역 변수
        int test_a = 10;

        Console.WriteLine("Test");
    }
}

