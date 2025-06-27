using System;

public class TimeRecord
{
    public TimeSpan Record { get; set; }
    public string Ranker { get; set; }
    public TimeRecord(TimeSpan record, string ranker)
    {
        Record = record;
        Ranker = ranker;
    }
    public override string ToString()
    {
        return $"{Ranker} : {Record}";
    }
}