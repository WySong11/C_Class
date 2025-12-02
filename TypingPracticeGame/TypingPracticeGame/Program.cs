using System;
using System.Diagnostics;

namespace TypingPracticeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            // 연습에 사용할 단어 목록
            string[] words =
            {
                "apple", "banana", "computer", "keyboard", "monitor",
                "school", "programming", "console", "practice", "random",
                "message", "window", "orange", "universe", "galaxy",
                "camera", "pencil", "notebook", "library", "internet"
            };

            const int gameDurationSeconds = 30; // 게임 진행 시간(초)
            Random random = new Random();

            int totalWords = 0;        // 전체 시도 단어 수
            int correctWords = 0;      // 정확히 친 단어 수
            int totalChars = 0;        // 전체 타이핑한 목표 글자 수
            int correctChars = 0;      // 정확히 맞춘 글자 수

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== 타자 연습 게임 ===");
            Console.WriteLine($"제한 시간: {gameDurationSeconds}초");
            Console.WriteLine("보이는 단어를 똑같이 입력하고 Enter를 누르세요.");
            Console.WriteLine("아무 키나 누르면 시작합니다...");
            Console.ReadKey(true);
            Console.Clear();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (true)
            {
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;

                if (elapsedSeconds >= gameDurationSeconds)
                {
                    break;
                }

                int remainingSeconds = Math.Max(0, gameDurationSeconds - (int)elapsedSeconds);

                // 콘솔 창 제목에도 남은 시간을 표시
                Console.Title = $"타자 연습 게임 - 남은 시간: {remainingSeconds}초";

                string target = words[random.Next(words.Length)];

                Console.WriteLine("=== 새 단어 ===");
                Console.WriteLine($"남은 시간: {remainingSeconds}초");
                Console.WriteLine($"단어: {target}");
                Console.Write("입력: ");

                string input = Console.ReadLine() ?? string.Empty;

                // 입력 도중에 시간이 끝났을 수 있으니 한 번 더 체크
                elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                if (elapsedSeconds > gameDurationSeconds)
                {
                    break;
                }

                totalWords++;
                totalChars += target.Length;

                // 글자 단위 정확도 계산
                int wordCorrectChars = CountCorrectChars(target, input);
                correctChars += wordCorrectChars;

                if (input == target)
                {
                    correctWords++;
                    Console.WriteLine("정답! ✅");
                }
                else
                {
                    Console.WriteLine($"오답! ❌ (맞춘 글자 수: {wordCorrectChars}/{target.Length})");
                }

                Console.WriteLine();
            }

            stopwatch.Stop();
            Console.Clear();

            double totalTime = stopwatch.Elapsed.TotalSeconds;
            if (totalTime == 0) totalTime = 0.1;

            double accuracy = totalChars > 0
                ? (double)correctChars / totalChars * 100.0
                : 0.0;

            double wpm = correctWords / (totalTime / 60.0); // Word Per Minute

            Console.Title = "타자 연습 게임 - 결과";

            Console.WriteLine("=== 결과 ===");
            Console.WriteLine($"총 플레이 시간: {totalTime:F1}초");
            Console.WriteLine($"전체 시도 단어 수: {totalWords}");
            Console.WriteLine($"정확히 친 단어 수: {correctWords}");
            Console.WriteLine($"글자 정확도: {accuracy:F1}%");
            Console.WriteLine($"분당 정확 단어 수(WPM): {wpm:F1}");
            Console.WriteLine();
            Console.WriteLine("아무 키나 누르면 종료합니다...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// target 과 input 을 비교해서 같은 위치에 같은 글자인 개수를 센다.
        /// (길이가 다르면 더 짧은 쪽 기준으로 비교)
        /// </summary>
        static int CountCorrectChars(string target, string input)
        {
            int length = Math.Min(target.Length, input.Length);
            int count = 0;

            for (int i = 0; i < length; i++)
            {
                if (target[i] == input[i])
                {
                    count++;
                }
            }

            return count;
        }
    }
}
