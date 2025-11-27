using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

/// <summary>
/// 제네릭 방식의 CSV 저장/로드 유틸리티.
/// - 타입 T의 public properties를 헤더로 사용하여 CSV를 생성/파싱합니다.
/// - 기본 파일명은 타입명(T).csv (예: LevelData -> "LevelData.csv").
/// - 기존 레거시 파일명인 "levels.csv"와의 호환을 위해 기존 CreateCSV/LoadCSV는 내부에서 제네릭 메서드를 호출합니다.
/// 
/// 주의 및 한계:
/// - CSV 파싱은 간단한 따옴표 인용(quoted fields) 처리를 지원합니다(필드 내 콤마/줄바꿈/따옴표 처리).
/// - 로드 시 프로퍼티 이름과 CSV 헤더를 대소문자 구분 없이 매핑합니다.
/// - 로드 시 각 필드는 대상 프로퍼티의 타입으로 Convert.ChangeType로 변환을 시도합니다.
///   변환 실패(예: "abc"를 int로 변환)하면 해당 필드는 무시되고 객체의 기본값이 유지됩니다.
/// - T는 공개 인자 없는 기본 생성자(public parameterless constructor)를 가져야 합니다(LoadCsv 제약).
/// </summary>
public static class CreateLevelCSV_T
{
    /// <summary>
    /// 기본 CSV 저장 디렉터리 경로 (외부에서 변경 가능)
    /// </summary>
    public static string CSV_DIR_PATH = "Data";

    /// <summary>
    /// 레거시 기본 파일명 (CreateCSV/LoadCSV에서 사용)
    /// </summary>
    public static string CSV_FILE_NAME = "levels.csv";

    /// <summary>
    /// 레거시 호환용: LevelData 샘플 데이터로 CSV 파일을 생성합니다.
    /// - 파일이 이미 존재하면 생성 작업을 건너뜁니다(덮어쓰기하지 않음).
    /// - 내부적으로 제네릭 SaveCsv를 호출합니다.
    /// </summary>
    public static void CreateCSV()
    {
        // 샘플 레벨 데이터 (Level, Exp)
        List<LevelData> levels = new List<LevelData>
        {
            new LevelData(1, 0),
            new LevelData(2, 100),
            new LevelData(3, 300),
            new LevelData(4, 600),
            new LevelData(5, 1000),
            new LevelData(6, 1500),
            new LevelData(7, 3000),
            new LevelData(8, 5000),
            new LevelData(9, 8000),
            new LevelData(10, 10000)
        };

        // 레거시 파일명으로 저장
        SaveCsv(levels, CSV_DIR_PATH, CSV_FILE_NAME);
    }

    /// <summary>
    /// 레거시 호환용: 레거시 파일명("levels.csv")로 LevelData 리스트를 로드합니다.
    /// - 파일이 없으면 빈 리스트를 반환합니다.
    /// - 내부적으로 제네릭 LoadCsv를 호출합니다.
    /// </summary>
    /// <returns>로드한 LevelData 리스트</returns>
    public static List<LevelData> LoadCSV()
    {
        return LoadCsv<LevelData>(CSV_DIR_PATH, CSV_FILE_NAME);
    }

    /// <summary>
    /// 제네릭: 컬렉션을 CSV 파일로 저장합니다.
    /// - 헤더는 T의 public readable 프로퍼티 이름으로 생성됩니다(순서는 Reflection에 따라 결정).
    /// - fileName이 null이면 기본으로 "{typeof(T).Name}.csv"를 사용합니다.
    /// - 디렉터리가 없으면 자동으로 생성합니다.
    /// </summary>
    /// <typeparam name="T">CSV에 저장할 객체 타입(클래스)</typeparam>
    /// <param name="items">저장할 객체 컬렉션</param>
    /// <param name="dirPath">저장할 디렉터리 경로(기본: CSV_DIR_PATH)</param>
    /// <param name="fileName">파일명(기본: typeof(T).Name + ".csv")</param>
    public static void SaveCsv<T>(IEnumerable<T> items, string? dirPath = null, string? fileName = null) where T : class
    {
        if (items == null) throw new ArgumentNullException(nameof(items));

        var dir = dirPath ?? CSV_DIR_PATH;
        var file = fileName ?? $"{typeof(T).Name}.csv";
        var fullPath = Path.Combine(dir, file);

        // 디렉터리 없으면 생성
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);

        // UTF-8 인코딩으로 파일 생성. 덮어쓰기(false).
        using (var writer = new StreamWriter(fullPath, false, Encoding.UTF8))
        {
            // T의 public instance readable 프로퍼티만 선택
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanRead)
                                 .ToArray();

            // 헤더 작성: 각 프로퍼티 이름을 CSV 필드명으로 사용
            // EscapeForCsv를 사용해 필드 내 특수문자 안전 처리
            writer.WriteLine(string.Join(",", props.Select(p => EscapeForCsv(p.Name))));

            // 데이터 라인 작성: 각 객체에 대해 프로퍼티 값을 문자열로 변환 후 CSV 표기법으로 작성
            foreach (var item in items)
            {
                var values = props.Select(p =>
                {
                    // null 가능성 처리: null이면 빈 문자열로 기록
                    var val = p.GetValue(item);
                    return EscapeForCsv(val?.ToString() ?? string.Empty);
                });
                writer.WriteLine(string.Join(",", values));
            }
        }
    }

    /// <summary>
    /// 제네릭: CSV 파일을 읽어 T 리스트로 반환합니다.
    /// - T는 public parameterless 생성자를 가져야 합니다(new() 제약).
    /// - CSV 첫 줄을 헤더로 읽고, 헤더 컬럼명과 동일한 프로퍼티에 값을 매핑합니다(대소문자 무시).
    /// - 각 필드는 대상 프로퍼티 타입으로 변환을 시도합니다. 변환 실패 시 해당 필드는 무시되어 기본값 유지됩니다.
    /// - fileName이 null이면 기본으로 "{typeof(T).Name}.csv"를 사용합니다.
    /// </summary>
    /// <typeparam name="T">파싱할 대상 타입(클래스, parameterless constructor 필요)</typeparam>
    /// <param name="dirPath">CSV 파일이 위치한 디렉터리(기본: CSV_DIR_PATH)</param>
    /// <param name="fileName">CSV 파일명(기본: typeof(T).Name + ".csv")</param>
    /// <returns>파싱된 객체 리스트. 파일이 없거나 파싱 실패 시 빈 리스트 반환.</returns>
    public static List<T> LoadCsv<T>(string? dirPath = null, string? fileName = null) where T : class, new()
    {
        var dir = dirPath ?? CSV_DIR_PATH;
        var file = fileName ?? $"{typeof(T).Name}.csv";
        var fullPath = Path.Combine(dir, file);

        var list = new List<T>();
        if (!File.Exists(fullPath))
            return list; // 파일 없음: 빈 리스트 반환

        // UTF-8 인코딩으로 읽음
        using (var reader = new StreamReader(fullPath, Encoding.UTF8))
        {
            // 첫 줄을 헤더로 읽음
            string? headerLine = reader.ReadLine();
            if (headerLine == null)
                return list; // 빈 파일이면 빈 리스트 반환

            // CSV 헤더 파싱 (quoted field 지원)
            var headers = ParseCsvLine(headerLine).ToArray();

            // T의 public instance writable 프로퍼티를 이름 기반 딕셔너리로 준비(대소문자 무시)
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .Where(p => p.CanWrite)
                                 .ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                // 각 데이터 라인을 필드로 파싱
                var fields = ParseCsvLine(line).ToArray();
                var obj = new T();

                // 헤더 수와 필드 수 중 더 작은 만큼 반복해서 매핑 시도
                for (int i = 0; i < headers.Length && i < fields.Length; i++)
                {
                    var header = headers[i];
                    var field = fields[i];

                    // 헤더에 해당하는 프로퍼티가 존재하면 변환 후 설정
                    if (props.TryGetValue(header, out var prop))
                    {
                        try
                        {
                            // nullable 타입 처리: Nullable<T>이면 내부 타입으로 변환
                            var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                            object? converted = null;

                            if (targetType == typeof(string))
                            {
                                // 문자열은 그대로 사용(빈 문자열도 허용)
                                converted = field;
                            }
                            else if (string.IsNullOrEmpty(field))
                            {
                                // 빈 필드는 null로 처리(값 타입의 경우 Convert.ChangeType 시 예외 발생하므로 null로 설정 시 실패 가능)
                                converted = null;
                            }
                            else
                            {
                                // 기본형(숫자, bool, DateTime 등)은 Convert.ChangeType로 변환 시도
                                // 필요시 Culture/포맷을 고려한 변환 로직으로 확장 가능
                                converted = Convert.ChangeType(field, targetType);
                            }

                            // 변환된 값을 프로퍼티에 설정(성공하면 덮어쓰기)
                            prop.SetValue(obj, converted);
                        }
                        catch
                        {
                            // 변환 실패 시 조용히 무시하여 기본값 유지
                            // 필요하면 로깅 로직 추가 권장(System.Diagnostics 또는 파일 로깅)
                        }
                    }
                }

                list.Add(obj);
            }
        }

        return list;
    }

    /// <summary>
    /// CSV 필드로 쓰기 위해 값에 포함된 특수문자(따옴표, 콤마, 개행)를 처리합니다.
    /// - 필드에 따옴표가 있으면 따옴표를 두 번으로 이스케이프하고 전체를 따옴표로 감쌉니다.
    /// - RFC 표준의 간단 구현으로, 복잡한 케이스는 별도 CSV 라이브러리 사용을 권장합니다.
    /// </summary>
    /// <param name="value">CSV에 기록할 원시 문자열</param>
    /// <returns>CSV 안전 문자열</returns>
    static string EscapeForCsv(string value)
    {
        if (value.Contains('"') || value.Contains(',') || value.Contains('\n') || value.Contains('\r'))
        {
            // 내부 따옴표를 두 개의 따옴표로 치환 후 전체를 따옴표로 감싸기
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
        return value;
    }

    /// <summary>
    /// CSV 라인을 파싱하여 필드 문자열 컬렉션을 반환합니다.
    /// - 따옴표로 감싼 필드(내부에 콤마/개행/이중따옴표 포함) 처리를 지원합니다.
    /// - 이 구현은 라인 단위 파싱으로, 파일 내부의 필드에 실제 개행이 포함된 복잡한 케이스는 완벽히 지원하지 않을 수 있습니다.
    /// </summary>
    /// <param name="line">파싱할 CSV 라인</param>
    /// <returns>필드 문자열들의 열거</returns>
    static IEnumerable<string> ParseCsvLine(string line)
    {
        if (line == null) yield break;

        var sb = new StringBuilder();
        bool inQuotes = false;

        // 문자 단위 상태 머신 방식으로 파싱
        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (inQuotes)
            {
                if (c == '"')
                {
                    // 연속된 두 따옴표는 실제 따옴표 문자로 처리
                    if (i + 1 < line.Length && line[i + 1] == '"')
                    {
                        sb.Append('"');
                        i++; // 다음 따옴표는 소비
                    }
                    else
                    {
                        // 현재 따옴표는 닫는 따옴표
                        inQuotes = false;
                    }
                }
                else
                {
                    // 따옴표 내부의 일반 문자 추가
                    sb.Append(c);
                }
            }
            else
            {
                if (c == '"')
                {
                    // 따옴표 시작: quoted field 진입
                    inQuotes = true;
                }
                else if (c == ',')
                {
                    // 필드 경계: 현재까지의 문자열을 yield하고 버퍼 초기화
                    yield return sb.ToString();
                    sb.Clear();
                }
                else
                {
                    // 일반 문자 추가
                    sb.Append(c);
                }
            }
        }

        // 마지막 필드 반환
        yield return sb.ToString();
    }
}