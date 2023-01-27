namespace LoadGenerator.Services;

public class LoadGenerator
{
    private DateTime _startTime;
    private DateTime _endTime;
    private long _totalLoadGenerated;

    public LoadGenerator()
    {

    }

    public void GenerateLoad(int minutesToRun, int eventsPerSecond)
    {
        Console.WriteLine();
        Console.WriteLine($"Generating load for {minutesToRun} min at {eventsPerSecond} event/s.");
        Console.WriteLine();

        _startTime = DateTime.Now;
        _endTime = _startTime.AddMinutes(minutesToRun);

        var currentBatchStartTime = DateTime.Now;

        while (DateTime.Now < _endTime)
        {
            
            SleepUntilNextSecond(currentBatchStartTime);

            currentBatchStartTime = DateTime.Now;
        }

        OutputSummary();
    }

    private void OutputSummary()
    {
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine($"Generated Load from {_startTime} to {_endTime}");
        Console.WriteLine($"{_totalLoadGenerated} records generated");
        Console.WriteLine();
    }

    private static void SleepUntilNextSecond(DateTime currentSecondStartTime)
    {
        var millisecondsUntilCurrentSecondEnds = 1000 - (DateTime.Now - currentSecondStartTime).TotalMilliseconds;
        if (millisecondsUntilCurrentSecondEnds > 0)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(millisecondsUntilCurrentSecondEnds));
        }
    }
}