namespace AoC2024.Days;

public static class Day1
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(1);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //1110981
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //24869388
    }

    private static async Task PartOne(string[] input)
    {        
        var diffs = new List<int>();

        (var leftNumbers, var rightNumbers) = GetLists(input);

        leftNumbers = leftNumbers.OrderBy(x => x).ToList();
        rightNumbers = rightNumbers.OrderBy(x => x).ToList();

        for (int i = 0; i < leftNumbers.Count; i++)
        {
            var diff = Math.Abs(rightNumbers[i] - leftNumbers[i]);
            diffs.Add(diff);
        }        
        Console.WriteLine(diffs.Sum());
        
    }

    private static async Task PartTwo(string[] input)
    {

        var similarities = new List<int>();
        (var leftNumbers, var rightNumbers) = GetLists(input);

        for (int i = 0; i < leftNumbers.Count; i++)
        {
            var leftNumber = leftNumbers[i];
            var rightCount = rightNumbers.Count(x => x == leftNumber);
            var sim = leftNumber * rightCount;
            similarities.Add(sim);
        }
        Console.WriteLine(similarities.Sum());
    }

    private static (List<int> left, List<int> right) GetLists(string[] input)
    {
        var leftNumbers = new List<int>();
        var rightNumbers = new List<int>();

        foreach (var line in input)
        {
            var splitLine = line.Split("   ");
            leftNumbers.Add(int.Parse(splitLine[0]));
            rightNumbers.Add(int.Parse(splitLine[1]));
        }

        return (leftNumbers, rightNumbers);
    }
}
