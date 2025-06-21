using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RecordTime
{    
    private static DateTime StartTime { get; set; }
    private static DateTime EndTime { get; set; }

    private static TimeSpan Duration => EndTime - StartTime;

    public static void Start()
    {
        StartTime = DateTime.Now;
    }

    public static void Stop()
    {
        EndTime = DateTime.Now;
    }

    public static void Reset()
    {
        StartTime = DateTime.MinValue;
        EndTime = DateTime.MinValue;
    }

    public static string PrintTime()
    {
        return $"\n시작 시간 : {StartTime}\n종료 시간 : {EndTime}\n소요 시간 : {Duration}\n";
    }
}
