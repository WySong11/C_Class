using System;
using static System.Console;

public class Program
{
    static void Main(string[] args)
    {
        /////////////////////////////////////////////////////
        /// 산술 연산자 (Arithmetic Operators)

        int a = 5;
        int b = 10;

        // a + b 를 더해서 sum 변수에 저장
        int sum = a + b;

        WriteLine(a + " + " + b + " = " + sum);

        // 문자열 보간법 (String Interpolation) 사용
        // $ : 문자열 앞에 $ 기호를 붙여서 변수나 표현식을 중괄호 {} 안에 넣어 문자열에 삽입
        WriteLine($"{a} + {b} = {sum}");

        sum = a - b;
        WriteLine($"{a} - {b} = {sum}");

        sum = a * b;
        WriteLine($"{a} * {b} = {sum}");

        // 실수형으로 변환하여 나눗셈 수행
        float divResult = (float)a / b;        
        WriteLine($"{a} / {b} = {divResult}");

        float e = 11.23f;
        float f = 2.4f;
        float g = e / f;

        WriteLine($"{e} / {f} = {g}");

        // 소수점 이하를 반올림하여 출력
        // MathF.Round() : 실수형(float) 반올림 함수
        WriteLine($"{e} / {f} = {MathF.Round(g)}");

        // MathF.Round() 함수의 두 번째 매개변수로 반올림할 소수점 자릿수 지정 가능
        WriteLine($"{e} / {f} = {MathF.Round(g, 2)}");

        // MathF.Floor() : 실수형 내림 함수
        // 소수점 이하를 내림하여 출력
        WriteLine($"{e} / {f} = {MathF.Floor(g)}");

        // MahtF.Ceiling() : 실수형 올림 함수
        // 소수점 이하를 올림하여 출력
        WriteLine($"{e} / {f} = {MathF.Ceiling(g)}");

        a = 15;
        b = 4;
        sum = a % b; // 나머지 연산자
        WriteLine($"{a} % {b} = {sum}");

        // 원하는 자리수의 나머지 구하기
        a = 103;
        b = 10;
        sum = a % b;
        WriteLine($"{a} % {b} = {sum}");

        a = 1234;
        b = 10; 
        sum = a % b;
        WriteLine($"{a} % {b} = {sum}");

        // 짝수, 홀수 판별
        a = 11;
        b = 2;
        sum = a % b;
        WriteLine($"{a} % {b} = {sum}");

        // 복합 대입 연산자
        a += 1;
        a -= 1;
        a += 2;
        a *= 3;
        a /= 2;
        a %= 4;

        a = 1;
        a = a + 1;

        a++; // a = a + 1 과 동일
        a--; // a = a - 1 과 동일

        WriteLine(a);

        // 증가 연산자
        {
            a++;
            WriteLine(a);
        }
        // WriteLine(++a);

        {
            WriteLine(a);
            a++;
        }        
        // WriteLine(a++);

        WriteLine(a++); // 후위 연산자: 현재 값을 출력한 후 1 증가
        WriteLine(a);
        WriteLine(++a); // 전위 연산자: 먼저 1 증가시킨 후 값을 출력
        WriteLine(a);

        WriteLine(a--); // 후위 연산자
        WriteLine(--a); // 전위 연산자

        /////////////////////////////////////////////////////
        /// 비교 연산자 (Comparison Operators)
        // 결과는 항상 논리형 (Boolean) 값: true 또는 false
        // == : 같다
        // != : 같지 않다
        // >  : 크다
        // <  : 작다
        // >= : 크거나 같다
        // <= : 작거나 같다

        bool result;

        result = (a == b);
        WriteLine($"{a} == {b} : {result}");

        result = (a != b);
        WriteLine($"{a} != {b} : {result}");

        result = (a > b);
        WriteLine($"{a} > {b} : {result}");

        result = (a < b);
        WriteLine($"{a} < {b} : {result}");

        result = (a >= b);
        WriteLine($"{a} >= {b} : {result}");

        result = (a <= b);
        WriteLine($"{a} <= {b} : {result}");

        /////////////////////////////////////////////////////
        /// 논리 연산자 (Logical Operators)
        // 결과는 항상 논리형 (Boolean) 값: true 또는 false
        // && : 그리고 (AND)
        // || : 또는 (OR)
        // !  : 아니다 (NOT)

        bool A = true;
        bool B = false;

        // && 연산자
        // A 와 B 가 모두 true 일 때만 결과가 true
        // false && false = false
        // false && true  = false
        // true  && false = false
        // true  && true  = true
        result = A && B;
        WriteLine($"{A} && {B} : {result}");

        // || 연산자
        // A 와 B 중 하나라도 true 면 결과가 true
        // false || false = false
        // false || true  = true
        // true  || false = true
        // true  || true  = true
        result = A || B;
        WriteLine($"{A} || {B} : {result}");

        // ! 연산자 ( 청개구리 연산자 )
        // A 가 true 면 false, A 가 false 면 true
        result = !A;
        WriteLine($"!{A} : {result}");
        result = !B;
        WriteLine($"!{B} : {result}");

        bool C = true;
        // 연산자 우선순위
        // ( true || false ) && true  = true
        result = A || B && C;
        WriteLine($"{A} || {B} && {C} : {result}");

        // ( true && false ) && true  = false
        result = (A && B) && C;
        WriteLine($"({A} && {B}) && {C} : {result}");

        // 괄호를 사용하여 우선순위 변경
        // true && ( false || true )  = true
        result = A && ( B || C );
        WriteLine($"{A} && ({B} || {C}) : {result}");

        /////////////////////////////////////////////////////
        /// 비트 연산자 (Bitwise Operators)
        // &  : 비트 AND
        // |  : 비트 OR
        // ^  : 비트 XOR
        // ~  : 비트 NOT
        // << : 왼쪽 시프트
        // >> : 오른쪽 시프트
        // 비트 연산자는 정수형 데이터에 대해 비트 단위로 연산 수행
        
        int intA = 6;  // 0000 0110 = 4 + 2 = 6
        int intB = 5;  // 0000 0101 = 4 + 1 = 5
                       // -----------------
                       // 0000 0100 = 4

        int intResult = intA & intB; // 비트 AND
        WriteLine($"{intA} & {intB} = {intResult}");

        intA = 6;  // 0000 0110 = 4 + 2 = 6
        intB = 5;  // 0000 0101 = 4 + 1 = 5
                   // -----------------
                   // 0000 0111 = 4 + 2 + 1 = 7

        intResult = intA | intB; // 비트 OR
        WriteLine($"{intA} | {intB} = {intResult}");

        intA = 6;  // 0000 0110 = 4 + 2 = 6
        intB = 5;  // 0000 0101 = 4 + 1 = 5
                   // -----------------
                   // 0000 0011 = 2 + 1 = 3

        intResult = intA ^ intB; // 비트 XOR
        WriteLine($"{intA} ^ {intB} = {intResult}");

        intA = 6;  // 0000 0110 = 4 + 2 = 6
                   // -----------------
                   // 1111 1001 = -7 (2의 보수)

        intResult = ~intA; // 비트 NOT
        WriteLine($"~{intA} = {intResult}");

        intA = 6;  // 0000 0110 = 4 + 2 = 6
                   // -----------------
                   // 0000 1100 = 12

        intResult = intA << 1; // 왼쪽 시프트
        WriteLine($"{intA} << 1 = {intResult}");

        intB = 20; // 0001 0100 = 16 + 4 = 20
                   // -----------------
                   // 0000 1010 = 8 + 2 = 10

        intResult = intB >> 1; // 오른쪽 시프트
        WriteLine($"{intB} >> 1 = {intResult}");

        // 0000 0000
        // 0000 0001 : 공격 가능
        // 0000 0010 : 방어 가능
        // 0000 0100 : 아이템 사용 가능
        // 0000 1000 : 마법 사용 가능

        // 캐릭터 상태 설정
        // 0000 0101 : 공격 가능, 아이템 사용 가능
        // 0000 1100 : 아이템 사용 가능, 마법 사용 가능
    }
}
