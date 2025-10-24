using System;
using static System.Console;

namespace TestPractice
{
    public class Program
    {
        void Main(string[] args)
        {
            WriteLine("Hello, World!");

            // 튜플 사용 예시
            (int Id, string Name) person = (1, "Alice");

            var result = ParseNumber("123");
            if (result.ok)
                WriteLine($"parsed: {result.value}");
            else
                WriteLine("parse failed");

            int num1 = 5;

            UseMethodOut(out num1);
        }

        // ref : 매개변수를 참조로 전달
        // 매개변수를 전달 전에 반드시 초기화 해야 함
        // 호출자 변수를 메서드가 읽고 수정할 때 사용 — "입출력" 의도.
        // 큰 구조체(large struct)에서는 ref로 전달하면 복사 비용을 줄일 수 있음.
        public void UseMethodRef(ref int number)
        {
            number += 10;

        }

        // out : 매개변수를 참조로 전달
        // 매개변수를 전달 전에 초기화 하지 않아도 됨
        // 단, 메서드 내에서 반드시 값을 할당해야 함
        // 메서드가 값을 생성해서 호출자에 돌려줄 때 사용 — "출력(반환)" 의도.
        public void UseMethodOut(out int number)
        {
            number = 20;
        }

        // 7.2부터 도입된 기능
        // Tuple을 사용하여 여러 값을 반환할 때 유용
        public (bool ok, int value) ParseNumber(string s)
        {
            if (int.TryParse(s, out var v))
                return (true, v);
            return (false, 0);
        }
    }
}
