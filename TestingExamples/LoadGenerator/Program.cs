const int defaultMinutesToRun = 1;
const int defaultEventsPerSecond = 10;

var minutesToRun = args.Length > 1 ? int.Parse(args[1]) : defaultMinutesToRun;
minutesToRun = minutesToRun > 60 ? 60 : minutesToRun;

var eventsPerSecond = args.Length > 2 ? int.Parse(args[2]) : defaultEventsPerSecond;

var loadGenerator = new LoadGenerator.Services.LoadGenerator();
loadGenerator.GenerateLoad(minutesToRun, eventsPerSecond);