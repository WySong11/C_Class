using System;

// 모든 함수가 변수가 static으로 선언된 Static 클래스를 사용하여
// 시간 기록을 관리하는 TimeRecorder 클래스를 정의합니다.
public static class TimeRecorder
{
    // 델리게이트 정의
    public delegate void StartTimeDelegate(DateTime starttime);
    public delegate void EndTimeDelegate(DateTime endtime);

    // 이벤트 정의
    private static event StartTimeDelegate? StartTimeEvent;
    private static event EndTimeDelegate? EndTimeEvent;

    // 상태 확인을 위한 프로퍼티
    // 표현식 바디를 사용하여 간결하게 작성
    public static bool IsRunning => StartTime != DateTime.MinValue && EndTime == DateTime.MinValue;
    public static bool IsStopped => StartTime != DateTime.MinValue && EndTime != DateTime.MinValue;
    public static bool IsReset => StartTime == DateTime.MinValue && EndTime == DateTime.MinValue;

    // 프로퍼티를 통해 시작 시간과 종료 시간을 가져올 수 있도록 설정    
    private static DateTime StartTime { get; set; } = DateTime.MinValue;
    private static DateTime EndTime { get; set; } = DateTime.MinValue;

    // Public 프로퍼티로 현재 시간을 가져올 수 있도록 설정
    // get;만 public으로 설정하고,
    // private set을 사용하여 외부에서 직접 설정할 수 없도록 함
    public static DateTime CurrentTime
    {
        get;
        private set;
    }

    private static DateTime TestTime 
    {
        get
        {
            return TestTime;
        }
        set
        {
            TestTime = value;
        }
    }

    // 시작 시간을 문자열로 표현하는 프로퍼티
    public static string StartTimeString
    {
        get
        {
            return StartTime == DateTime.MinValue ? "시작 시간이 설정되지 않았습니다." : StartTime.ToString("F");
        }
    }

    // 종료 시간을 문자열로 표현하는 프로퍼티
    public static string EndTimeString
    {
        get
        {
            return EndTime == DateTime.MinValue ? "종료 시간이 설정되지 않았습니다." : EndTime.ToString("F");
        }
    }

    // 소요 시간을 문자열로 표현하는 프로퍼티
    public static string DurationTimeString
    {
        get
        {
            if( Duration.TotalHours >= 1 )
            {
                return $"{(int)Duration.TotalHours}시간 {(int)Duration.Minutes}분 {(int)Duration.Seconds}초";
            }
            else if( Duration.TotalMinutes >= 1 )
            {
                return $"{(int)Duration.TotalMinutes}분 {(int)Duration.Seconds}초";
            }
            else
            {
                return $"{(int)Duration.TotalSeconds}초";
            }
        }
    }


    // 소요 시간 계산을 위한 프로퍼티
    private static TimeSpan Duration => EndTime - StartTime;

    public static TimeSpan GetDuration()
    {
        if (IsRunning)
        {
            return DateTime.Now - StartTime;
        }
        else if (IsStopped)
        {
            return Duration;
        }
        else
        {
            return TimeSpan.Zero; // 초기화 상태에서는 0초 반환
        }
    }

    // Static이 아니라 사용할 수 없는 변수
    //public bool TestTimeSpan;

    public static void Start()
    {
        StartTime = DateTime.Now;
        StartTimeEvent?.Invoke(StartTime);
    }

    public static void Stop()
    {
        EndTime = DateTime.Now;
        EndTimeEvent?.Invoke(EndTime);
    }

    public static void Reset()
    {
        StartTime = DateTime.MinValue;
        EndTime = DateTime.MinValue;
    }

    public static void Clear()
    {
        Reset();
        StartTimeEvent = null;
        EndTimeEvent = null;
    }

    // 시작 시간과 종료 시간을 설정하는 델리게이트를 외부에서 설정할 수 있도록 하는 메서드
    public static void AddStartTimeDelegate(StartTimeDelegate startTimeDelegate)
    {
        StartTimeEvent += startTimeDelegate;
    }

    public static void AddEndTimeDelegate(EndTimeDelegate endTimeDelegate)
    {
        EndTimeEvent += endTimeDelegate;
    }

    public static void RemoveStartTimeDelegate(StartTimeDelegate startTimeDelegate)
    {
        StartTimeEvent -= startTimeDelegate;
    }

    public static void RemoveEndTimeDelegate(EndTimeDelegate endTimeDelegate)
    {
        EndTimeEvent -= endTimeDelegate;
    }

    // 모든 델리게이트를 제거하는 메서드
    public static void RemoveAllDelegates()
    {
        StartTimeEvent = null;
        EndTimeEvent = null;
    }

    public static string PrintTime()
    {
        return $"\n시작 시간 : {StartTimeString}\n종료 시간 : {EndTimeString}\n소요 시간 : {DurationTimeString}\n";
    }

    // Static 메서드가 아니라서 인스턴스 메서드는 사용할 수 없다.
    //public void SetCurrentTime(DateTime time)
    //{
    //    CurrentTime = time;
    //}
}