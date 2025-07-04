using System;

public class TimeRecord
{
    public string? PlayerName { get; set; }
    public int PlayCount { get; set; }
    public TimeSpan PlayTime { get; set; }

    // System.Text.Json은 생성자 기반 역직렬화를 시도할 때, 생성자의 매개변수 이름이 속성 이름과 정확히 일치해야 하고,
    // 기본적으로 **필드가 아닌 속성(property)**에만 매핑한다는 것입니다.
    // 기본 생성자(default constructor) 를 제공해서 System.Text.Json이 먼저 객체를 만들고, 이후 프로퍼티에 값을 할당하도록 합니다.
    public TimeRecord() { }

    public TimeRecord(int InPlayCount, TimeSpan InPlayTime, string? InPlayerName)
    {
        PlayerName = InPlayerName;
        PlayCount = InPlayCount;
        PlayTime = InPlayTime;
    }
}