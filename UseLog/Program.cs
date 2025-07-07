using System;
using System.Diagnostics;

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
            Trace.Listeners.Add(new TextWriterTraceListener("log.txt"));
            Trace.AutoFlush = true;
            Trace.WriteLine("파일로 저장되는 로그입니다.1");


            /*| 로그 레벨 | 설명 | 사용 예시 |
            | Trace | 가장 상세한 로그. 디버깅용 | 함수 진입 / 종료, 루프 내부 상태 등 |
            | Debug | 개발 중 필요한 정보 | 변수 값, 조건 분기 등 |
            | Information | 일반적인 실행 흐름 | 사용자 로그인, 처리 완료 등 |
            | Warning | 문제는 아니지만 주의 필요 | API 응답 지연, 설정 누락 등 |
            | Error | 오류 발생 | 예외 처리, 실패한 작업 등 |
            | Critical | 시스템 중단급 오류 | DB 연결 실패, 서비스 다운 등 |*/







            /*        ILogger logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger("MyApp");

                    logger.LogTrace("Trace 로그입니다.");
                    logger.LogDebug("Debug 로그입니다.");
                    logger.LogInformation("Information 로그입니다.");
                    logger.LogWarning("Warning 로그입니다.");
                    logger.LogError("Error 로그입니다.");
                    logger.LogCritical("Critical 로그입니다.");*/
        }
    }
}
