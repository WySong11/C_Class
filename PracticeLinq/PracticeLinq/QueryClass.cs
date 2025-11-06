using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace PracticeLinqQuery
{
    public record Student(int Id, string Name, int Age, int Score, string ClassName);
    public record ClassInfo(string ClassName, string Teacher);

    public class QueryClass
    {
        static void Main(string[] args)
        {
            var students = new List<Student>
        {
            new(1, "민수", 16, 92, "A"),
            new(2, "영희", 15, 85, "B"),
            new(3, "철수", 16, 78, "A"),
            new(4, "지우", 17, 88, "B"),
            new(5, "수빈", 15, 95, "C"),
            new(6, "현우", 16, 70, "C"),
        };

            var classes = new List<ClassInfo>
        {
            new("A", "김선생"),
            new("B", "박선생"),
            new("C", "이선생"),
        };

            WriteLine("LINQ: 쿼리 문법 vs 메서드 문법 비교\n");

            ShowWhere(students);
            ShowSelect(students);
            ShowOrderBy(students);
            ShowGroupBy(students);
            ShowJoin(students, classes);
        }

        // 1) Where (필터링)
        static void ShowWhere(IEnumerable<Student> students)
        {
            WriteLine("1) Where — 점수 85 이상인 학생 (메서드 vs 쿼리)");
            var methodResult = students.Where(s => s.Score >= 85).Select(s => s.Name);

            var queryResult = from s in students
                              where s.Score >= 85
                              select s.Name;

            WriteLine("  메서드 문법:  " + string.Join(", ", methodResult));
            WriteLine("  쿼리 문법:  " + string.Join(", ", queryResult));
            WriteLine();
        }

        // 2) Select (투영)
        static void ShowSelect(IEnumerable<Student> students)
        {
            WriteLine("2) Select — 이름과 점수만 추출");
            var methodResult = students.Select(s => new { s.Name, s.Score });
            var queryResult = from s in students
                              select new { s.Name, s.Score };

            WriteLine("  메서드 문법:");
            foreach (var x in methodResult) WriteLine($"    {x.Name}: {x.Score}");
            WriteLine("  쿼리 문법:");
            foreach (var x in queryResult) WriteLine($"    {x.Name}: {x.Score}");
            WriteLine();
        }

        // 3) OrderBy (정렬)
        static void ShowOrderBy(IEnumerable<Student> students)
        {
            WriteLine("3) OrderByDescending — 점수 내림차순 상위 3명");
            var methodResult = students.OrderByDescending(s => s.Score).Take(3);
            var queryResult = (from s in students
                               orderby s.Score descending
                               select s).Take(3);

            WriteLine("  메서드 문법:");
            foreach (var s in methodResult) WriteLine($"    {s.Name}: {s.Score}");
            WriteLine("  쿼리 문법:");
            foreach (var s in queryResult) WriteLine($"    {s.Name}: {s.Score}");
            WriteLine();
        }

        // 4) GroupBy (그룹화)
        static void ShowGroupBy(IEnumerable<Student> students)
        {
            WriteLine("4) GroupBy — 반별 학생 수와 평균 점수");
            var methodResult = students
                .GroupBy(s => s.ClassName)
                .Select(g => new { Class = g.Key, Count = g.Count(), Avg = g.Average(x => x.Score) });

            var queryResult = from s in students
                              group s by s.ClassName into g
                              select new { Class = g.Key, Count = g.Count(), Avg = g.Average(x => x.Score) };

            WriteLine("  메서드 문법:");
            foreach (var g in methodResult) WriteLine($"    반 {g.Class}: 학생수={g.Count}, 평균={g.Avg:F1}");
            WriteLine("  쿼리 문법:");
            foreach (var g in queryResult) WriteLine($"    반 {g.Class}: 학생수={g.Count}, 평균={g.Avg:F1}");
            WriteLine();
        }

        // 5) Join (조인)
        static void ShowJoin(IEnumerable<Student> students, IEnumerable<ClassInfo> classes)
        {
            WriteLine("5) Join — 학생과 반 선생님 연결");
            var methodResult = students.Join(
                classes,
                s => s.ClassName,
                c => c.ClassName,
                (s, c) => new { s.Name, s.Score, c.Teacher });

            var queryResult = from s in students
                              join c in classes on s.ClassName equals c.ClassName
                              select new { s.Name, s.Score, c.Teacher };

            WriteLine("  메서드 문법:");
            foreach (var x in methodResult) WriteLine($"    {x.Name} ({x.Score}) - 선생님: {x.Teacher}");
            WriteLine("  쿼리 문법:");
            foreach (var x in queryResult) WriteLine($"    {x.Name} ({x.Score}) - 선생님: {x.Teacher}");
            WriteLine();
        }
    }
}
