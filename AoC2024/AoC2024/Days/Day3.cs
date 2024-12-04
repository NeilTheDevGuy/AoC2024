using System.Text.RegularExpressions;

namespace AoC2024.Days;

public static class Day3
{    
    public static async Task Run()
    {
        var input = await InputGetter.GetAllAsString(3);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //159833790
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //89349241
    }

    private static async Task PartOne(string input)
    {
        var sums = new List<long>();
        var regex = new Regex(@"mul\((\d+),(\d+)\)");
        var result = regex.Match(input);
        while(true)
        {   
            var numbersString = result.Value.Replace("mul(", "").Replace(")", "");
            var numbers = numbersString.Split(",");
            var left = long.Parse(numbers[0]);
            var right = long.Parse(numbers[1]);
            long sumResult = left * right;
            sums.Add(sumResult);            
            result = result.NextMatch();
            if (!result.Success) break;
        }
        
        var finalResult = sums.Sum();
        Console.WriteLine(finalResult);
    }

    private static async Task PartTwo(string input)
    {
        {
            var sums = new List<long>();
            var regex = new Regex(@"mul\((\d+),(\d+)\)|don't\(\)|do\(\)");
            var result = regex.Match(input);
            var process = true;
            while (true)
            {
                if (result.Value.Contains("do("))
                {
                    process = true;
                    result = result.NextMatch();
                    continue;
                }
                if (result.Value.Contains("don't"))
                {
                    process = false;
                    result = result.NextMatch();
                    continue;
                }
                if (process)
                {
                    var numbersString = result.Value.Replace("mul(", "").Replace(")", "");
                    var numbers = numbersString.Split(",");
                    var left = long.Parse(numbers[0]);
                    var right = long.Parse(numbers[1]);
                    long sumResult = left * right;
                    sums.Add(sumResult);
                }
                result = result.NextMatch();
                if (!result.Success) break;
            }

            var finalResult = sums.Sum();
            Console.WriteLine(finalResult);
        }
    }
}
