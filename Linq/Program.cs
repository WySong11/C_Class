using System;
using System.Linq; // LINQ 네임스페이스 추가
using System.Collections.Generic; // List<T> 사용을 위해 추가

// LINQ(Language Integrated Query)는 컬렉션 데이터를 쿼리하는 강력하고 간결한 방법을 제공합니다.
// SQL과 비슷한 문법을 사용하면서도 C# 언어 안에 통합되어 있어 코드가 깔끔하고 이해하기 쉬워집니다.
// LINQ를 사용하면 배열, 리스트, 딕셔너리 등 다양한 컬렉션에 대해 필터링, 정렬, 그룹화, 집계 등의 작업을 쉽게 수행할 수 있습니다.

// Where() : 조건에 맞는 요소를 필터링합니다.
// Select() : 각 요소를 변환합니다.
// GroupBy() : 요소를 특정 키를 기준으로 그룹화합니다.
// OrderBy() : 요소를 오름차순으로 정렬합니다.
// OrderByDescending() : 요소를 내림차순으로 정렬합니다.
// Average() : 숫자 컬렉션의 평균을 계산합니다.
// Count() : 컬렉션의 요소 개수를 계산합니다.
// SelectMany() : 다중 컬렉션을 단일 컬렉션으로 평탄화합니다.
// ToList() : IEnumerable<T>를 List<T>로 변환합니다.
// ToArray() : IEnumerable<T>를 T[] 배열로 변환합니다.
// Join() : 두 컬렉션을 특정 키를 기준으로 결합합니다.

public class Employee
{
    public string? Name { get; set; }
    public string? Department { get; set; }
}

public class Program
{
    static void Main(string[] args)
    {
        ////////////////////////////////////////////////////////////////////////////////////////
        // 예제 데이터: 정수 리스트
        var numbers = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // LINQ를 사용하여 짝수만 선택하고, 제곱값으로 변환
        var evenSquares = numbers
            .Where(n => n % 2 == 0)   // 짝수 필터링
            .Select(n => n * n);      // 제곱값 변환

        // 결과 출력
        Console.WriteLine("짝수의 제곱값:");
        foreach (var num in evenSquares)
        {
            Console.WriteLine(num);
        }
        Console.WriteLine();

        ////////////////////////////////////////////////////////////////////////////////////////
        // LINQ를 사용하여 홀수/짝수로 그룹화
        var grouped = numbers
            .GroupBy(n => n % 2 == 0 ? "짝수" : "홀수");

        // 그룹별로 출력
        foreach (var group in grouped)
        {
            Console.WriteLine($"{group.Key} 그룹:");
            foreach (var num in group)
            {
                Console.Write($"{num} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();

        ////////////////////////////////////////////////////////////////////////////////////////
        // LINQ를 사용하여 문자열 리스트에서 특정 문자로 시작하는 단어 필터링
        // 예제 데이터: 직원 리스트
        var employees = new List<Employee>
        {
            new Employee { Name = "홍길동", Department = "클라이언트" },
            new Employee { Name = "김철수", Department = "서버" },
            new Employee { Name = "아영", Department = "디자이너" },
            new Employee { Name = "박민수", Department = "모델러" },
            new Employee { Name = "최지우", Department = "애니메이터" },
            new Employee { Name = "김태형", Department = "클라이언트" },
            new Employee { Name = "유수영", Department = "클라이언트" },
            new Employee { Name = "한창완", Department = "서버" },
            new Employee { Name = "김정은", Department = "디자이너" },
            new Employee { Name = "이도", Department = "모델러" },
        };

        // LINQ를 사용하여 부서별로 그룹화
        var groups1 = employees.GroupBy(e => e.Department);

        // 그룹별로 출력
        foreach (var group in groups1)
        {
            Console.WriteLine($"{group.Key} 부서:");
            foreach (var emp in group)
            {
                Console.WriteLine($"- {emp.Name}");
            }
        }
        Console.WriteLine();

        ////////////////////////////////////////////////////////////////////////////////////////
        // LINQ를 사용하여 부서별로 그룹화하고, 그룹과 그룹 내 데이터 모두 정렬
        var groups2 = employees
            .GroupBy(e => e.Department)
            .OrderBy(g => g.Key); // 부서명 기준 오름차순 정렬

        foreach (var group in groups2)
        {
            Console.WriteLine($"{group.Key} 부서:");
            // 그룹 내 직원 이름 오름차순 정렬
            foreach (var emp in group.OrderBy(e => e.Name))
            {
                Console.WriteLine($"- {emp.Name}");
            }
        }
        Console.WriteLine();

        ////////////////////////////////////////////////////////////////////////////////////////
        // LINQ를 사용하여 부서별로 그룹화하고, 각 부서의 직원 수와 이름 길이 평균 집계
        var departmentStats1 = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                EmployeeCount = g.Count(),
                NameLengthAverage = g.Average(e => e.Name != null ? e.Name.Length : 0)
            });

        foreach (var stat in departmentStats1)
        {
            Console.WriteLine($"{stat.Department} 부서: 인원수={stat.EmployeeCount}, 이름 길이 평균={stat.NameLengthAverage:F1}");
        }
        Console.WriteLine();

        ////////////////////////////////////////////////////////////////////////////////////////
        // LINQ를 사용하여 부서별로 그룹화, 집계, 그리고 집계 결과로 정렬
        var departmentStats3 = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                EmployeeCount = g.Count(),
                NameLengthAverage = g.Average(e => e.Name != null ? e.Name.Length : 0)
            })
            .OrderByDescending(stat => stat.EmployeeCount); // 직원 수 내림차순 정렬

        foreach (var stat in departmentStats3)
        {
            Console.WriteLine($"{stat.Department} 부서: 인원수={stat.EmployeeCount}, 이름 길이 평균={stat.NameLengthAverage:F1}");
        }
        Console.WriteLine();

        ////////////////////////////////////////////////////////////////////////////////////////
        // LINQ를 사용하여 부서별로 그룹화, 집계, 필터링, 그리고 집계값(이름 길이 평균) 기준 정렬
        var departmentStats4 = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                EmployeeCount = g.Count(),
                NameLengthAverage = g.Average(e => e.Name != null ? e.Name.Length : 0)
            })
            .Where(stat => stat.EmployeeCount >= 2) // 직원 수 2명 이상인 부서만
            .OrderByDescending(stat => stat.NameLengthAverage); // 이름 길이 평균 내림차순 정렬

        foreach (var stat in departmentStats4)
        {
            Console.WriteLine($"{stat.Department} 부서: 인원수={stat.EmployeeCount}, 이름 길이 평균={stat.NameLengthAverage:F1}");
        }
        Console.WriteLine();

        ////////////////////////////////////////////////////////////////////////////////////////
        // LINQ를 사용하여 부서별로 그룹화, 집계, 그리고 여러 기준으로 정렬
        var departmentStats5 = employees
            .GroupBy(e => e.Department)
            .Select(g => new
            {
                Department = g.Key,
                EmployeeCount = g.Count(),
                NameLengthAverage = g.Average(e => e.Name != null ? e.Name.Length : 0)
            })
            .OrderByDescending(stat => stat.EmployeeCount)      // 1차: 직원 수 내림차순
            .ThenBy(stat => stat.NameLengthAverage);            // 2차: 이름 길이 평균 오름차순

        foreach (var stat in departmentStats5)
        {
            Console.WriteLine($"{stat.Department} 부서: 인원수={stat.EmployeeCount}, 이름 길이 평균={stat.NameLengthAverage:F1}");
        }
        Console.WriteLine();
    }
}

