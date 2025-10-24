using System;

// BaseClass 라는 이름의 클래스를 정의합니다.
// 클래스는 객체 지향 프로그래밍의 기본 단위입니다.
// 클래스는 속성(데이터)과 메서드(기능)를 포함할 수 있습니다.
// Public 접근 제한자는 이 클래스가 다른 클래스에서 접근 가능함을 의미합니다.
public class BaseClass
{
    // WriteSpace 라는 이름의 메서드를 정의합니다.
    // 이 메서드는 정수형 매개변수 inCount 를 받습니다.
    // inCount 숫자 만큼 공백 문자를 출력합니다.
    public void WriteSpace(int inCount)
	{
		Console.Write(new string(' ', inCount));
    }

    /// <summary>
    /// WriteStar 라는 이름의 메서드를 정의합니다.
    /// inCount 숫자 만큼 '*' 문자를 출력합니다.
    /// </summary>
    /// <param name="inCount"></param>
	public void WriteStar(int inCount)
	{
		Console.Write(new string('*', inCount));
    }
}
