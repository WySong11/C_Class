using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace PracticeLinq
{
    public record Student(int Id, string Name, int Age, int Score, string ClassName);
    public record ClassInfo(string ClassName, string Teacher);

    public class Program
    {
        static void Main(string[] args)
        {
            // 예제 데이터
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

            WriteLine("원본 학생 목록:");
            foreach (var s in students)
                WriteLine($"  {s.Id}: {s.Name}, {s.Age}세, 점수 {s.Score}, 반 {s.ClassName}");
            WriteLine();

            // 1) 필터링(Where) — 점수 85 이상인 학생
            var highScores = students.Where(s => s.Score >= 85);
            WriteLine("1) 점수 85점 이상 (Where, 메서드 문법):");
            foreach (var s in highScores)
                WriteLine($"  {s.Name} ({s.Score})");
            WriteLine();

            // 2) 투영(Select) — 이름만 추출
            var names = students.Select(s => s.Name);
            WriteLine("2) 이름만 추출 (Select):");
            WriteLine("  " + string.Join(", ", names));
            WriteLine();

            // 3) 정렬(OrderBy / OrderByDescending)
            var byScoreDesc = students.OrderByDescending(s => s.Score);
            WriteLine("3) 점수 내림차순 정렬:");
            foreach (var s in byScoreDesc)
                WriteLine($"  {s.Name}: {s.Score}");
            WriteLine();

            // 4) 그룹화(GroupBy) — 반별 그룹과 평균 점수
            var groupByClass = students.GroupBy(s => s.ClassName)
                                       .Select(g => new { Class = g.Key, Count = g.Count(), Avg = g.Average(x => x.Score) });

            WriteLine("4) 반별 학생 수와 평균 점수 (GroupBy):");
            foreach (var g in groupByClass)
                WriteLine($"  반 {g.Class}: 학생수={g.Count}, 평균={g.Avg:F1}");
            WriteLine();

            // 5) 조인(Join) — 학생과 반 선생님 연결
            var studentWithTeacher = from s in students
                                     join c in classes on s.ClassName equals c.ClassName
                                     select new { s.Name, s.Score, c.Teacher };

            WriteLine("5) 학생과 반 선생님 연결 (Join, 쿼리 문법):");
            foreach (var item in studentWithTeacher)
                WriteLine($"  {item.Name} ({item.Score}) - 선생님: {item.Teacher}");
            WriteLine();

            // 6) 집계(Aggregate/Any/FirstOrDefault 등)
            var averageScore = students.Average(s => s.Score);
            var anyPerfect = students.Any(s => s.Score >= 95);
            var topStudent = students.OrderByDescending(s => s.Score).FirstOrDefault();

            WriteLine("6) 집계 예시:");
            WriteLine($"  전체 평균 점수: {averageScore:F1}");
            WriteLine($"  95점 이상 학생 존재 여부: {anyPerfect}");
            WriteLine($"  최고 점수 학생: {topStudent?.Name} ({topStudent?.Score})");
        }
    }
}