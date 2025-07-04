using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Employee
{
    public int ID { get; set; }
    public string? Name { get; set; }
    public string? Department { get; set; }
    public string? Email { get; set; }
}

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("CSV 파일 경로를 입력하세요 (기본값: test.csv):");
        string? filePath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(filePath))
            filePath = "test.csv";

        // 기존 직원 목록 불러오기
        var employees = new List<Employee>();
        if (File.Exists(filePath))
        {
            try
            {
                employees = ReadCSV(filePath);
                Console.WriteLine("\n기존 CSV 파일에서 직원 목록을 불러왔습니다.\n");
                PrintEmployees(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nCSV 파일 읽기 중 오류 발생: {ex.Message}\n");
            }
        }
        else
        {
            Console.WriteLine("\n새로운 CSV 파일을 생성합니다.\n");
        }

        // 신규 직원 입력
        var newEmployees = InputEmployees();

        // 기존 데이터와 신규 데이터 병합
        employees.AddRange(newEmployees);

        // 입력된 데이터가 있을 때만 저장
        if (employees.Count > 0)
        {
            try
            {
                WriteCSV(employees, filePath);
                Console.WriteLine("\nCSV 파일이 성공적으로 생성되었습니다!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CSV 저장 중 오류 발생: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("저장할 직원 정보가 없습니다.");
        }

        // 저장된 데이터 읽어오기
        try
        {
            var loaded = ReadCSV(filePath);
            
            Console.WriteLine("\nCSV 파일에서 읽은 직원 목록:");
            PrintEmployees(loaded);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CSV 읽기 중 오류 발생: {ex.Message}");
        }
    }

    static List<Employee> InputEmployees()
    {
        var employees = new List<Employee>();
        while (true)
        {
            Console.WriteLine("직원 정보를 입력하세요 (ID,이름,부서,이메일) 또는 엔터로 종료:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) break;

            var values = input.Split(',');
            if (values.Length < 4)
            {
                Console.WriteLine("입력 형식이 올바르지 않습니다.");
                continue;
            }

            if (!int.TryParse(values[0], out int id))
            {
                Console.WriteLine("ID는 숫자여야 합니다.");
                continue;
            }

            employees.Add(new Employee
            {
                ID = id,
                Name = values[1],
                Department = values[2],
                Email = values[3]
            });
        }
        return employees;
    }

    // CSV 파일에 직원 목록을 저장
    static void WriteCSV(List<Employee> employees, string filePath)
    {
        // StreamWriter를 사용하여 CSV 파일 쓰기
        // StreamWriter는 텍스트 기반 파일을 쓰기 위한 클래스입니다.
        // StreamWriter는 파일을 UTF-8 인코딩으로 씁니다.
        // 파일이 없으면 새로 생성하고, 있으면 덮어씁니다.

        // using 키워드를 사용하여 StreamWriter를 자동으로 닫습니다.
        // StreamWriter를 자원은 IDisposable 인터페이스를 구현하므로 using 블록을 사용하여 자동으로 닫을 수 있습니다.

        using (var writer = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            writer.WriteLine("ID,Name,Department,Email");
            foreach (var emp in employees)
            {
                string name = QuoteIfNeeded(emp.Name);
                string dept = QuoteIfNeeded(emp.Department);
                string email = QuoteIfNeeded(emp.Email);
                writer.WriteLine($"{emp.ID},{name},{dept},{email}");
            }
        }
    }

    // CSV 파일에서 직원 목록을 읽어오기
    static List<Employee> ReadCSV(string filePath = "test.csv")
    {
        var employees = new List<Employee>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"파일이 존재하지 않습니다: {filePath}");
            return employees;
        }

        // StreamReader를 사용하여 CSV 파일 읽기
        // StreamReader를 텍스트 기반 파일을 읽기 위한 클래스입니다.
        // StreamReader는 파일을 UTF-8 인코딩으로 읽습니다.
        // 파일이 없으면 빈 리스트를 반환

        // using 키워드를 사용하여 StreamReader를 자동으로 닫습니다.
        // StreamReader 자원은 IDisposable 인터페이스를 구현하므로 using 블록을 사용하여 자동으로 닫을 수 있습니다.

        //using (StreamReader reader = new StreamReader(filePath))
        //{
        // 파일 읽기 작업 수행
        //}

        // 이렇게 쓰면, reader 객체의 수명이 using 블록이 끝나는 시점까지로 한정되고,
        // 그 직후에 Dispose() 메서드가 자동으로 호출돼요.즉, 아래 코드를 쓴 것과 같아요:

        //StreamReader reader = new StreamReader(filePath);
        //try
        //{
        //    // 작업
        //}
        //finally
        //{
        //    reader.Dispose(); // 리소스 해제
        //}


        using (var reader = new StreamReader(filePath, Encoding.UTF8))
        {
            bool isHeader = true;

            // EndOfStream 프로퍼티는 파일의 끝에 도달했는지 여부를 나타내는 속성입니다.
            // bool 속석으로 true이면 파일의 끝에 도달했음을 의미합니다.
            // false이면 아직 읽을 데이터가 남아있음을 의미합니다.            

            while (!reader.EndOfStream)
            {
                // ReadLine 메서드를 사용하여 한 줄씩 읽습니다.
                var line = reader.ReadLine();
                if (line == null) continue;

                // Header가 있는 경우 첫 줄은 건너뜁니다.
                if (isHeader) { isHeader = false; continue; }

                var values = ParseCsvLine(line);
                if (values.Count < 4) continue;

                if (!int.TryParse(values[0], out int id)) continue;

                employees.Add(new Employee
                {
                    ID = id,
                    Name = values[1],
                    Department = values[2],
                    Email = values[3]
                });
            }
        }
        return employees;
    }

    static void PrintEmployees(List<Employee> employees)
    {
        Console.WriteLine("\nID | Name | Department | Email");
        Console.WriteLine("---------------------------------");
        foreach (var emp in employees)
        {
            Console.WriteLine($"{emp.ID} | {emp.Name} | {emp.Department} | {emp.Email}");
        }
        Console.WriteLine();
    }

    // CSV 필드에 필요한 경우 따옴표로 감싸기
    static string QuoteIfNeeded(string? field)
    {
        // field가 null인 경우 빈 문자열을 반환
        if (field == null) return "";

        // field가 빈 문자열인 경우 빈 문자열을 반환
        // 필드에 쉼표나 따옴표가 있으면 따옴표로 감싸고, 내부 따옴표는 이중 따옴표로 대체

        if (field.Contains(",") || field.Contains("\""))
            return $"\"{field.Replace("\"", "\"\"")}\"";
        return field;
    }

    // CSV 라인 파싱
    static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();

        // 현재 필드의 내용을 저장할 StringBuilder

        // 일반적인 string은** 불변(immutable)**이라서 문자열을 바꿀 때마다 새로운 문자열 객체를 생성해요.
        // 이로 인해 성능이 저하될 수 있어요.

        // StringBuilder는 문자열을 효율적으로 수정할 수 있는 가변(mutable) 객체입니다.
        // StringBuilder는 문자열을 추가, 삭제, 수정할 때 성능이 좋습니다.
        // StringBuilder는 내부적으로 문자열을 버퍼에 저장하고, 필요할 때마다 크기를 조정합니다.

        var sb = new StringBuilder();

        // 따옴표 안에 있는지 여부를 나타내는 플래그
        bool inQuotes = false;
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            // 따옴표가 나오면
            if (c == '\"')
            {
                // 만약 현재 따옴표가 열려있다면, 다음 문자가 또 따옴표인지 확인
                // 만약 다음 문자가 따옴표라면, 이중 따옴표로 처리하고, 인덱스를 증가시킵니다.
                // 만약 다음 문자가 따옴표가 아니라면, 따옴표 상태를 토글합니다.
                // inQuotes가 true이면 따옴표가 닫히는 상태로, false이면 열리는 상태로 간주합니다.
                // - "He said ""Hi""" → 필드 값은 He said "Hi"

                if (inQuotes && i + 1 < line.Length && line[i + 1] == '\"')
                {
                    // sb 끝에 이중 따옴표를 추가합니다.
                    sb.Append('\"');
                    i++;
                }
                else
                {
                    // 따옴표 상태를 토글합니다.
                    // inQuotes가 true이면 false로, false이면 true로 변경합니다.
                    inQuotes = !inQuotes;
                }
            }

            // 쉼표는 기본적으로 필드 구분자로 사용되지만, 따옴표 안에 있는 경우는 제외합니다.
            else if (c == ',' && !inQuotes)
            {
                // 현재 필드가 끝났으므로, StringBuilder에 저장된 내용을 결과 리스트에 추가합니다.
                result.Add(sb.ToString());
                sb.Clear();
            }
            else
            {
                // 현재 문자가 쉼표가 아니고, 따옴표 안에 있거나, 따옴표 밖에 있는 경우
                // StringBuilder에 현재 문자를 추가합니다.
                // 이 부분은 따옴표 안에 있는 경우에도 문자를 추가합니다.
                // - "Hello, World" → 필드 값은 Hello, World
                // - "Hello, ""World""" → 필드 값은 Hello, "World"
                // - "Hello, World", "Another, Field" → 필드 값은 Hello, World와 Another, Field

                sb.Append(c);
            }
        }
        result.Add(sb.ToString());
        return result;
    }
}

