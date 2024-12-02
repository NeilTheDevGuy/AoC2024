namespace AoC2024.Days;

public static class Day2
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(2);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //463
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //
    }

    private static async Task PartOne(string[] input)
    {
        var safeLevels = 0;
        foreach (var line in input)
        {   
            var levels = line.Split(' ').Select(l => int.Parse(l)).ToArray();
            var isSafeLevel = IsSafeLevel(levels);
            if (isSafeLevel) safeLevels++;
        }
        Console.WriteLine(safeLevels);
    }

    private static async Task PartTwo(string[] input)
    {
        var safeLevels = 0;
        foreach (var line in input)
        {            
            var levels = line.Split(' ').Select(l => int.Parse(l)).ToArray();
            var isSafeLevel = IsSafeLevel(levels);
            if (!isSafeLevel)
            {
                //Loop through the whole level again multiple times, but omit one number each time.
                //If it's ever safe, then add to the count and break
                for (int i = 0; i < levels.Length; i++) 
                {
                    var restrictedLevels = levels.ToList();
                    restrictedLevels.RemoveAt(i);
                    var isNowSafe = IsSafeLevel(restrictedLevels.ToArray());
                    if (isNowSafe)
                    {
                        isSafeLevel = true;
                        break;
                    }
                }            
            }
            if (isSafeLevel) 
            {   
                safeLevels++;
            };
        }
        Console.WriteLine(safeLevels);
    }

    private static bool IsSafeLevel(int[] levels)
    {
        var isSafeLevel = true;
        var isIncrease = levels[0] - levels[1] > 0;
        for (int i = 0; i < levels.Length - 1; i++)
        {
            var diff = levels[i] - levels[i + 1];
            if (
                (diff == 0 || diff > 3 || diff < -3) ||
                (isIncrease && diff < 0) ||
                (!isIncrease && diff > 0)
                )
            {
                isSafeLevel = false;
                break;
            }
        }
        return isSafeLevel;
    }
}
