using DrawDiamond;
using System;
using System.Diagnostics;

/*
 * DLL(Dynamic Link Library)
실행 가능한 코드와 리소스를 담고 있는 공유 라이브러리 파일

- 확장자: .dll
- 용도: 여러 프로그램에서 공통으로 사용하는 함수, 클래스, 리소스를 모아둔 파일
- 특징:
-실행 파일이 아님(.exe와 달리 직접 실행 불가)
-다른 프로그램에서 참조하여 사용
- 코드 재사용성과 유지보수성 향상
- 예시: System.Text.Json.dll, MyLibrary.dll

 * PDB(Program Database)
디버깅 정보를 담고 있는 파일

- 확장자: .pdb
- 용도: 디버깅 시 소스 코드와 실행 파일을 연결해주는 역할
- 포함 정보:
-변수 이름, 함수 이름, 줄 번호
- 중단점 설정 위치
- 예외 발생 시 스택 트레이스 정보
- 특징:
-디버깅할 때만 필요(배포 시에는 보통 제외)
- .dll 또는.exe와 정확히 일치해야 디버깅 가능

- Visual Studio에서 Debug 모드로 빌드하면 PDB가 생성되고, Release 모드에선 생략하거나 최소화돼요.
- .pdb 없이도 프로그램은 실행되지만, 예외 발생 시 줄 번호나 변수명이 안 나와서 디버깅이 어려워요.
*/

// DLL을 사용하는 방법
// 1. DLL 프로젝트를 참조 추가
// 2. 네임스페이스를 using으로 가져오기
// 3. DLL 내의 클래스나 메서드를 호출
// 4. 빌드 후 실행

// DrawDiamond.dll을 사용하여 다이아몬드 모양을 그리는 프로그램


/*
 * Debug 모드
개발 중 디버깅을 쉽게 하기 위한 모드

- 디버깅 정보 포함: .pdb 파일 생성으로 중단점, 변수 추적 가능
- 코드 최적화 없음: 소스 코드와 실행 코드가 최대한 일치
- 속도 느림: 디버깅을 위한 부가 정보와 검사 코드가 포함됨
- 메모리 사용량 많음: 안정성을 위해 변수 초기화 등 추가 처리
#if DEBUG
    Console.WriteLine("디버그 모드입니다.");
#endif



* Release 모드
최종 사용자에게 배포할 때 사용하는 모드

- 디버깅 정보 없음: 실행 파일이 더 작고 빠름
- 코드 최적화 적용: 불필요한 코드 제거, 성능 향상
- 중단점 설정 불가: 디버깅이 어려움
- 실행 속도 빠름: 최적화된 코드로 컴파일됨
*/


namespace DiamondUseDLL
{
    public class Program
    {
        static void Main(string[] args)
        {
            DiamondDrawer.DrawDiamond();
        }
    }
}