using System;

// 변수
// 함수 : Method
// 클래스


public class Program
{
    // public 접근 제한자
    // 누구나 접근 가능
    public static int health = 100; // 체력

    // private 접근 제한자
    // 클래스 내부에서만 접근 가능
    private static int mana = 50;  // 마나

    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        // tempMana 변수에 GetMana 함수의 반환값을 대입
        int tempMana = GetMana();
        Console.WriteLine("변수에 대입 : " + tempMana);

        Console.WriteLine("직접 출력 : " + GetMana());

        Attack();

        for(int i=2; i<=9; i++)
        {
            for(int j=1; j<=9; j++)
            {
                Console.WriteLine("{0} x {1} = {2}", i, j, Multiply(i, j));
            }
        }

        // 리턴값이 있는 함수 호출
        // 받는 쪽이 없으면 리턴값이 버려짐
        // 에러 아님
        Multiply(3, 4);

        // 인자가 두 개인 함수에 인자 하나만 전달하면 에러
        //Multiply(5);

        // 인자가 두 개인 함수에 인자 세 개 전달. 에러
        //Multiply(5, 6, 7);

        // 두 번째 인자에 기본값이 지정된 함수 호출
        Add(8); // 두 번째 인자 생략

        Sum(1, 2, 3);

        int testsum = 2;

        Sum(testsum); // 두 번째, 세 번째 인자 생략

        // out 매개변수 사용 예제
        // out 매개변수는 초기화하지 않아도 됨
        int outNumber;

        // 매개 변수 값을 변경할 수 있음
        TestOut(out outNumber);

        // ref 매개변수 사용 예제
        // ref 매개변수는 함수 호출 전에 반드시 초기화되어 있어야 함
        int refNumber = 0;  // 초기화. 값을 할당한 상태

        // 매개 변수 값을 변경할 수 있음
        TestRef(ref refNumber);

        Console.WriteLine("프로그램 종료");
    }

    // 두 정수를 곱하는 함수
    // int : 정수형 반환값
    // a, b : 매개변수(인자)
    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    // 두 정수를 더하는 함수
    // b 매개변수에 기본값 12 지정
    // 두 번째 인자를 생략하고 호출 가능
    // 마지막 매개변수에만 기본값 지정 가능
    public static int Add(int a, int b = 12)
    {
        return a + b;
    }

    // 세 정수를 더하는 함수
    // c 매개변수에 기본값 5 지정
    // 마지막 매개 변수에 기본값이 있어야, 다른 매개변수에도 기본값 지정 가능    
    public static void Sum(int a = 2, int b = 5 , int c = 12 )
    {
        Console.WriteLine(a + b + c);
    }

    // public 접근 제한자
    // 누구나 접근 가능
    // void : 반환값이 없음
    // static : 정적 함수
    // static이 붙은 함수는 static이 붙은 함수에서만 호출 가능
    // static이 붙지 않은 함수는 인스턴스를 생성해야 호출 가능
    // static 함수에서는 인스턴스 멤버(변수, 함수)에 접근 불가
    public static void Attack()
    {
        Console.WriteLine("공격을 합니다.");

        // 반환값이 없을 때는 return문 생략 가능
        // return을 만나면 함수를 종료
        return; 

        Console.WriteLine("이 코드는 실행되지 않습니다.");
    }

    // private 접근 제한자
    // 클래스 내부에서만 접근 가능
    private void Defend()
    {
        int damage = 10;

        Console.WriteLine("방어를 합니다.");

        // void 함수에서는 return 뒤에 값을 작성할 수 없음
        //return 10;
    }

    // 마나를 반환하는 함수
    // int : 정수형 반환값
    // return 키워드 사용
    // retuern 뒤에 반환값 작성
    // return문 이후의 코드는 실행되지 않음
    // return을 하지 않는다면 컴파일 오류 발생
    public static int GetMana()
    {
        return mana;
    }

    // 스태미나를 반환하는 함수
    // float : 실수형 반환값
    private float Stamina()
    {
        return 75.5f;
    }

    public void Healing()
    {
        Console.WriteLine("힐링을 합니다.");
    }

    // 별 그리기. 한 줄만.
    public void Draw(int space, int star)
    {
        for (int i = 0; i < space; i++)
        {
            Console.Write(" ");
        }
        for (int j = 0; j < star; j++)
        {
            Console.Write("*");
        }
        Console.WriteLine();
    }

    // out 매개변수 예제
    // out 매개 변수에는 함수 내에서 반드시 값이 할당되어야 함
    // 함수 호출 시 초기화하지 않아도 됨
    public static void TestOut(out int number)
    {
        // out 매개변수는 함수 내에서 반드시 값이 할당되어야 함
        // 값을 할당하지 않으면 에러 발생
        number = 42;
    }

    // ref 매개변수 예제
    // ref 매개 변수는 함수 내에서 값을 변경할 수 있음
    // 함수 호출 시 반드시 초기화되어 있어야 함
    public static int TestRef(ref int number/*, ref int number2*/)
    {
        // ref 매개변수는 함수 내에서 값을 변경할 수 있음
        // 값을 변경하지 않아도 에러는 발생하지 않음
        //number += 10; 

        return 0;
    }
}
