using System;
using static System.Console;

public class LectureDataType
{
    static void Main(string[] args)
    {
        // 정수형 데이터 타입
        // C#에서 정수형 데이터 타입은 int, long, short, byte 등이 있습니다.
        // int는 4바이트(32비트) 크기의 정수형 데이터 타입으로, -2,147,483,648 ~ 2,147,483,647 범위의 값을 가질 수 있습니다.
        // long은 8바이트(64비트) 크기의 정수형 데이터 타입으로, -9,223,372,036,854,775,808 ~ 9,223,372,036,854,775,807 범위의 값을 가질 수 있습니다.
        // short는 2바이트(16비트) 크기의 정수형 데이터 타입으로, -32,768 ~ 32,767 범위의 값을 가질 수 있습니다.
        // byte는 1바이트(8비트) 크기의 정수형 데이터 타입으로, 0 ~ 255 범위의 값을 가질 수 있습니다.
        int intValue = 10; // 정수형

        WriteLine($"intValue : {intValue}");

        WriteLine($"{int.MinValue} ~ {int.MaxValue}");

        // 1. 기본 2진수 변환 (앞의 불필요한 0은 생략됨)
        string binary = Convert.ToString(intValue, 2);
        WriteLine(binary); // 출력: 1010
      
        string octal = Convert.ToString(intValue, 8);
        WriteLine(octal); // 출력: 12

        string hex = Convert.ToString(intValue, 16);
        WriteLine(hex); // 출력: a

        long longValue = 10000000000; // 정수형
        short shortValue = 100; // 정수형
        byte byteValue = 255; // 정수형

        WriteLine($"longValue : {longValue}");
        WriteLine($"shortValue : {shortValue}");
        WriteLine($"byteValue : {byteValue}");

        // 실수형 데이터 타입
        // C#에서 실수형 데이터 타입은 float, double, decimal 등이 있습니다.
        // float는 4바이트(32비트) 크기의 실수형 데이터 타입으로, 약 ±1.5 x 10^-45 ~ ±3.4 x 10^38 범위의 값을 가질 수 있습니다.
        // double은 8바이트(64비트) 크기의 실수형 데이터 타입으로, 약 ±5.0 x 10^-324 ~ ±1.7 x 10^308 범위의 값을 가질 수 있습니다.
        // decimal은 16바이트(128비트) 크기의 실수형 데이터 타입으로, 약 ±1.0 x 10^-28 ~ ±7.9 x 10^28 범위의 값을 가질 수 있습니다.
        float floatValue = 3.14f; // 실수형
        double doubleValue = 3.14; // 실수형
        decimal decimalValue = 3.14m; // 실수형

        WriteLine($"floatValue : {floatValue}");
        WriteLine($"doubleValue : {doubleValue}");
        WriteLine($"decimalValue : {decimalValue}");

        // bool형 데이터 타입
        // C#에서 bool형 데이터 타입은 true 또는 false 값을 가질 수 있습니다.
        // bool형 데이터 타입은 조건문에서 주로 사용됩니다.
        bool boolValue = true; // bool형

        WriteLine($"boolValue : {boolValue}");

        // char형 데이터 타입
        // C#에서 char형 데이터 타입은 단일 문자를 나타내는 데이터 타입으로, 2바이트(16비트) 크기를 가집니다.
        // char형 데이터 타입은 작은 따옴표('')로 감싸서 표현합니다.
        char charValue = 'A'; // char형
        
        WriteLine($"charValue : {charValue}");

        charValue = 'B';
        WriteLine($"charValue : {charValue}");

        charValue++;

        WriteLine($"charValue : {charValue}");

        // 문자열(string) 데이터 타입
        // C#에서 문자열(string) 데이터 타입은 문자들의 집합을 나타내는 데이터 타입으로, 큰 따옴표("")로 감싸서 표현합니다.
        // 문자열은 char형 배열로 구성되어 있으며, 문자열의 길이는 Length 속성을 통해 확인할 수 있습니다.
        string stringValue = "Hello, World!"; // 문자열
        WriteLine(stringValue);
        WriteLine($"stringValue의 길이 : {stringValue.Length}");
    }
}