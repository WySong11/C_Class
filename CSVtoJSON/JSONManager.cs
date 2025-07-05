using System;
using System.IO;
using System.Text;
using System.Text.Json;

public class JSONManager
{
    private static readonly Lazy<JSONManager> _instance = new(() => new JSONManager());

    public static JSONManager Instance => _instance.Value;

    private JSONManager() { }

    // 제네릭 타입으로 JSON 파일 읽기
    // Read<T>: 제네릭 타입으로 JSON 파일을 읽어 객체로 반환합니다.
    public T? Read<T>(string filePath, JsonSerializerOptions? options = null, Encoding? encoding = null)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"파일이 존재하지 않습니다: {filePath}");
            return default;
        }

        encoding ??= Encoding.UTF8;
        using var reader = new StreamReader(filePath, encoding);
        string json = reader.ReadToEnd();
        return JsonSerializer.Deserialize<T>(json, options);
    }

    // 제네릭 타입으로 JSON 파일 쓰기
    // Write<T>: 제네릭 타입으로 객체를 JSON 파일에 저장합니다.
    public void Write<T>(string filePath, T data, JsonSerializerOptions? options = null, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        string json = JsonSerializer.Serialize(data, options);
        using var writer = new StreamWriter(filePath, false, encoding);
        writer.Write(json);
    }
}
