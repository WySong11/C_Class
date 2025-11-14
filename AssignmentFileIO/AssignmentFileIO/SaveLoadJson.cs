using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public static class SaveLoadJson
{
    public static string JSON_DIR_PATH = "Save";
    public static string JSON_FILE_NAME = "playerdata.json";

    // 특정 클래스 T로 만들어진 리스트를 JSON으로 저장합니다.
    public static void SaveList<T>(List<T> list, string? dirPath = null, string? fileName = null) where T : class
    {
        var dir = dirPath ?? JSON_DIR_PATH;
        var file = fileName ?? JSON_FILE_NAME;
        var fullPath = Path.Combine(dir, file);

        if (Directory.Exists(dir) == false)
        {
            Directory.CreateDirectory(dir);
        }

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(list, options);
        File.WriteAllText(fullPath, json);
    }

    // 저장된 JSON 파일을 읽어 동일한 클래스 T 타입의 리스트로 반환합니다.
    // 파일이 없거나 직렬화된 내용이 비어있으면 빈 리스트를 반환합니다.
    public static List<T> LoadList<T>(string? dirPath = null, string? fileName = null) where T : class, new()
    {
        var dir = dirPath ?? JSON_DIR_PATH;
        var file = fileName ?? JSON_FILE_NAME;
        var fullPath = Path.Combine(dir, file);

        if (File.Exists(fullPath) == false)
        {
            return new List<T>();
        }

        var json = File.ReadAllText(fullPath);
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<T>();
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var result = JsonSerializer.Deserialize<List<T>>(json, options);
        return result ?? new List<T>();
    }
}