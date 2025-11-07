using System;
using System.Collections.Generic;
using System.Globalization;

namespace ScoreMini_NoIEnumerable_Starter_TODO_Catches
{
    public class Score
    {
        public string Student { get; set; }
        public string Subject { get; set; }
        public int Point { get; set; }
        public DateTime TakenAt { get; set; }
    }

    public class AvgReachedEventArgs : EventArgs
    {
        public double Average { get; }
        public AvgReachedEventArgs(double avg) => Average = avg;
    }

    public class ScoreEngine
    {
        public event EventHandler<AvgReachedEventArgs> AvgReached;

        private readonly List<Score> _scores = new();

        public void LoadSeed()
        {
            _scores.AddRange(new[]
            {
                new Score { Student = "Jae",  Subject = "Math",    Point = 88, TakenAt = new DateTime(2025,11,03) },
                new Score { Student = "Jae",  Subject = "Design",  Point = 92, TakenAt = new DateTime(2025,11,04) },
                new Score { Student = "Mina", Subject = "Math",    Point = 77, TakenAt = new DateTime(2025,11,03) },
                new Score { Student = "Mina", Subject = "Design",  Point = 81, TakenAt = new DateTime(2025,11,05) },
                new Score { Student = "Jun",  Subject = "Math",    Point = 95, TakenAt = new DateTime(2025,11,04) },
                new Score { Student = "Jun",  Subject = "Physics", Point = 84, TakenAt = new DateTime(2025,11,05) },
                new Score { Student = "Ara",  Subject = "Design",  Point = 73, TakenAt = new DateTime(2025,11,05) },
                new Score { Student = "Ara",  Subject = "Math",    Point = 69, TakenAt = new DateTime(2025,11,05) },
                new Score { Student = "Luc",  Subject = "Physics", Point = 91, TakenAt = new DateTime(2025,11,05) }
            });
        }

        // 이미 구현됨
        public double ClassAverage()
        {
            if (_scores.Count == 0) return 0;
            int sum = 0;
            for (int i = 0; i < _scores.Count; i++) sum += _scores[i].Point;
            return (double)sum / _scores.Count;
        }

        // TODO 1: 상위 n명 (학생, 평균) 리스트 반환. 사전 + for 루프만 사용.
        public List<(string Student, double Avg)> TopStudents(int n)
        {
            // 1) 학생별 합계/개수 누적
            // 2) (학생, 평균) 리스트 생성
            // 3) 평균 내림차순 정렬
            // 4) 상위 n명만 반환
            throw new NotImplementedException("TODO 1: TopStudents 구현");
        }

        // TODO 2: 조건에 맞는 Score들을 새 List로 반환. for 루프만 사용.
        public List<Score> Query(Func<Score, bool> predicate)
        {
            // 1) _scores 순회
            // 2) predicate(s)==true 인 것만 추가
            // 3) 결과 반환
            throw new NotImplementedException("TODO 2: Query 구현");
        }

        // TODO 3: 조건 평균. for 루프만 사용.
        public double AverageOf(Func<Score, bool> predicate)
        {
            // sum, cnt 누적 후 cnt==0이면 0, 아니면 sum/cnt
            throw new NotImplementedException("TODO 3: AverageOf 구현");
        }

        // 이미 구현됨
        public void CheckAvgAlert(double threshold)
        {
            var avg = ClassAverage();
            if (avg >= threshold)
                AvgReached?.Invoke(this, new AvgReachedEventArgs(avg));
        }

        // TODO 4: 과목별 평균 (Subject, Avg) 리스트. 사전 + for 루프만 사용.
        public List<(string Subject, double Avg)> SubjectAverages()
        {
            // 과목 -> (sum, cnt) 누적 후 Avg 내림차순 정렬
            throw new NotImplementedException("TODO 4: SubjectAverages 구현");
        }

        // TODO 5: 개인 과목별 최고점 (Subject, Max) 리스트. 사전 + for 루프만 사용.
        public List<(string Subject, int Max)> PersonalBestBySubject(string student)
        {
            // 과목별 최대값 갱신 후 Max 내림차순 정렬
            throw new NotImplementedException("TODO 5: PersonalBestBySubject 구현");
        }

        // TODO 6: 최고 평균 학생 한 명 반환. TopStudents 재사용 권장.
        public (string Student, double Avg) TopStudent()
        {
            // TopStudents(int.MaxValue) 사용해서 0번째 반환. 없으면 (null,0)
            throw new NotImplementedException("TODO 6: TopStudent 구현");
        }
    }

    public static class Program
    {
        // TODO 7: 여러 조건을 AND로 결합. for 루프만 사용.
        public static Func<Score, bool> AndAll(List<Func<Score, bool>> parts)
        {
            return s =>
            {
                for (int i = 0; i < parts.Count; i++)
                {
                    if (!parts[i](s)) return false;
                }
                return true;
            };
        }

        public static void Main(string[] args)
        {
            var engine = new ScoreEngine();

            // TODO C1: AvgReached 발생 시 부가 정보 출력 설계
            engine.AvgReached += (s, e) =>
            {
                Console.WriteLine($"[EVENT] Class average reached {e.Average:F1}");
                // TODO C1: 최고 평균 학생 한 줄 추가
            };

            double avgThreshold = 85;
            int defaultTop = 3;

            try
            {
                engine.LoadSeed();
                Console.WriteLine("[OK] Seed loaded");
            }
            // TODO C2: 초기화 실패 처리 구현
            // 예: 사용자 안내 메시지, 로깅, 복구 시나리오 등
            // 예시) Console.WriteLine($"[ERR] Seed load failed: {ex.Message}");
            // 필요 시 흐름 제어 결정(계속/종료)
            

            Console.WriteLine("Commands: avg | top n | find name | range yyyy-MM-dd..yyyy-MM-dd | set avg=V top=N | help | exit");

            while (true)
            {
                Console.Write("> ");
                var input = Console.ReadLine();
                if (input is null) continue;
                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase)) break;

                try
                {
                    if (input.Equals("avg", StringComparison.OrdinalIgnoreCase))
                    {
                        var avg = engine.ClassAverage();
                        Console.WriteLine($"ClassAverage={avg:F2}");

                        // TODO 9: 과목별 평균 함께 출력

                        engine.CheckAvgAlert(avgThreshold);
                    }
                    else if (input.StartsWith("top ", StringComparison.OrdinalIgnoreCase))
                    {
                        var nStr = input.Substring(4).Trim();
                        var n = int.Parse(nStr);
                        var list = engine.TopStudents(n);
                        for (int i = 0; i < list.Count; i++)
                            Console.WriteLine($"{list[i].Student} {list[i].Avg:F1}");
                    }
                    else if (input.Equals("top", StringComparison.OrdinalIgnoreCase))
                    {
                        var list = engine.TopStudents(defaultTop);
                        for (int i = 0; i < list.Count; i++)
                            Console.WriteLine($"{list[i].Student} {list[i].Avg:F1}");
                    }
                    else if (input.StartsWith("find ", StringComparison.OrdinalIgnoreCase))
                    {
                        var name = input.Substring(5).Trim();
                        var pred = AndAll(new List<Func<Score, bool>> { s => s.Student.Equals(name, StringComparison.OrdinalIgnoreCase) });
                        var q = engine.Query(pred);

                        // TODO 10: 점수 내림차순 정렬 구현

                        for (int i = 0; i < q.Count; i++)
                            Console.WriteLine($"{q[i].Student} {q[i].Subject} {q[i].Point} {q[i].TakenAt:yyyy-MM-dd}");

                        var personalAvg = engine.AverageOf(pred);
                        Console.WriteLine($"PersonalAverage={personalAvg:F2}");

                        // TODO 11: 개인 과목별 최고점 출력
                    }
                    else if (input.StartsWith("range ", StringComparison.OrdinalIgnoreCase))
                    {
                        var token = input.Substring(6).Trim();
                        var parts = token.Split("..");
                        if (parts.Length != 2) throw new FormatException("Use range yyyy-MM-dd..yyyy-MM-dd");

                        var from = DateTime.ParseExact(parts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
                        var to = DateTime.ParseExact(parts[1], "yyyy-MM-dd", CultureInfo.InvariantCulture);

                        var pred = AndAll(new List<Func<Score, bool>>
                        {
                            s => s.TakenAt.Date >= from.Date,
                            s => s.TakenAt.Date <= to.Date
                        });

                        var rangeAvg = engine.AverageOf(pred);
                        Console.WriteLine($"RangeAverage={rangeAvg:F2}");
                    }
                    else if (input.StartsWith("set ", StringComparison.OrdinalIgnoreCase))
                    {
                        var tokens = input.Substring(4).Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < tokens.Length; i++)
                        {
                            var kv = tokens[i].Split('=');
                            if (kv.Length != 2) throw new FormatException("Use key=value");
                            if (kv[0].Equals("avg", StringComparison.OrdinalIgnoreCase))
                                avgThreshold = double.Parse(kv[1]);
                            else if (kv[0].Equals("top", StringComparison.OrdinalIgnoreCase))
                                defaultTop = int.Parse(kv[1]);
                            else
                                throw new FormatException("Allowed keys: avg, top");
                        }
                        Console.WriteLine($"Set avg={avgThreshold} top={defaultTop}");
                    }
                    else if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("avg | top n | find name | range yyyy-MM-dd..yyyy-MM-dd | set avg=V top=N | exit");
                    }
                    else
                    {
                        Console.WriteLine("Unknown command");
                    }
                }
                // TODO C3: 형식 오류 처리 구현
                // 예: 잘못된 날짜/숫자 형식 안내 메시지
                // 예시) Console.WriteLine($"[ERR] Invalid format: {ex.Message}");

                // TODO C4: 일반 예외 처리 구현
                // 예: 알 수 없는 오류 로깅 및 사용자 가이드
                // 예시) Console.WriteLine($"[ERR] {ex.GetType().Name}: {ex.Message}");                
            }

            Console.WriteLine("Bye");
        }
    }
}
