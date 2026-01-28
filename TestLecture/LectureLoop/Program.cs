using System;
using System.Collections.Specialized;

public class TestLoop
{   
    // 클래스 전역 변수
    int classLevelVariable = 10;

    static void Main(string[] args)
    {
        // 메서드 내 지역 변수
        int test;
        //int i;

        // For 반복문
        // 1. i = 0 : 초기화
        // 2. i < 5 : 조건
        // 3. Console.WriteLine : 실행할 코드
        // 4. i++ : 증감
        // 5. 조건이 false가 될 때까지 2~4를 반복
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Iteration {i + 1}");
        }

        // for 문이 끝난 후에는 i 변수를 사용할 수 없습니다.
        //Console.WriteLine("Loop finished." + i);

        {
            int j = 0;
        }

        // j 변수는 위의 블록 밖에서는 접근할 수 없습니다.
        //Console.WriteLine(j);


        // 중첩 for 문
        // i는 0부터 4까지 반복
        // j는 0부터 2까지 반복
        // 각 i에 대해 j가 0, 1, 2로 반복됩니다.
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                Console.WriteLine($"i: {i}, j: {j}");
            }
        }

        for (int i = 5; i > 0; i--)
        {
            Console.WriteLine($"Countdown: {i}");
        }

        for (int i = 5; i > 0; i-=2 )
        {
            Console.WriteLine($"Countdown: {i}");
        }

        int count = 0;

        for ( ; count < 5; )
        {
            Console.WriteLine($"Count is: {count}");
            count++;
        }

        for( ; ; )
        {
            // while, do-while 문에서도 적용되는 continue 문
            // continue : 현재 반복을 건너뛰고 다음 반복으로 넘어감
            if (count == 5)
                continue;

            // while, do-while 문에서도 적용되는 break 문
            // break : 반복문을 완전히 종료
            if (count >= 10)
                break;

            Console.WriteLine($"Infinite Loop Count: {count}");
            count++;
        }

        //////////////////////////////////////////
        ///

        int[] numbers = { 1, 2, 3, 4, 5 };

        // foreach 반복문
        // 배열이나 컬렉션의 각 요소를 순회할 때 사용
        // int number : 배열의 각 요소를 담는 임시 변수
        // in numbers : 순회할 배열
        foreach ( int number in numbers)
        {
            Console.WriteLine($"Number: {number}");
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            int value = numbers[i];
            Console.WriteLine($"Number from for loop: {numbers[i]}");
        }

        // var 키워드를 사용한 foreach 문
        // var : 컴파일러가 변수의 타입을 자동으로 추론
        foreach (var number in numbers)
        {
            Console.WriteLine($"Number: {number}");
        }

        string[] fruits = { "Apple", "Banana", "Cherry" };

        foreach(string fruit in fruits)
        {
            Console.WriteLine($"Fruit: {fruit}");
        }

        foreach (var fruit in fruits)
        {
            Console.WriteLine($"Fruit: {fruit}");
        }        
    }
}
