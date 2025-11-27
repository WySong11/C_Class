using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

/// <summary>
/// 제네릭 JSON 저장/로드 유틸리티 클래스.
/// - 제네릭 타입 T에 대해 List<T>를 JSON으로 저장하고 다시 로드할 수 있습니다.
/// - 기본 저장 디렉터리와 기본 파일명은 공개 필드로 노출되어 있어 필요 시 변경할 수 있습니다.
/// 
/// 주의:
/// - 현재 구현은 파일 입출력 중 발생하는 예외를 호출자에게 전달합니다.
///   안정성이 필요하면 호출자에서 try/catch로 예외를 처리하거나 Try* 패턴을 추가하세요.
/// - 멀티스레드 환경에서는 파일 접근 동기화가 필요할 수 있습니다.
/// </summary>
public static class SaveLoadJson_T
{
    /// <summary>
    /// JSON 파일을 저장/로드할 기본 디렉터리 경로입니다.
    /// 기본값은 "Save"이며 외부에서 변경할 수 있습니다.
    /// </summary>
    public static string JSON_DIR_PATH = "Save";

    /// <summary>
    /// 레거시 기본 파일명입니다. 호출 시 fileName 매개변수를 넘기지 않으면 이 값이 사용됩니다.
    /// 일반적으로 타입 기반 파일명을 사용하려면 메서드 호출에서 fileName을 null로 두고
    /// typeof(T).Name 기반의 규칙을 적용하도록 변경할 수 있습니다.
    /// </summary>
    public static string JSON_FILE_NAME = "playerdata.json";

    /// <summary>
    /// 제네릭 리스트를 JSON으로 동기 저장합니다.
    /// - list: 저장할 객체 리스트입니다(null일 수 없음).
    /// - dirPath: 저장할 디렉터리(생략 시 JSON_DIR_PATH 사용).
    /// - fileName: 저장할 파일명(생략 시 JSON_FILE_NAME 사용).
    /// 
    /// 동작:
    /// 1. dirPath/fileName을 결합해 전체 경로를 만든다.
    /// 2. 디렉터리가 없으면 생성한다.
    /// 3. System.Text.Json을 이용해 리스트를 직렬화하고 파일에 작성한다.
    /// 
    /// 예외:
    /// - 디스크 부족, 권한 문제 등으로 File.WriteAllText에서 예외가 발생할 수 있습니다.
    /// - 호출자는 필요하면 try/catch로 감싸 예외를 처리하세요.
    /// </summary>
    /// <typeparam name="T">저장할 객체의 타입(클래스)</typeparam>
    /// <param name="list">저장할 리스트</param>
    /// <param name="dirPath">저장 디렉터리 (선택)</param>
    /// <param name="fileName">저장 파일명 (선택)</param>
    public static void SaveList<T>(List<T> list, string? dirPath = null, string? fileName = null) where T : class
    {
        if (list == null) throw new ArgumentNullException(nameof(list));

        // 매개변수 또는 기본값으로 디렉터리/파일 경로 결정
        var dir = dirPath ?? JSON_DIR_PATH;
        var file = fileName ?? JSON_FILE_NAME;
        var fullPath = Path.Combine(dir, file);

        // 디렉터리가 없으면 생성(동시에 다른 스레드/프로세스가 생성할 수 있으니 예외 가능성 고려)
        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }

        // 읽기 쉬운 포맷으로 직렬화 옵션 설정
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        // 직렬화 후 파일에 작성
        // (동일 파일에 대한 동시 쓰기는 파일 손상 유발 가능 — 필요 시 임시 파일 + 교체 전략 권장)
        var json = JsonSerializer.Serialize(list, options);
        File.WriteAllText(fullPath, json);
    }

    /// <summary>
    /// 저장된 JSON 파일을 읽어 동일한 타입의 리스트로 반환합니다.
    /// - dirPath/fileName을 생략하면 기본 경로/파일명을 사용합니다.
    /// - 파일이 존재하지 않거나 내용이 비어 있으면 빈 리스트를 반환합니다.
    /// - JSON의 프로퍼티 이름과 .NET 객체의 프로퍼티 매핑은 대소문자 구분 없이 처리됩니다(PropertyNameCaseInsensitive = true).
    /// 
    /// 반환 시나리오:
    /// - 정상 파싱 성공: 파싱된 List<T> 반환
    /// - 파일 없음 또는 공백: 빈 List<T> 반환
    /// - JSON에 필요한 필드가 누락되거나 타입 불일치가 있을 경우 JsonSerializer는 가능한 값만 채우고 나머지는 기본값으로 둡니다.
    /// 
    /// 예외:
    /// - File.ReadAllText 또는 JsonSerializer.Deserialize에서 예외가 발생할 수 있으므로 호출자의 예외 처리가 필요합니다.
    /// </summary>
    /// <typeparam name="T">로드할 객체의 타입(클래스, parameterless 생성자 필요하지 않음)</typeparam>
    /// <param name="dirPath">로드할 디렉터리 (선택)</param>
    /// <param name="fileName">로드할 파일명 (선택)</param>
    /// <returns>로드된 객체 리스트 또는 빈 리스트</returns>
    public static List<T> LoadList<T>(string? dirPath = null, string? fileName = null) where T : class, new()
    {
        // 디렉터리/파일 경로 결정
        var dir = dirPath ?? JSON_DIR_PATH;
        var file = fileName ?? JSON_FILE_NAME;
        var fullPath = Path.Combine(dir, file);

        // 파일이 없으면 빈 리스트 반환
        if (File.Exists(fullPath) == false)
        {
            return new List<T>();
        }

        // 파일 내용 읽기
        var json = File.ReadAllText(fullPath);
        if (string.IsNullOrWhiteSpace(json))
        {
            // 파일이 비어있거나 공백이면 빈 리스트 반환
            return new List<T>();
        }

        // 대소문자 무시 옵션을 사용해 JSON 프로퍼티와 .NET 프로퍼티 매핑 허용
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // 역직렬화: 형식 불일치 시 일부 필드는 기본값으로 남아 있을 수 있음
        var result = JsonSerializer.Deserialize<List<T>>(json, options);
        return result ?? new List<T>();
    }
}