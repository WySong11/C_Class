using System;
using static System.Console;
using TestBaseClass;


public class DogClass : AnimalClass
{    

    public DogClass(int age) : base(age)
    {
        Type = AnimalType.Dog;
    }

/*    public void Speak()
    {
        base.Speak();

        WriteLine("Bow Wow!");        
    }*/


    // Override : 부모 클래스의 메서드를 재정의
    public override void DrawAga()
    {
        //WriteLine("Dog's Age: " + age);

        //WriteLine("Dog's Age: " + GetAge());

        // 부모 클래스의 메서드 호출
        base.DrawAga();

        WriteLine("Dog is domestic: " + isDomestic);

        base.DrawAga();
    }
}
