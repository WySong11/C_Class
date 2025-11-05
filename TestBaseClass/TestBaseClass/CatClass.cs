using System;
using static System.Console;
using TestBaseClass;


public class CatClass : AnimalClass
{
    public CatClass(int age) : base(age)
    {
        Type = AnimalType.Cat;
    }
}
