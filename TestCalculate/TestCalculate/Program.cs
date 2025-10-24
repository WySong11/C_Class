using System;

namespace TestCalculate
{
    /// <summary>
    /// 콘솔 기반 간단한 계산기
    /// 입력: 1번째 숫자, 연산자(+, -, *, /, %, ^ 또는 pow), 2번째 숫자
    /// 빈 입력 시 종료
    /// </summary>
    internal static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("간단한 계산기 (빈 입력 시 종료)");

            // 무한 루프: 사용자가 빈 줄을 입력하면 루프를 빠져나가 종료합니다.
            while (true)
            {
                // 1) 첫 번째 숫자 입력 받기
                Console.Write("첫 번째 숫자: ");
                string aStr = Console.ReadLine();

                // 사용자가 아무 것도 입력하지 않으면 종료 (빈 입력 처리)
                if (string.IsNullOrWhiteSpace(aStr)) break;

                // 2) 연산자 입력 받기
                Console.Write("연산자 (+ - * / % ^ 또는 pow): ");
                string opStr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(opStr)) break;

                // 3) 두 번째 숫자 입력 받기
                Console.Write("두 번째 숫자: ");
                string bStr = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(bStr)) break;

                // TryCalculate를 호출하여 계산 시도
                // 성공하면 result에 결과가 담기고 true 반환
                // 실패하면 error에 오류 메시지 담기고 false 반환
                if (TryCalculate(aStr, opStr, bStr, out double result, out string error))
                {
                    Console.WriteLine($"결과: {result}");
                }
                else
                {
                    Console.WriteLine($"오류: {error}");
                }

                Console.WriteLine(); // 보기 좋게 빈 줄 추가
            }

            Console.WriteLine("프로그램을 종료합니다.");
        }

        // 재사용 가능한 계산 로직 (입력 문자열 -> 결과)
        // 반환값: 계산 성공하면 true, 실패하면 false
        // out 매개변수: result(계산 결과), error(오류 메시지)
        public static bool TryCalculate(string aStr, string opStr, string bStr, out double result, out string error)
        {
            // 초기값 설정
            result = 0;
            error = null;

            // 입력이 비어 있거나 공백만 있는 경우, 오류로 처리
            // 사용자가 숫자/연산자/숫자 세 항목 모두 입력해야 함을 알려줌
            if (string.IsNullOrWhiteSpace(aStr) || string.IsNullOrWhiteSpace(opStr) || string.IsNullOrWhiteSpace(bStr))
            {
                error = "모든 입력(숫자, 연산자, 숫자)을 입력하세요.";
                return false;
            }

            // 문자열에서 양쪽 공백 제거 후 double로 변환 시도
            // TryParse는 변환에 실패하면 false를 반환하므로 예외 대신 안전하게 처리 가능
            if (!double.TryParse(aStr.Trim(), out double a))
            {
                // 변환 실패 시 어떤 입력이 문제인지 사용자에게 알림
                error = $"첫 번째 숫자({aStr})를 숫자로 변환할 수 없습니다.";
                return false;
            }

            // double 실수형으로 변환

            if (!double.TryParse(bStr.Trim(), out double b))
            {
                error = $"두 번째 숫자({bStr})를 숫자로 변환할 수 없습니다.";
                return false;
            }

            // 연산자 문자열에서 공백 제거
            string op = opStr.Trim();

            // 편의를 위해 연산자 문자의 첫 글자만 사용
            // 예: "pow" -> 'p' 이므로 default로 빠지지만 문자열 전체를 체크하는 로직도 하단에 있음
            char token = op.Length > 0 ? op[0] : '\0';

            if (token == '+')
            {
                return a + b;
            }

            // 연산자에 따라 분기 처리
            switch (token)
            {
                case '+':
                    // 덧셈
                    result = a + b;
                    return true;

                case '-':
                    // 뺄셈
                    result = a - b;
                    return true;

                case '*':
                case 'x': // 사용자 편의로 소문자 x 허용
                case 'X': // 대문자 X 허용
                    // 곱셈
                    result = a * b;
                    return true;

                case '/':
                    // 나눗셈: 0으로 나누는지 체크
                    // double.Epsilon은 아주 작은 값이므로, 완전한 0 체크 목적이라면 (b == 0)로 해도 무방합니다.
                    if (Math.Abs(b) < double.Epsilon)
                    {
                        error = "0으로 나눌 수 없습니다.";
                        return false;
                    }
                    result = a / b;
                    return true;

                case '%':
                    // 나머지 연산: 분모가 0이면 안 됨
                    if (Math.Abs(b) < double.Epsilon)
                    {
                        error = "0으로 나눌 수 없습니다 (나머지 연산).";
                        return false;
                    }
                    result = a % b;
                    return true;

                case '^':
                    // 거듭제곱: a ^ b 를 Math.Pow(a, b)로 계산
                    result = Math.Pow(a, b);
                    return true;

                default:
                    // 위의 단일 문자 연산자 외에 문자열 키워드도 허용 (예: "pow" 또는 "power")
                    string low = op.ToLowerInvariant();
                    if (low == "pow" || low == "power")
                    {
                        result = Math.Pow(a, b);
                        return true;
                    }

                    // 지원하지 않는 연산자인 경우 사용자에게 사용 가능한 연산자 목록을 알려줌
                    error = $"지원하지 않는 연산자: '{op}'. 사용 가능한 연산자: + - * / % ^ (또는 pow)";
                    return false;
            }
        }
    }
}