using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

// 샘플 데이터 (문제에서 공통으로 사용)
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

/*
문제 1 — 필터링(Where)
•	설명: 점수(score) 85 이상인 학생의 이름 목록을 반환하라.
•	함수 시그니처(예시): IEnumerable<string> GetHighScorers(IEnumerable<Student> students)
•	요구: 람다식으로 Where와 Select 사용.
•	기대 출력: ["민수", "영희", "지우", "수빈"]

문제 2 — 투영(Select) & 조건부 값 매핑
•	설명: 학생 목록을 Name + GradeCategory 문자열(점수 기준: 90 이상 "A", 80~89 "B", 그 외 "C")로 변환하라.
•	시그니처: IEnumerable<string> ProjectNameWithGrade(IEnumerable<Student> students)
•	요구: Select 안에서 람다식으로 조건부 연산(삼항 연산자 등) 사용.
•	예시 항목: "민수 (A)", "영희 (B)", ...

문제 3 — 정렬 + Take
•	설명: 점수 내림차순으로 정렬한 뒤 상위 N명의 Student 객체를 반환하라.
•	시그니처: IEnumerable<Student> TopNByScore(IEnumerable<Student> students, int n)
•	요구: OrderByDescending, Take를 람다로 사용.
•	예: TopNByScore(students, 3) → 수빈(95), 민수(92), 지우(88)

문제 4 — 그룹화(GroupBy) 및 집계
•	설명: 반(ClassName)별 평균 점수를 Dictionary<string, double> 형태로 반환하라.
•	시그니처: Dictionary<string, double> AverageScoreByClass(IEnumerable<Student> students)
•	요구: GroupBy, Average 사용(람다 인자 사용).
•	기대 예: { "A": 85.0, "B": 86.5, "C": 82.5 } (값 예시)
문제 5 — 컬렉션 Join (메서드 문법)
*/

namespace PracticeLinqQuery
{

    public static class LambdaExercisesByLinq
    {
        // 문제 1: 점수 85 이상인 학생의 이름 목록 반환 (List<string> 반환)
        public static List<string> GetHighScorers(List<Student> students)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            return students.Where(s => s.Score >= 85)
                           .Select(s => s.Name)
                           .ToList();
        }

        // 문제 2: 이름과 등급 문자열로 투영 (90+ => A, 80~89 => B, 그 외 => C)
        public static List<string> ProjectNameWithGrade(List<Student> students)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            return students.Select(s => $"{s.Name} ({(s.Score >= 90 ? "A" : s.Score >= 80 ? "B" : "C")})")
                           .ToList();
        }

        // 문제 3: 점수 내림차순 정렬 후 상위 n명 반환 (List<Student> 반환)
        public static List<Student> TopNByScore(List<Student> students, int n)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            if (n <= 0) return new List<Student>();
            return students.OrderByDescending(s => s.Score)
                           .Take(n)
                           .ToList();
        }

        // 문제 4: 반별 평균 점수를 Dictionary로 반환
        public static Dictionary<string, double> AverageScoreByClass(List<Student> students)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            return students.GroupBy(s => s.ClassName)
                           .ToDictionary(g => g.Key, g => g.Average(x => x.Score));
        }

        // 문제 5: students와 classes를 Join하여 "이름 (점수) - 선생님" 문자열 리스트 반환 (List<string> 반환)
        public static List<string> StudentWithTeacher(List<Student> students, List<ClassInfo> classes)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            if (classes is null) throw new ArgumentNullException(nameof(classes));

            return students.Join(
                    classes,
                    s => s.ClassName,
                    c => c.ClassName,
                    (s, c) => $"{s.Name} ({s.Score}) - {c.Teacher}"
                )
                .ToList();
        }

        // 문제 6: Id 오름차순으로 정렬한 뒤 이름을 CSV 문자열로 합침
        public static string JoinNamesCsv(List<Student> students)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));

            // Aggregate 대신 string.Join을 사용해 간결하고 안전하게 처리
            var names = students.OrderBy(s => s.Id)
                                .Select(s => s.Name)
                                .ToList();

            return string.Join(",", names);
        }
    }
}

namespace PracticeLinqQuery
{
    public static class LambdaExercises
    {
        // 문제 1: 점수 85 이상인 학생의 이름 목록 반환 (List<string> 반환)
        public static List<string> GetHighScorers(List<Student> students)
        {
            var result = new List<string>();
            foreach (var s in students)
            {
                if (s.Score >= 85) result.Add(s.Name);
            }
            return result;
        }

        // 문제 2: 이름과 등급 문자열로 투영 (90+ => A, 80~89 => B, 그 외 => C)
        public static List<string> ProjectNameWithGrade(List<Student> students)
        {
            var result = new List<string>(students.Count);
            foreach (var s in students)
            {
                string grade = s.Score >= 90 ? "A" : s.Score >= 80 ? "B" : "C";
                result.Add($"{s.Name} ({grade})");
            }
            return result;
        }

        // 문제 3: 점수 내림차순 정렬 후 상위 n명 반환 (List<Student> 반환)
        public static List<Student> TopNByScore(List<Student> students, int n)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            if (n <= 0) return new List<Student>();

            // 원본을 변경하지 않기 위해 복사본 생성
            var copy = new List<Student>(students);
            copy.Sort((a, b) => b.Score.CompareTo(a.Score)); // 내림차순

            var take = Math.Min(n, copy.Count);
            var result = new List<Student>(take);
            for (int i = 0; i < take; i++) result.Add(copy[i]);
            return result;
        }

        // 문제 4: 반별 평균 점수를 Dictionary로 반환
        public static Dictionary<string, double> AverageScoreByClass(List<Student> students)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));

            // 누적 합과 카운트 저장
            var sums = new Dictionary<string, (int Sum, int Count)>(StringComparer.Ordinal);
            foreach (var s in students)
            {
                if (sums.TryGetValue(s.ClassName, out var acc))
                {
                    acc.Sum += s.Score;
                    acc.Count += 1;
                    sums[s.ClassName] = acc;
                }
                else
                {
                    sums[s.ClassName] = (s.Score, 1);
                }
            }

            var result = new Dictionary<string, double>(StringComparer.Ordinal);
            foreach (var kvp in sums)
            {
                var (sum, count) = kvp.Value;
                result[kvp.Key] = count == 0 ? 0.0 : (double)sum / count;
            }
            return result;
        }

        // 문제 5: students와 classes를 Join하여 "이름 (점수) - 선생님" 문자열 리스트 반환 (List<string> 반환)
        public static List<string> StudentWithTeacher(List<Student> students, List<ClassInfo> classes)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            if (classes is null) throw new ArgumentNullException(nameof(classes));

            // classes를 딕셔너리로 만들어 빠르게 조회
            var teacherByClass = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var c in classes)
            {
                // 마지막 값으로 덮어쓰도록 함 (같은 반 이름이 여러개면 마지막 것이 사용됨)
                teacherByClass[c.ClassName] = c.Teacher;
            }

            var result = new List<string>();
            foreach (var s in students)
            {
                if (teacherByClass.TryGetValue(s.ClassName, out var teacher))
                {
                    result.Add($"{s.Name} ({s.Score}) - {teacher}");
                }
                // 매칭되는 반이 없으면 스킵 (원하시면 빈 문자열/특정 텍스트 추가로 변경 가능)
            }

            return result;
        }

        // 문제 6: Id 오름차순으로 정렬한 뒤 이름을 CSV 문자열로 합침
        public static string JoinNamesCsv(List<Student> students)
        {
            if (students is null) throw new ArgumentNullException(nameof(students));
            if (students.Count == 0) return string.Empty;

            var copy = new List<Student>(students);
            copy.Sort((a, b) => a.Id.CompareTo(b.Id)); // Id 오름차순

            var sb = new StringBuilder();
            for (int i = 0; i < copy.Count; i++)
            {
                if (i > 0) sb.Append(',');
                sb.Append(copy[i].Name);
            }
            return sb.ToString();
        }
    }
}