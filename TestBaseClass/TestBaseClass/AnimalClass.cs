using System;
using static System.Console;


public enum AnimalType
{
    Dog,
    Cat,
    Bird,
}

namespace TestBaseClass
{
    public class AnimalClass
    {
        // Private : 같은 클래스 내에서만 접근 가능
        private int age;

        // Protected : 자식 클래스에서 접근 가능
        protected bool isDomestic = true;

        // Public : 어디서나 접근 가능
        public AnimalType Type { get; set; }


        public AnimalClass(int age)
        {
            this.age = age;
        }

        public void Speak()
        {
            switch (Type)
            {
                case AnimalType.Dog:
                    WriteLine("Woof!");
                    break;
                case AnimalType.Cat:
                    WriteLine("Meow!");
                    break;
                case AnimalType.Bird:
                    WriteLine("Chirp!");
                    break;
                default:
                    WriteLine("Unknown animal sound.");
                    break;
            }
        }

        public void DrawType()
        {
            WriteLine("Animal Type: " + Type);
        }

        // Virtual : 자식 클래스에서 재정의 가능
        public virtual void DrawAga()
        {
            WriteLine("Age: " + age);
        }

        public int GetAge()
        {
            return age;
        }
    }
}
