using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace TypingPracticeGame_CsvJson
{
    class Program
    {
        // 게임 시간 (초)
        const int GameDurationSeconds = 30;

        // CSV, JSON 파일 이름
        const string DefaultCsvFileName = "words.csv";
        const string DefaultJsonFileName = "typing_results.json";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("=== 타자 연습 게임 (CSV 입력 + JSON 기록) ===");
            Console.WriteLine($"게임 시간: {GameDurationSeconds}초");
            Console.WriteLine();

            // CSV 경로 입력 받기 (엔터면 기본값 사용)
            Console.Write($"단어 목록 CSV 파일 경로를 입력하세요 (기본값: {DefaultCsvFileName}): ");
            string csvPath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(csvPath))
            {
                csvPath = DefaultCsvFileName;
            }

            if (!File.Exists(csvPath))
            {
                Console.WriteLine($"[오류] CSV 파일을 찾을 수 없습니다: {csvPath}");
                Console.WriteLine("프로그램을 종료합니다.");
                return;
            }

            List<string> words;

            try
            {
                words = LoadWordsFromCsv(csvPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[오류] CSV 파일을 읽는 중 문제가 발생했습니다.");
                Console.WriteLine(ex.Message);
                Console.WriteLine("프로그램을 종료합니다.");
                return;
            }

            if (words.Count == 0)
            {
                Console.WriteLine("[오류] CSV에서 읽은 단어가 없습니다.");
                Console.WriteLine("프로그램을 종료합니다.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"로드된 단어 개수: {words.Count}개");
            Console.WriteLine("엔터를 누르면 게임을 시작합니다.");
            Console.ReadLine();

            // 게임 실행
            GameResult result = RunTypingGame(words);

            // 결과 출력
            Console.WriteLine();
            Console.WriteLine("=== 결과 ===");
            Console.WriteLine($"플레이 시간(초): {result.DurationSeconds:F1}");
            Console.WriteLine($"전체 시도 단어 수: {result.TotalWords}");
            Console.WriteLine($"정확히 친 단어 수: {result.CorrectWords}");
            Console.WriteLine($"목표 글자 수: {result.TotalChars}");
            Console.WriteLine($"맞춘 글자 수: {result.CorrectChars}");
            Console.WriteLine($"정확도: {result.Accuracy:F2}%");
            Console.WriteLine($"WPM(분당 타자 수): {result.Wpm:F2}");
            Console.WriteLine();

            // JSON 기록 저장
            try
            {
                SaveResultToJson(result, DefaultJsonFileName);
                Console.WriteLine($"게임 기록을 JSON 파일에 저장했습니다: {DefaultJsonFileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("[경고] JSON 파일 저장 중 오류가 발생했습니다.");
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("엔터를 누르면 종료합니다.");
            Console.ReadLine();
        }

        /// <summary>
        /// CSV 파일에서 단어 목록을 읽어 옵니다.
        /// 각 줄의 첫 번째 컬럼을 단어로 사용합니다.
        /// </summary>
        static List<string> LoadWordsFromCsv(string filePath)
        {
            var words = new List<string>();

            foreach (var line in File.ReadLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // 쉼표 기준으로 나누고 첫 번째 칸만 사용
                string[] cells = line.Split(',');
                string word = cells[0].Trim();

                // 양 끝에 따옴표가 있으면 제거
                if (word.StartsWith("\"") && word.EndsWith("\"") && word.Length >= 2)
                {
                    word = word.Substring(1, word.Length - 2);
                }

                if (!string.IsNullOrWhiteSpace(word))
                {
                    words.Add(word);
                }
            }

            return words;
        }

        /// <summary>
        /// 실제 타자 연습 게임을 실행하고 결과를 반환합니다.
        /// </summary>
        static GameResult RunTypingGame(List<string> words)
        {
            var random = new Random();

            int totalWords = 0;
            int correctWords = 0;
            int totalChars = 0;
            int correctChars = 0;

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(GameDurationSeconds);

            Console.WriteLine();
            Console.WriteLine("게임 시작!");
            Console.WriteLine("보이는 단어를 그대로 입력하고 엔터를 누르세요.");
            Console.WriteLine("시간이 끝나면 자동으로 종료됩니다.");
            Console.WriteLine();

            while (DateTime.Now < endTime)
            {
                string target = words[random.Next(words.Count)];
                Console.Write($"단어: {target} >>> ");

                // 남은 시간이 거의 없을 때 입력 대기 때문에 약간 더 길어질 수 있음
                string input = Console.ReadLine();

                if (input == null)
                {
                    // Ctrl + Z 같은 경우
                    break;
                }

                totalWords++;
                totalChars += target.Length;

                int wordCorrectChars = CountCorrectChars(target, input);
                correctChars += wordCorrectChars;

                if (input == target)
                {
                    correctWords++;
                }
            }

            double duration = (DateTime.Now - startTime).TotalSeconds;
            if (duration <= 0)
            {
                duration = 0.1; // 0 나누기 방지용
            }

            // WPM 계산 (5타 = 1단어 기준)
            double wpm = (correctChars / 5.0) / (duration / 60.0);

            double accuracy = 0;
            if (totalChars > 0)
            {
                accuracy = (correctChars / (double)totalChars) * 100.0;
            }

            return new GameResult
            {
                PlayedAt = DateTime.Now,
                DurationSeconds = duration,
                TotalWords = totalWords,
                CorrectWords = correctWords,
                TotalChars = totalChars,
                CorrectChars = correctChars,
                Accuracy = accuracy,
                Wpm = wpm
            };
        }

        /// <summary>
        /// 목표 단어와 입력한 단어를 비교해서
        /// 같은 위치에서 같은 글자인 개수를 센다.
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

        /// <summary>
        /// 결과를 JSON 파일에 누적 저장합니다.
        /// 파일이 있으면 기존 리스트에 추가하고,
        /// 없으면 새 리스트를 만든 뒤 저장합니다.
        /// </summary>
        static void SaveResultToJson(GameResult result, string jsonFilePath)
        {
            List<GameResult> results;

            if (File.Exists(jsonFilePath))
            {
                try
                {
                    string existingJson = File.ReadAllText(jsonFilePath);

                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        results = JsonSerializer.Deserialize<List<GameResult>>(existingJson)
                                  ?? new List<GameResult>();
                    }
                    else
                    {
                        results = new List<GameResult>();
                    }
                }
                catch
                {
                    // 기존 파일이 깨져 있으면 새로 시작
                    results = new List<GameResult>();
                }
            }
            else
            {
                results = new List<GameResult>();
            }

            results.Add(result);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string newJson = JsonSerializer.Serialize(results, options);
            File.WriteAllText(jsonFilePath, newJson, Encoding.UTF8);
        }
    }

    /// <summary>
    /// 한 번 플레이한 결과를 JSON으로 저장하기 위한 클래스.
    /// </summary>
    public class GameResult
    {
        public DateTime PlayedAt { get; set; }
        public double DurationSeconds { get; set; }
        public int TotalWords { get; set; }
        public int CorrectWords { get; set; }
        public int TotalChars { get; set; }
        public int CorrectChars { get; set; }
        public double Accuracy { get; set; }
        public double Wpm { get; set; }
    }
}
