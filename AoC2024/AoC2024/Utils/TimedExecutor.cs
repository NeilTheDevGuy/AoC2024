using System.Diagnostics;

namespace AoC2024.Utils;

public class TimedExecutor
{
    public async Task ExecuteTimed(Func<Task> func)
    {
        var sw = new Stopwatch();
        sw.Start();
        await func();
        sw.Stop();
        Console.WriteLine($"Executed in {sw.ElapsedMilliseconds} milliseconds, {sw.ElapsedMilliseconds / 1000} seconds");        
    }
}
