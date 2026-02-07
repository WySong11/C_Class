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
            var names = students.Select(s => s.Name).ToList();

            WriteLine("2) 이름만 추출 (Select):");
            WriteLine("  " + string.Join(", ", names));
            WriteLine();


            // 3) 정렬(OrderBy / OrderByDescending)
            var sortedByScore = students.OrderByDescending(s => s.Score).ToList();

            WriteLine("3) 점수 내림차순 정렬 (OrderByDescending):");
            foreach (var s in sortedByScore)
                WriteLine($"  {s.Name} ({s.Score})");
            WriteLine();

            // 4) 그룹화(GroupBy) — 반별 그룹과 평균 점수
            var groupedByClass = students
                .GroupBy(s => s.ClassName)
                .Select(g => new
                {
                    ClassName = g.Key,
                    AverageScore = g.Average(s => s.Score),
                    Students = g.ToList()
                });

            var groupedByClassList = from s in students
                                     group s by s.ClassName into g
                                     select new
                                     {
                                         ClassName = g.Key,
                                         AverageScore = g.Average(s => s.Score),
                                         Students = g.ToList()
                                     };

            WriteLine("4) 반별 그룹화 및 평균 점수 (GroupBy):");
            foreach (var group in groupedByClass)
            {
                WriteLine($"  반 {group.ClassName} - 평균 점수: {group.AverageScore:F2}");
                foreach (var s in group.Students)
                    WriteLine($"    {s.Name} ({s.Score})");
            }

            // 5) 조인(Join) — 학생과 반 선생님 연결

            var studentWithTeachersMethod = students.Join(classes,
                                                        s => s.ClassName,
                                                        c => c.ClassName,
                                                        (s, c) => new
                                                        {
                                                            s.Name,
                                                            s.Score,
                                                            s.ClassName,
                                                            c.Teacher
                                                        });

            var studentWithTeachers = from s in students
                                      join c in classes
                                      on s.ClassName equals c.ClassName
                                      select new
                                      {
                                          s.Name,
                                          s.Score,
                                          s.ClassName,
                                          c.Teacher
                                      };

            WriteLine("5) 학생과 반 선생님 연결 (Join):");
            foreach (var item in studentWithTeachers)
            {
                WriteLine($"  {item.Name} ({item.Score}) - 반 {item.ClassName}, 선생님: {item.Teacher}");
            }

            // 6) 집계(Aggregate/Any/FirstOrDefault 등)
            var averageScore = students.Average(s => s.Score);
            var hasPerfectScore = students.Any(s => s.Score == 100);
            var topStudent = students.OrderByDescending(s => s.Score).FirstOrDefault();

            WriteLine("6) 집계 예제:");
            WriteLine($"  전체 학생 평균 점수: {averageScore:F2}");
            WriteLine($"  만점자 존재 여부: {(hasPerfectScore ? "예" : "아니오")}");
            if (topStudent != null)
                WriteLine($"  최고 점수 학생: {topStudent.Name} ({topStudent.Score})");

            WriteLine();

            // orderby, thenby 예제
            // 점수 오름차순, 나이 오름차순 정렬 후 점수만 추출
            // orderby, thenby 차이점 확인
            // ThenBy 사용
            // 점수가 같을 경우 나이로 추가 정렬
            var scores = students.OrderBy(s => s.Score).ThenBy(s => s.Age).Select(s => s.Score);

            // OrderBy 두 번 사용
            // 나중에 적용된 OrderBy가 우선순위를 가짐
            var scoreList = students.OrderBy(s => s.Score)
                                .OrderBy(s => s.Age)
                                .Select(s => s.Score)
                                .ToList();


            WriteLine("점수 오름차순, 나이 오름차순 정렬 후 점수만 추출 (ThenBy):");

            foreach (var score in scores)
            {
                WriteLine($"  {score}");
            }

            WriteLine("점수 오름차순, 나이 오름차순 정렬 후 점수만 추출 (ToList):");

            foreach (var score in scoreList)
            {
                WriteLine($"  {score}");
            }
        }
    }
}