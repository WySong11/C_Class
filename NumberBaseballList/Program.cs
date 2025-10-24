using System;
using System.Collections.Generic;
using static System.Console;

namespace NumberBaseballList
{
    public class Program
    {
        // 프로그램 전체에서 하나의 Random 인스턴스를 재사용합니다.
        // Random을 반복해서 새로 만들면 매우 짧은 시간 안에 같은 시드로 인해
        // 같은 숫자가 반복될 수 있으므로 한 번만 생성해 재사용하는 것이 안전합니다.
        private static readonly Random _random = new Random();

        private static void Main(string[] args)
        {
            // Quest 리스트는 정답으로 사용할 서로 다른 세 자리 숫자(각 자리마다 한 숫자)를 저장합니다.
            // 예: 정답이 "1 5 7" 이면 Quest는 {1, 5, 7} 형태로 저장됩니다.
            List<int> Quest = new List<int>();

            // 정답 리스트에 서로 다른 숫자 3개를 채울 때까지 반복합니다.
            // Count가 3이 되면 세 자리 정답이 완성됩니다.
            while (Quest.Count < 3)
            {
                // 중복을 피하기 위해 현재 Quest 값을 전달해서 새로운 숫자를 받아옵니다.
                Quest.Add(GetQuestNumber(Quest));
            }

            // 개발/디버그용으로 생성된 정답을 출력합니다.
            // 실전 게임에서는 정답을 숨기는 것이 일반적입니다.
            WriteLine($"정답 : {Quest[0]} , {Quest[1]} , {Quest[2]}");

            // 스트라이크(같은 자리, 같은 숫자)와 볼(다른 자리지만 정답에 존재하는 숫자)의 개수를 담을 변수
            int strike = 0;
            int ball = 0;

            // 사용자가 3 스트라이크(모든 자리 숫자 일치)를 얻을 때까지 입력을 반복합니다.
            do
            {
                WriteLine("\n세 자리 숫자를 입력하세요: ");
                // 사용자가 입력한 문자열을 가져옵니다. null이 될 수 있으므로 nullable로 선언됨.
                string? answer = Console.ReadLine();

                // 입력값이 null이거나 문자열 길이가 3이 아닌 경우 예외 처리
                // 예: 빈 입력, 2자리 또는 4자리 입력을 방지
                if (string.IsNullOrEmpty(answer) || answer.Length != 3)
                {
                    WriteLine("\n세 자리 숫자를 입력해야 합니다. 예: 123 또는 012");
                    continue; // 잘못된 입력이면 반복문의 처음으로 돌아가 새 입력을 받습니다.
                }

                // 문자열을 정수로 시도 변환합니다.
                // int.TryParse는 변환 성공 시 true를 반환하고 out으로 변환된 값을 전달합니다.
                // 여기서 0 ~ 999 범위를 체크하는 이유는 "000" ~ "999" 까지의 3자리 입력을 허용하기 위함입니다.
                // (예: "012"는 정수 12로 변환되지만 자리 분해 로직에서 올바르게 0,1,2가 분리됩니다.)
                if (!int.TryParse(answer, out int userInput) || userInput < 0 || userInput > 999)
                {
                    WriteLine("\n유효한 세 자리 숫자를 입력하세요. (숫자 0~9가 각 자리여야 함)");
                    continue;
                }

                // 입력값의 각 자리 숫자와 정답의 각 자리 숫자를 비교해서 스트라이크와 볼을 계산합니다.
                CompareDigits(Quest, userInput, out strike, out ball);

                // 결과 출력: 사용자가 입력한 문자열 그대로와 함께 스트라이크/볼 개수를 보여줍니다.
                // 문자열을 그대로 출력하면 "012" 같은 입력이 보존되어 사용자에게 혼동이 없습니다.
                WriteLine($"\n{answer} : {strike} Strike, {ball} Ball");

            } while (strike < 3); // 스트라이크가 3이 되면 게임 종료

            WriteLine("\nStrike Out~!!! Game Over.");
        }

        // GetQuestNumber는 현재까지 선택된 숫자 목록(questList)을 받아서
        // 중복되지 않는 새로운 한 자리 숫자(0~9)를 반환합니다.
        // 파라미터:
        //  - questList: 이미 선택된 숫자들의 리스트
        // 반환값:
        //  - 새로 선택된 중복 없는 한 자리 정수(0~9)
        public static int GetQuestNumber(List<int> questList)
        {
            // _random.Next(0, 10)은 0부터 9까지의 랜덤 정수를 반환합니다.
            int num = _random.Next(0, 10);

            // 만약 questList가 비어있다면(첫 번째 숫자) 바로 반환합니다.
            if (questList.Count == 0)
            {
                return num;
            }

            // 생성한 숫자가 이미 리스트에 포함되어 있지 않으면 그것을 반환합니다.
            if (questList.Contains(num) == false)
            {
                return num;
            }

            // 이미 포함되어 있으면 다시 시도합니다.
            // 재귀 호출 방식으로 구현되어 있지만, 중복이 자주 발생하지 않으므로 간단히 사용합니다.
            // (원한다면 while 루프 등으로 변경할 수 있습니다.)
            return GetQuestNumber(questList);
        }

        // 숫자를 비교하여 스트라이크와 볼을 계산하는 메서드
        /*
         * 파라미터 설명:
         * answerDigits: 정답 숫자를 담고 있는 리스트 (각 요소는 한 자리 숫자, 예: {1,5,7})
         * userInput:    사용자가 입력한 정수(예: "123" -> 123)
         * strike:       같은 자리에서 숫자가 일치한 개수 (출력 파라미터, 예: 2)
         * ball:         다른 자리에서 숫자가 등장한 개수 (출력 파라미터, 예: 1)
         *
         * 동작 요약:
         * 1) userInput을 100의 자리, 10의 자리, 1의 자리로 분해하여 리스트로 만듭니다.
         * 2) 각 자리마다 정답 리스트에서 그 숫자가 어디에 있는지 찾습니다(IndexOf).
         *    - 값이 없으면IndexOf는 -1을 반환 -> None
         *    - 값이 있고 인덱스가 동일하면 Strike
         *    - 값이 있고 인덱스가 다르면 Ball
         */
        public static void CompareDigits(List<int> answerDigits, int userInput, out int strike, out int ball)
        {
            // out 파라미터는 호출 전에 초기화할 필요는 없지만, 메서드 안에서 반드시 초기화해야 합니다.
            strike = 0;
            ball = 0;

            // userInput에서 각 자리 숫자를 분리합니다.
            // 예: 123 -> {1, 2, 3}
            //      012 -> userInput은 12지만 분해하면 {0, 1, 2}가 되어 입력의 각 자리가 보존됩니다.
            List<int> userDigits = new List<int>()
            {
                // 100의 자리 숫자: userInput / 100
                // 예: 123 / 100 = 1
                userInput / 100 % 10,
                // 10의 자리 숫자: (userInput / 10) % 10
                // 예: 123 / 10 = 12 -> 12 % 10 = 2
                userInput / 10 % 10,
                // 1의 자리 숫자: userInput % 10
                // 예: 123 % 10 = 3
                userInput % 10
            };

            // 각 자리(0,1,2)를 반복하면서 정답 리스트에서 같은 숫자가 있는지 확인합니다.
            for (int i = 0; i < userDigits.Count && i < answerDigits.Count; i++)
            {
                // IndexOf는 리스트에서 값의 첫 번째 인덱스를 반환합니다.
                // 값이 존재하지 않으면 -1을 반환합니다.
                int result = answerDigits.IndexOf(userDigits[i]);

                if (result == -1)
                {
                    // 정답 리스트에 해당 숫자가 없으면 아무 것도 증가시키지 않고 다음 자리로 진행합니다.
                    continue;
                }

                // 정답 리스트에서 찾은 인덱스(result)와 현재 자리 인덱스(i)가 같으면 Strike입니다.
                if (result == i)
                {
                    strike++;
                    // Strike이면 같은 숫자에 대해 Ball 처리를 하지 않기 위해 continue 합니다.
                    continue;
                }

                // 정답 리스트에는 숫자가 있지만 위치가 다르다면 Ball입니다.
                ball++;
            }

            // 추가로 초보자가 이해하기 쉽도록 각 자리별 판정을 콘솔에 자세히 출력합니다.
            // 이 블록은 학습용이며 실제 게임에서는 생략할 수 있습니다.
            WriteLine(); // 빈 줄로 가독성 향상

            // foreach 대신 for를 쓰지 않은 이유: IndexOf를 사용했을 때 같은 값이 여러 번 나올 경우
            // IndexOf는 첫 번째 인덱스를 반환하므로 같은 값이 중복되어 처리되는 상황을
            // 명확히 보여주기 위해 현재 방식으로 출력합니다.
            foreach (var item in userDigits)
            {
                int result = answerDigits.IndexOf(item);

                if (result == -1)
                {
                    WriteLine($"{item} : None"); // 정답에 없는 숫자
                }
                else if (result == userDigits.IndexOf(item))
                {
                    // userDigits.IndexOf(item)은 해당 item의 첫 등장 인덱스를 반환
                    // (동일한 숫자가 중복 입력된 경우에는 첫 등장만 Strike/ Ball 처리 방식에 영향을 줌)
                    WriteLine($"{item} : Strike");
                }
                else
                {
                    WriteLine($"{item} : Ball");
                }
            }
        }
    }
}
