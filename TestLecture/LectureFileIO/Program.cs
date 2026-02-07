using System;
using static System.Console;
using System.IO;

public class Program
{
    public static void Main()
    {
        string filePath = "example.txt";

        // 옛날 방식
        //File.WriteAllText(filePath, "Hello, World!\nThis is a sample file.");

        // StreamWriter : 파일에 텍스트를 쓰기 위한 클래스입니다. 파일이 없으면 새로 생성하고, 있으면 덮어씁니다.
        //StreamWriter writer = new StreamWriter(filePath);

        //writer.WriteLine("StreamWriter Example");

        // Close : 스트림을 닫아줍니다. 자원을 해제하는 역할을 합니다.
        //writer.Close();

        // 최신 방식
        // Using : 자동으로 자원을 해제해주는 기능입니다.
        using (StreamWriter writerUsing = new StreamWriter(filePath))
        {
            writerUsing.WriteLine("Hello, World!");
            writerUsing.WriteLine("This is a sample file.");
        }


        string filePath2 = "example2.txt";

        // 파일이 존재하는지 확인
        if (File.Exists(filePath2) == true)
        {
            // Try-Catch : 예외 처리를 위한 구문입니다.
            try
            {
                // Reading from a file
                using (StreamReader reader = new StreamReader(filePath2))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                WriteLine($"Error: {ex.Message}");
            }
        }

        if (File.Exists(filePath))
        {
            // Reading from a file
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    WriteLine(line);
                }
            }
        }

        string dirPath = "SampleDir";

        // Directory.Exists : 디렉토리가 존재하는지 확인하는 메서드입니다.
        if (Directory.Exists(dirPath) == false)
        {
            // Directory.CreateDirectory : 디렉토리를 생성하는 메서드입니다.
            Directory.CreateDirectory(dirPath);
        }

        // Path Combination
        // Combine : 경로를 결합하는 메서드입니다.
        string movedFilePath = Path.Combine(dirPath, "moved_example.txt");

        using (StreamWriter writerMoved = new StreamWriter(movedFilePath))
        {
            writerMoved.WriteLine("This file has been moved to SampleDir.");
        }

        if (Directory.Exists(dirPath) && File.Exists(movedFilePath))
        {
            using (StreamReader readerMoved = new StreamReader(movedFilePath))
            {
                string line;
                while ((line = readerMoved.ReadLine()) != null)
                {
                    WriteLine(line);
                }
            }
        }

        /////////////////////////////////////////////////////////////
        /// Binary
        /// 

        // FileStream : 파일을 바이트 단위로 읽고 쓰기 위한 클래스입니다. Binary 파일 작업에 주로 사용됩니다.
        // FileMode : 파일을 여는 방식을 지정하는 열거형입니다.
        // FileMode.Create : 파일이 존재하지 않으면 새로 생성하고, 존재하면 덮어씁니다.
        // FileAccess : 파일에 대한 접근 권한을 지정하는 열거형입니다.
        // FileAccess.Write : 파일에 쓰기 권한을 부여합니다.
        using (FileStream fs = new FileStream("binaryfile.bin", FileMode.Create, FileAccess.Write))
        {
            byte[] data = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            fs.Write(data, 0, data.Length);
        }

        // BinaryWriter : 이진 파일에 데이터를 쓰기 위한 클래스입니다.
        // FileMode.Create : 파일이 존재하지 않으면 새로 생성하고, 존재하면 덮어씁니다.
        using (BinaryWriter writer = new BinaryWriter(File.Open("binaryfile2.bin", FileMode.Create)))
        {
            writer.Write(123);          // int
            writer.Write(45.67);       // double
            writer.Write("Hello");     // string
        }

        // BinaryReader : 이진 파일에서 데이터를 읽기 위한 클래스입니다.
        // FileMode.Open : 기존 파일을 엽니다.
        using (BinaryReader reader = new BinaryReader(File.Open("binaryfile2.bin", FileMode.Open)))
        {
            int intValue = reader.ReadInt32();
            double doubleValue = reader.ReadDouble();
            string stringValue = reader.ReadString();
            WriteLine($"Int: {intValue}, Double: {doubleValue}, String: {stringValue}");
        }

        try
        {
            using (BinaryReader reader = new BinaryReader(File.Open("binaryfile.bin", FileMode.Open)))
            {
                int intValue = reader.ReadInt32();
                double doubleValue = reader.ReadDouble();
                string stringValue = reader.ReadString();
                WriteLine($"Int: {intValue}, Double: {doubleValue}, String: {stringValue}");
            }
        }
        // 예외 처리
        // EndOfStreamException : 스트림의 끝에 도달했을 때 발생하는 예외입니다.
        // 이 예외는 파일에서 더 이상 읽을 데이터가 없을 때 발생합니다.
        catch (EndOfStreamException ex)
        {
            WriteLine($"Error: {ex.Message}");
        }

        ///////////////////////////////////////////////////////
        // Append

        // File.AppendAllText : 파일의 끝에 텍스트를 추가하는 메서드입니다.
        // 파일이 없으면 새로 생성합니다.
        File.AppendAllLines(filePath, new string[] { "First appended line.", "Second appended line." });

        // StreamWriter with append option
        // append: true 옵션을 사용하여 파일의 끝에 텍스트를 추가합니다.
        using (StreamWriter writerAppend = new StreamWriter(filePath, append: true))
        {
            writerAppend.WriteLine("This line is appended.");
        }

        /////////////////////////////////////////////////////////////
        ///

        // 인스턴스 생성       
        FileReadWrite fileReadWrite = new FileReadWrite();

        // 콘솔 화면 지우기
        Clear();

        // StartFileReadWrite 함수 호출 : 파일 읽기/쓰기 시작
        fileReadWrite.StartFileReadWrite();

    }
}