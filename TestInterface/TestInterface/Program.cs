using System;
using System.Collections.Generic;
using static System.Console;



public class Program
{
    static void Main(string[] args)
    {
        //////////////////////////////////////////////////////
        // 인터페이스를 이용한 명시적 기능 구현

        TestClass ts = new TestClass();

        for (int i = 1; i < 6; i++)
        {
            IDrawSpace space = new TestClass();

            space.DrawSpace(i);

            IDrawStar star = new TestClass();

            star.DrawStar(6 - i);
        }   
        
        ///////////////////////////////////////////////////////
        // 인터페이스를 이용한 다형성 구현

        IDrawStar test1 = new TestClass1();
        IDrawStar test2 = new TestClass();

        List<IDrawStar> list = new List<IDrawStar>();

        list.Add(test1);
        list.Add(test2);

        foreach(var item in list)
        {
            item.DrawStar(5);
        }

        WriteLine();

        ///////////////////////////////////////////////////////
        // 인터페이스의 암시적 구현

        TestClass2 tc2 = new TestClass2();

        for(int i = 0; i< 5; i++)
        {
            tc2.DrawComma(); ;
        }
            
    }
}


