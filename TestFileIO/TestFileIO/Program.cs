using System;
using System.IO;

namespace TestFileIO
{
    public class Program
    {
        static void Main(string[] args)
        {
            ///////////////////////////////////////////////////////////////
            // 파일 생성. 한 줄 쓰기.
            File.WriteAllText("testfile1.txt", "Hello, World!");

            // using : 파일을 열고 자동으로 닫기
            using (StreamWriter writer1 = new StreamWriter("testfile2.txt"))
            {
                writer1.WriteLine("Hello, World 1!");
                writer1.WriteLine("Hello, World 2!");
                writer1.WriteLine("Hello, World 3!");
            }

            // using 사용 안함. 수동으로 닫기
            StreamWriter writer2 = new StreamWriter("testfile3.txt");

            writer2.WriteLine("Hello, C# 1!");
            writer2.WriteLine("Hello, C# 2!");
            writer2.WriteLine("Hello, C# 3!");
            writer2.WriteLine("Hello, C# 4!");

            writer2.Close();

            ///////////////////////////////////////////////////////////////
            // 파일 읽기

            string content = File.ReadAllText("testfile2.txt");
            Console.WriteLine(content);

            // 한 줄씩 읽기
            using (StreamReader reader = new StreamReader("testfile3.txt"))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }

            ///////////////////////////////////////////////////////////////
            // 파일 존재 여부 확인 후 읽기

            // 존재하지 않는 파일 읽기 시도
            if (File.Exists("testfile5.txt"))
            {
                try
                {
                    string text = File.ReadAllText("testfile5.txt");
                    Console.WriteLine("File content: " + text);
                }
                // IOException 처리 : 파일 읽기 중 발생하는 일반적인 예외 처리
                catch (IOException ex)
                {
                    Console.WriteLine("An error occurred while reading the file: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("File 'testfile5.txt' does not exist.");
            }

            ///////////////////////////////////////////////////////////////
            // 바이너리 파일 쓰기 및 읽기

            // FileStream : 바이너리 파일 쓰기
            // FileMode.Create : 새 파일 생성
            // FileAccess.Write : 쓰기 전용
            using (FileStream fs = new FileStream("testfile6.bin", FileMode.Create, FileAccess.Write))
            {
                byte[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                fs.Write(data, 0, data.Length);
            }

            using (BinaryWriter writer = new BinaryWriter(File.Open("testfile7.bin", FileMode.Create)))
            {
                writer.Write(123);          // int
                writer.Write(45.67);       // double
                writer.Write("Hello");     // string
            }

            using (BinaryReader reader = new BinaryReader(File.Open("testfile7.bin", FileMode.Open)))
            {
                int intValue = reader.ReadInt32();
                double doubleValue = reader.ReadDouble();
                string stringValue = reader.ReadString();

                Console.WriteLine($"Read from binary file: int={intValue}, double={doubleValue}, string={stringValue}");
            }

            //////////////////////////////////////////////////////////////
            // 디렉토리 작업

            string dirPath = "Save";

            // 디렉토리 존재 여부 확인 후 생성
            if (Directory.Exists(dirPath) == false)
            {
                Directory.CreateDirectory(dirPath);
            }

            // Path.Combine : 디렉토리 경로와 파일명을 결합
            string filePath = Path.Combine(dirPath, "testfile2.txt");

            using (StreamWriter writer1 = new StreamWriter(filePath))
            {
                writer1.WriteLine("World 1!");
                writer1.WriteLine("World 2!");
                writer1.WriteLine("World 3!");
            }

            // 디렉토리와 파일 존재 여부 확인 후 읽기
            if (Directory.Exists(dirPath))
            {
                // 결합된 전체 파일 경로
                if (File.Exists(filePath))
                {
                    // 한 줄씩 읽기
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        string? line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line);
                        }
                    }
                }
            }

            ///////////////////////////////////////////////////////////////
            // AppendAllText : 파일 끝에 내용 추가
            File.AppendAllText("testfile1.txt", "\nAppended Line 1");


            //////////////////////////////////////////////////////////////

            FileReadWrite fileReadWrite = new FileReadWrite();

            fileReadWrite.StartFileReadWrite();

        }
    }
}