using System;

public class Program
{
    static void Main(string[] args)
    {
        StartNumberBaseballDigits();
    }

    private static void StartNumberBaseballDigits()
    {
        // NumberBaseballDigits 클래스의 인스턴스를 생성하고 StartGame 메서드를 호출합니다.
        NumberBaseballDigits numberBaseballDigits = new NumberBaseballDigits();
        numberBaseballDigits.StartGame();
    }
}