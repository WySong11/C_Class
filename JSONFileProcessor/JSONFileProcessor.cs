using System;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JSONFileManager
{
    // sealed : 해당 클래스는 단 하나의 인스턴스만 존재할 수 있도록 보장합니다.
    // 해당 클래스를 상속 받을 수 없으며, 이는 싱글턴 패턴을 구현할 때 유용합니다.

    public sealed class JSONFileProcessor
    {
        // readonly : 해당 필드는 초기화 이후 변경할 수 없으며, 이는 멀티스레드 환경에서 안전하게 사용할 수 있도록 보장합니다.
        // Lazy<T> : 인스턴스가 실제로 필요할 때까지 생성하지 않습니다. 이는 성능 최적화에 유용합니다.

        private static readonly Lazy<JSONFileProcessor> _instance = new(() => new JSONFileProcessor());

        // 싱글턴 인스턴스에 접근하기 위한 프로퍼티
        // _instance.Value에서 Value는 Lazy<T> 클래스의 속성입니다.
        // Lazy<T>의 Value 속성은 객체를 안전하게, 필요할 때 한 번만 생성하도록 보장합니다.

        public static JSONFileProcessor Instance => _instance.Value;

        private JSONFileProcessor() { }

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

            // 스트림의 끝까지 모든 텍스트를 한 번에 읽어 문자열로 반환합니다.
            // 파일 전체를 한 번에 처리할 때 편리하지만,
            // 파일이 너무 크면 OutOfMemory 예외가 발생할 수 있으니 주의해야 합니다.
            //string json = reader.ReadToEnd();

            // StringBuilder를 사용하여 파일의 모든 줄을 읽어 문자열로 만듭니다.
            // 이 방법은 메모리 사용을 줄이고, 파일이 크더라도 안전하게 처리할 수 있습니다.

            var sb = new StringBuilder();
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                sb.AppendLine(line);
            }
            string json = sb.ToString();

            return JsonSerializer.Deserialize<T>(json, options);
        }

        // 제네릭 타입으로 JSON 파일 쓰기
        // Write<T>: 제네릭 타입으로 객체를 JSON 파일에 저장합니다.
        public void Write<T>(string filePath, T data, JsonSerializerOptions? options = null, Encoding? encoding = null)
        {
            // 디렉토리가 존재하지 않으면 생성합니다.
            string? dirPath = Path.GetDirectoryName(filePath);
            if (dirPath != null && !Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            encoding ??= Encoding.UTF8;
            string json = JsonSerializer.Serialize(data, options);
            using var writer = new StreamWriter(filePath, false, encoding);
            writer.Write(json);
        }
    }
}