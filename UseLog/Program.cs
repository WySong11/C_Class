using System;
using System.Diagnostics;
using System.IO;

namespace UseLog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            // 이 코드는 디버그 모드에서만 출력돼요. 릴리즈 모드에서는 무시됩니다.
            Debug.WriteLine("게임 데이터 관리 프로그램 시작");

            // Trace는 디버그뿐 아니라 릴리즈 모드에서도 동작할 수 있어요.
            // 로그 파일로도 출력 가능하죠.
            Trace.WriteLine("트레이스 로그 메시지입니다.");

            // 로그를 파일로 저장하기 위해 TextWriterTraceListener를 사용합니다.
            // 이 코드는 프로그램 실행 디렉토리에 log.txt 파일을 생성합니다.

            // 기존 로그에 추가됨.
            Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.AutoFlush = true;
            Trace.WriteLine("파일로 저장되는 로그입니다. 1");


            var writer = new StreamWriter("log.txt", append: true);
            Trace.Listeners.Add(new TextWriterTraceListener(writer));
            Trace.AutoFlush = true;
            Trace.WriteLine("파일로 저장되는 로그입니다. 2");


            // 기존 로그를 덮어쓰려면 StreamWriter의 append를 false로 설정
            var writer = new StreamWriter("log.txt", append: false);
            Trace.Listeners.Add(new TextWriterTraceListener(writer));
            Trace.AutoFlush = true;
            Trace.WriteLine("파일로 저장되는 로그입니다. 3");
        }
    }
}
