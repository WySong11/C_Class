using System;
using static System.Console;



public class TestClass : IDrawStar , IDrawSpace
{
    // 명시적 인터페이스 구현
    // 인터페이스 이름을 명시해야 한다.
    // 접근 제한자는 붙이지 않는다.

    void IDrawStar.DrawStar()
    {
        Write("*");
    }

    void IDrawStar.DrawStar(int count)
    {
        WriteLine( new string('*', count) );
    }

    void IDrawSpace.DrawSpace()
    {
        Write(" ");
    }

    void IDrawSpace.DrawSpace(int count)
    {
        WriteLine(new string(' ', count));
    }
}

public class TestClass1 : IDrawStar
{
    // 명시적 인터페이스 구현
    // 인터페이스 이름을 명시해야 한다.
    // 접근 제한자는 붙이지 않는다.
    void IDrawStar.DrawStar()
    {
        Write("*");
    }
    void IDrawStar.DrawStar(int count)
    {
        WriteLine(new string('*', count));
    }
}

public  class TestClass3 : IDrawStar, IDrawSpace
{
    // 혼합된 인터페이스 구현
    // 명시적 인터페이스 구현과 암시적 인터페이스 구현을 혼합할 수 있다.
    // 암시적 인터페이스 구현
    public void DrawStar()
    {
        Write("*");
    }
    // 명시적 인터페이스 구현
    void IDrawStar.DrawStar(int count)
    {
        WriteLine(new string('*', count));
    }
    // 암시적 인터페이스 구현
    public void DrawSpace()
    {
        Write(" ");
    }
    // 명시적 인터페이스 구현
    void IDrawSpace.DrawSpace(int count)
    {
        WriteLine(new string(' ', count));
    }
}

public class TestClass2 : IDrawComma
{
    // 암시적 인터페이스 구현
    // 접근 제한자는 public 이어야 한다.
    // 인터페이스 이름을 명시하지 않는다.

    public void DrawComma()
    {
        Write(",");
    }
}