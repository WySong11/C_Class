using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RecordTime
{
    public delegate void StartTimeDelegate(DateTime starttime);
    public delegate void EndTimeDelegate(DateTime endtime);

    private static event StartTimeDelegate? StartTimeEvent;
    private static event EndTimeDelegate? EndTimeEvent;

    public static bool IsRunning => StartTime != DateTime.MinValue && EndTime == DateTime.MinValue;
    public static bool IsStopped => StartTime != DateTime.MinValue && EndTime != DateTime.MinValue;
    public static bool IsReset => StartTime == DateTime.MinValue && EndTime == DateTime.MinValue;

    private static DateTime StartTime { get; set; }
    private static DateTime EndTime { get; set; }

    private static TimeSpan Duration => EndTime - StartTime;

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

    public static void SetStartTimeDelegate(StartTimeDelegate startTimeDelegate)
    {
        StartTimeEvent += startTimeDelegate;
    }

    public static void SetEndTimeDelegate(EndTimeDelegate endTimeDelegate)
    {
        EndTimeEvent += endTimeDelegate;
    }

    public static string PrintTime()
    {
        return $"\n시작 시간 : {StartTime}\n종료 시간 : {EndTime}\n소요 시간 : {Duration}\n";
    }
}
