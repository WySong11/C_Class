using System;
using System.Collections.Generic;
using System.Globalization;

namespace PracticeDelegateLambdaLinqTryCatch
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

        public double ClassAverage()
        {
            if (_scores.Count == 0) return 0;
            int sum = 0;
            for (int i = 0; i < _scores.Count; i++) sum += _scores[i].Point;
            return (double)sum / _scores.Count;
        }

        // 상위 n명: (학생, 평균) 리스트
        public List<(string Student, double Avg)> TopStudents(int n)
        {
            var totals = new Dictionary<string, (int sum, int cnt)>();
            for (int i = 0; i < _scores.Count; i++)
            {
                var s = _scores[i];
                if (!totals.ContainsKey(s.Student)) totals[s.Student] = (0, 0);
                var t = totals[s.Student];
                totals[s.Student] = (t.sum + s.Point, t.cnt + 1);
            }

            var result = new List<(string Student, double Avg)>();
            foreach (var kv in totals)
            {
                double avg = kv.Value.cnt == 0 ? 0 : (double)kv.Value.sum / kv.Value.cnt;
                result.Add((kv.Key, avg));
            }

            result.Sort((a, b) => b.Avg.CompareTo(a.Avg));
            if (n < result.Count) return result.GetRange(0, n);
            return result;
        }

        // 필터 쿼리: 조건에 맞는 Score들을 새 리스트로 반환
        public List<Score> Query(Func<Score, bool> predicate)
        {
            var list = new List<Score>();
            for (int i = 0; i < _scores.Count; i++)
            {
                var s = _scores[i];
                if (predicate(s)) list.Add(s);
            }
            return list;
        }

        // 조건 평균
        public double AverageOf(Func<Score, bool> predicate)
        {
            int sum = 0;
            int cnt = 0;
            for (int i = 0; i < _scores.Count; i++)
            {
                var s = _scores[i];
                if (predicate(s))
                {
                    sum += s.Point;
                    cnt++;
                }
            }
            return cnt == 0 ? 0 : (double)sum / cnt;
        }

        public void CheckAvgAlert(double threshold)
        {
            var avg = ClassAverage();
            if (avg >= threshold)
                AvgReached?.Invoke(this, new AvgReachedEventArgs(avg));
        }

        // 확장 X1: 과목별 평균 (Subject, Avg)
        public List<(string Subject, double Avg)> SubjectAverages()
        {
            var totals = new Dictionary<string, (int sum, int cnt)>();
            for (int i = 0; i < _scores.Count; i++)
            {
                var s = _scores[i];
                if (!totals.ContainsKey(s.Subject)) totals[s.Subject] = (0, 0);
                var t = totals[s.Subject];
                totals[s.Subject] = (t.sum + s.Point, t.cnt + 1);
            }

            var list = new List<(string Subject, double Avg)>();
            foreach (var kv in totals)
            {
                double avg = kv.Value.cnt == 0 ? 0 : (double)kv.Value.sum / kv.Value.cnt;
                list.Add((kv.Key, avg));
            }
            list.Sort((a, b) => b.Avg.CompareTo(a.Avg));
            return list;
        }

        // 확장 X2: 개인 과목별 최고점 (Subject, Max)
        public List<(string Subject, int Max)> PersonalBestBySubject(string student)
        {
            var best = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < _scores.Count; i++)
            {
                var s = _scores[i];
                if (!s.Student.Equals(student, StringComparison.OrdinalIgnoreCase)) continue;
                if (!best.ContainsKey(s.Subject) || s.Point > best[s.Subject]) best[s.Subject] = s.Point;
            }

            var list = new List<(string Subject, int Max)>();
            foreach (var kv in best) list.Add((kv.Key, kv.Value));
            list.Sort((a, b) => b.Max.CompareTo(a.Max));
            return list;
        }

        // 확장 X3: 최고 평균 학생 한 명
        public (string Student, double Avg) TopStudent()
        {
            var tops = TopStudents(int.MaxValue);
            return tops.Count == 0 ? (null, 0) : tops[0];
        }
    }

    public static class Program
    {
        // 여러 조건을 AND로 결합
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
            engine.AvgReached += (s, e) =>
            {
                Console.WriteLine($"[EVENT] Class average reached {e.Average:F1}");
                var top = engine.TopStudent();
                if (!string.IsNullOrEmpty(top.Student))
                    Console.WriteLine($"[EVENT] Top student: {top.Student} {top.Avg:F1}");
            };

            double avgThreshold = 85;
            int defaultTop = 3;

            try
            {
                engine.LoadSeed();
                Console.WriteLine("[OK] Seed loaded");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FATAL] Seed load failed: {ex.Message}");
                return;
            }

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

                        // 확장 X1: 과목별 평균 함께 출력
                        var subjectAvgs = engine.SubjectAverages();
                        for (int i = 0; i < subjectAvgs.Count; i++)
                            Console.WriteLine($"SubjectAverage {subjectAvgs[i].Subject} {subjectAvgs[i].Avg:F2}");

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

                        // 점수 내림차순 정렬
                        q.Sort((a, b) => b.Point.CompareTo(a.Point));

                        for (int i = 0; i < q.Count; i++)
                            Console.WriteLine($"{q[i].Student} {q[i].Subject} {q[i].Point} {q[i].TakenAt:yyyy-MM-dd}");

                        var personalAvg = engine.AverageOf(pred);
                        Console.WriteLine($"PersonalAverage={personalAvg:F2}");

                        // 확장 X2: 과목별 최고점
                        var best = engine.PersonalBestBySubject(name);
                        for (int i = 0; i < best.Count; i++)
                            Console.WriteLine($"PersonalBest {best[i].Subject} {best[i].Max}");
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
                catch (FormatException fe)
                {
                    Console.WriteLine($"[ERR] Invalid format: {fe.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERR] {ex.GetType().Name}: {ex.Message}");
                }
            }

            Console.WriteLine("Bye");
        }
    }
}