namespace AoC2024.Days;

public static class Day7
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(7);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //1289579105366
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //
    }

    private static async Task PartOne(string[] input)
    {
        long totalResult = 0;

        foreach (var line in input)
        {
            var operatorResults = new HashSet<long>();
            var splitLine = line.Split(":");
            var desiredResult = long.Parse(splitLine[0]);
            var splitNumbers = splitLine[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numbers = splitNumbers.Select(x => long.Parse(x));

            foreach (var number in numbers)
            {
                var resultsToTry = new HashSet<long>();

                //Need to add the first one manually
                if (!operatorResults.Any()) {
                    operatorResults.Add(number);
                    continue;
                }
                                
                foreach (var result in operatorResults)
                {
                    var multiplyResult = result * number;
                    resultsToTry.Add(multiplyResult);
                    
                    var addResult = result + number;
                    resultsToTry.Add(addResult);
                }
                operatorResults = resultsToTry; //Now run again from the bigger set.
            }
            var match = operatorResults.FirstOrDefault(x => x == desiredResult);
            totalResult += match;
        }
        Console.WriteLine(totalResult);
    }

    private static async Task PartTwo(string[] input)
    {
        long totalResult = 0;

        foreach (var line in input)
        {
            var operatorResults = new HashSet<long>();
            var splitLine = line.Split(":");
            var desiredResult = long.Parse(splitLine[0]);
            var splitNumbers = splitLine[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var numbers = splitNumbers.Select(x => long.Parse(x));

            foreach (var number in numbers)
            {
                var resultsToTry = new HashSet<long>();

                //Need to add the first one manually
                if (!operatorResults.Any())
                {
                    operatorResults.Add(number);
                    continue;
                }

                foreach (var result in operatorResults)
                {
                    var multiplyResult = result * number;
                    resultsToTry.Add(multiplyResult);

                    var addResult = result + number;
                    resultsToTry.Add(addResult);

                    var concatNumberStr = $"{result}{number}";
                    var concatNumber = long.Parse(concatNumberStr);                    
                    resultsToTry.Add(concatNumber);                    
                }
                operatorResults = resultsToTry; //Now run again from the bigger set.
            }
            var match = operatorResults.FirstOrDefault(x => x == desiredResult);
            totalResult += match;
        }
        Console.WriteLine(totalResult);
    }
}
