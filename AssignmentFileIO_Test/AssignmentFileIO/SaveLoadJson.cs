using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class SaveLoadJson
{
    public static string JSON_DIR_PATH = "Save";
    public static string JSON_FILE_NAME = "playerdata.json";

    // 특정 클래스 T로 만들어진 리스트를 JSON으로 저장합니다.
    public static void SaveList(List<PlayerData> list, string? dirPath = null, string? fileName = null)
    {
        var dir = dirPath ?? JSON_DIR_PATH;
        var file = fileName ?? JSON_FILE_NAME;
        var fullPath = Path.Combine(dir, file);

        // /Save/playerdata.json 파일에 저장

        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }

        // 리스트를 JSON 문자열로 직렬화하여 파일에 기록
        var jsonString = JsonSerializer.Serialize(list);

        // 파일에 JSON 문자열 쓰기
        File.WriteAllText(fullPath, jsonString);
    }

    // 저장된 JSON 파일을 읽어 동일한 클래스 T 타입의 리스트로 반환합니다.
    // 파일이 없거나 직렬화된 내용이 비어있으면 빈 리스트를 반환합니다.
    public static List<PlayerData> LoadList(string? dirPath = null, string? fileName = null)
    {
        var dir = dirPath ?? JSON_DIR_PATH;
        var file = fileName ?? JSON_FILE_NAME;
        var fullPath = Path.Combine(dir, file);

        // /Save/playerdata.json 파일이 없으면 빈 리스트 반환

        // 파일이 없으면 빈 리스트 반환
        if (File.Exists(fullPath) == false)
        {
            return new List<PlayerData>();
        }

        // 파일에서 JSON 문자열 읽기
        var jsonString = File.ReadAllText(fullPath);

        // 대소문자 구분 없이 속성 이름을 반영하는 옵션 설정
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // JSON 문자열을 리스트로 역직렬화하여 반환
        var result = JsonSerializer.Deserialize<List<PlayerData>>(jsonString, options);

        // ?? 파일이 비어있거나 역직렬화에 실패하면 빈 리스트 반환
        return result ?? new List<PlayerData>();
    }
}