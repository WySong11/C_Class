using System;
using static System.Console;

public class Program
{
    static void Main(string[] args)
    {
        int[] ints = { 1, 2, 0, 4, 5 };

        WriteLine("Value at Index 1 : " + GetInt(ints, 1));
        WriteLine("Value at Index 9 : " + GetInt(ints, 9));
    }

    static int GetInt(int[] array, int index)
    {
        // 일단 null 체크를 먼저 수행
        // null 이면 기본값 반환
        // 무조건 null 체크를 먼저 수행하는 습관을 들이자
/*        if ( array == null )
        {
            WriteLine("Array is null.");
            return -1; // 기본값 반환
        }

        // 배열 길이 체크
        if (array.Length == 0)
        {
            WriteLine("Array is empty.");
            return -1; // 기본값 반환
        }

        // 인덱스 범위 체크
        if ( index < 0 || index >= array.Length )
        {
            WriteLine("Index out of range.");
            return -1; // 기본값 반환
        }*/

        // 모든 체크를 통과했으므로 안전하게 접근 가능
        // array 는 null 이 아니고, index 도 유효한 상태
        // 따라서 예외가 발생하지 않음
        //return array[index];

        // 그러나 예외 처리 구문을 추가하여 안전성을 더욱 높일 수 있음
        try
        {
            return array[index];
        }
        // 1 다음과 같이 구체적인 예외를 잡을 수도 있고,
        // catch (Exception ex) 와 같이 모든 예외를 잡을 수도 있음
        // IndexOutOfRangeException : 인덱스가 배열 범위를 벗어날 때 발생
        catch (IndexOutOfRangeException ex)
        {
            WriteLine("IndexOutOfRangeException caught: " + ex.Message);
            return -1; // 기본값 반환
        }
        // ArgumentNullException : null 인수가 전달될 때 발생
        catch (ArgumentNullException ex)
        {
            WriteLine("ArgumentNullException caught: " + ex.Message);
            return -1; // 기본값 반환
        }

        // 2 NullReferenceException : null 참조에 접근할 때 발생
        catch (NullReferenceException ex)
        {
            WriteLine("NullReferenceException caught: " + ex.Message);
            return -1; // 기본값 반환
        }
        // ArgumentOutOfRangeException : 인수가 허용된 범위를 벗어날 때 발생
        catch (ArgumentOutOfRangeException ex)
        {
            WriteLine("ArgumentOutOfRangeException caught: " + ex.Message);
            return -1; // 기본값 반환
        }
        // ArgumentException : 잘못된 인수가 전달될 때 발생
        catch ( ArgumentException ex )
        {
            WriteLine("ArgumentException caught: " + ex.Message);
            return -1; // 기본값 반환
        }

        // InvalidCastException : 잘못된 형 변환이 시도될 때 발생
        catch ( InvalidCastException ex )
        {
            WriteLine("InvalidCastException caught: " + ex.Message);
            return -1; // 기본값 반환
        }

        // OverflowException : 산술 연산이 오버플로우될 때 발생
        // 예 : int.MaxValue + 1
        catch (OverflowException ex)
        {
            WriteLine("OverflowException caught: " + ex.Message);
            return -1; // 기본값 반환
        }        

        // 3 StackOverflowException : 스택 오버플로우가 발생할 때
        // Stack : 재귀 호출이 너무 깊어질 때 발생
        // 재귀호출 : 함수가 자기 자신을 반복적으로 호출하는 것
        // 일반적으로 복구 불가능한 예외로 간주됨
        catch ( StackOverflowException ex )
        {
            WriteLine("StackOverflowException caught: " + ex.Message);
            return -1; // 기본값 반환
        }

        // DivideByZeroException : 0으로 나누기를 시도할 때 발생
        catch (DivideByZeroException ex)
        {
            WriteLine("DivideByZeroException caught: " + ex.Message);
            return -1; // 기본값 반환
        }

        // 5 OutOfMemoryException : 메모리가 부족할 때 발생
        catch (OutOfMemoryException ex)
        {
            WriteLine("OutOfMemoryException caught: " + ex.Message);
            return -1; // 기본값 반환
        }

        // 4 IOException : 입출력 작업 중 오류가 발생할 때
        // 예: 파일 읽기/쓰기 오류
        catch ( System.IO.IOException ex )
        {
            WriteLine("IOException caught: " + ex.Message);
            return -1; // 기본값 반환
        }

        catch (Exception ex)
        {
            WriteLine("Exception caught: " + ex.Message);
            return -1; // 기본값 반환
        }
    }

    // 재귀 호출 예제 메서드
    public static int square(int x, int y)
    {
        if (y == 0)
            return 1;

        return x * square(x, y - 1);
    }

    // 팩토리얼 재귀 호출 예제 메서드
    public static int factorial(int n)
    {
        if (n <= 1)
            return 1;
        return n * factorial(n - 1);
    }
}

