using System;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine(args.Length + "개의 인자가 입력되었습니다.");


        // Arguments 출력 
        foreach (string arg in args)
        {
            Console.WriteLine("인자: " + arg);
        }        
        //Console.WriteLine("\n게임을 시작합니다. '1'을 입력하여 숫자 야구 게임을 시작하거나, '2'를 입력하여 다이아몬드 출력 프로그램을 실행하세요.\n");

        if (args.Length == 0)
        {
            StartNumberBaseballDigits();
        }
        else
        {
            switch (args[0])
            {
                case "1":
                    StartNumberBaseballDigits();
                    break;

                case "2":
                    StartDiamond();
                    break;

                default:
                    Console.WriteLine("잘못된 인자입니다.");
                    break;
            }
        }
    }

    private static void StartNumberBaseballDigits()
    {
        // NumberBaseballDigits 클래스의 인스턴스를 생성하고 StartGame 메서드를 호출합니다.
        NumberBaseballDigits numberBaseballDigits = new NumberBaseballDigits();
        numberBaseballDigits.StartGame();
    }

    private static void StartDiamond()
    {
        // Static 클래스는 인스턴스를 생성하지 않고도 메서드를 호출할 수 있습니다.
        Diamond.StartDiamond();

        // Static 메서드가 아니라서 인스턴스를 생성해야 하는 경우
        //amond.ClearGame();
    }
}