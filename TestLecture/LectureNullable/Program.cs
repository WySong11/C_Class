using System;
using static System.Console;

public class Program
{
    static void Main(string[] args)
    {
        //int x = null; // 오류: null을 int에 할당할 수 없음
        //float y = null; // 경고: null을 float에 할당할 수 없음
        //char z = null; // 경고: null을 char에 할당할 수 없음
        //bool b = null; // 경고: null을 bool에 할당할 수 없음

        string str = null; // 올바름: 참조 형식은 null 할당 가능

        // nullable 형식 사용
        // nullable 형식은 값 형식 뒤에 '?'를 붙여서 선언
        int? x = null; // 올바름: nullable int
        float? y = null; // 올바름: nullable float
        char? z = null; // 올바름: nullable char
        bool? b = null; // 올바름: nullable bool

        WriteLine($"x: {x}, y: {y}, z: {z}, b: {b}");

        Nullable<int> a = null; // Nullable<T> 제네릭 형식 사용

        // null 검사를 습관화   
        if (x != null)
        {
            WriteLine($"x has value: {x.Value}");
        }
        else
        {
            WriteLine("x is null");
        }

        // ? : 연산자 사용
        // null일 경우 기본값 사용
        string? name = null; // nullable 참조 형식 (C# 8.0 이상)

        // ?? : 왼쪽 피연산자가 null이면 오른쪽 피연산자 반환
        // null 병합 연산자
        // name이 null이면 "Unknown" 반환
        // null 이 아니면 name 반환
        string displayName = name ?? "Unknown";

        // ??= : 변수가 null일 때만 값 할당
        name ??= "Default Name"; // name이 null일 때 "Default Name" 할당

        //////////////////////////////////////////////////
        // 삼항 연산자
        int number = 10;
        int value = number;

        if (number > 0)
        {
            value = number;
        }
        else
        {
            value = -number;
        }

        // 위의 if-else 문을 삼항 연산자로 변환
        //  조건 ? 참일 때 값 : 거짓일 때 값
        //  number가 0보다 크면 number, 아니면 -number
        value = number > 0 ? number : -number;

        // 중첩 삼항 연산자
        value = number > 5 ? number < 10 ? 100 : 200 : -100;

        value = (number > 5) ? (number < 10 ? 100 : 200) : -100;

        WriteLine($"displayName: {displayName}, name: {name}, value: {value}");

        ////////////////////////////////////////////////////
        ///

        Action OnTick = null;

        // ?. : null 조건부 연산자
        // OnTick가 null이 아니면 Invoke 호출
        OnTick?.Invoke();

        OnTick += () => WriteLine("Tick event occurred.");

        OnTick.Invoke();


        OnTick?.Invoke();

        /*        int[] aa = { 1, 2, 3 };

                try
                {
                    //aa[5] = 10;

                    OnTick.Invoke();
                }
                catch (NullReferenceException ex)
                {
                    WriteLine("NullReferenceException caught: " + ex.Message);
                }
                catch (Exception ex)
                {
                    WriteLine("Exception caught: " + ex.Message);
                }*/
    }
}
