using System;

// 튜플은 여러 값을 하나의 객체로 묶을 수 있는 기능을 제공합니다.
// 메서드에서 여러 값을 반환할 때 유용하게 사용됩니다.
// 관련 데이터를 그룹화하여 코드의 가독성을 높이고,
// 불필요한 클래스를 정의하지 않고도 데이터를 전달할 수 있습니다.

// 튜플을 사용하여 최솟값과 최댓값을 반환하는 예제
public class Tuple
{
    static void Main()
    {
        var person1 = ("누구", 25);
        Console.WriteLine($"이름: {person1.Item1}, 나이: {person1.Item2}");

        var person2 = (Name: "누구", Age: 25);
        Console.WriteLine($"이름: {person2.Name}, 나이: {person2.Age}");

        var (name, age) = GetPerson();
        Console.WriteLine($"이름: {name}, 나이: {age}");


        int[] numbers = { 4, 7, 9, 2, 5 };
        var limits = FindMinMax(numbers);

        Console.WriteLine($"최솟값: {limits.min}, 최댓값: {limits.max}");
    }

    // 튜플을 반환하는 메서드
    static (string Name, int Age) GetPerson()
    {
        return ("홍길동", 30);
    }

    // 배열에서 최솟값과 최댓값을 찾는 메서드
    static (int min, int max) FindMinMax(int[] input)
    {
        if (input == null || input.Length == 0)
            throw new ArgumentException("배열이 비어 있습니다.");

        int min = int.MaxValue;
        int max = int.MinValue;

        foreach (var num in input)
        {
            if (num < min) min = num;
            if (num > max) max = num;
        }

        return (min, max);
    }

}