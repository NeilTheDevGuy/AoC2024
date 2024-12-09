using System.Data;

namespace AoC2024.Days;

public static class Day5
{    
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(5);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //5509
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //
    }

    private static async Task PartOne(string[] input)
    {
        (var correctOrderUpdates, var incorrectOrderUpdates) = GetStuff(input);

        var midSum = 0;        
        foreach (var correctOrder in correctOrderUpdates)
        {
            int mid = correctOrder[correctOrder.Count / 2];
            midSum += mid;
        }

        Console.WriteLine(midSum);
    }

    private static async Task PartTwo(string[] input)
    {
        (var rules, var pageUpdateNumbers) = GetRulesAndPages(input);
        (var correctOrderUpdates, var incorrectOrderUpdates) = GetStuff(input);

        var corrected = new List<List<int>>();

        Console.WriteLine($"{incorrectOrderUpdates.Count} incorrect");

        foreach (var incorrect in incorrectOrderUpdates)
        {
            Console.WriteLine($"Trying to fix {string.Join(",", incorrect)}");            
            while (true)
            {
                var isValid = true;
                ShuffleList(incorrect); //This is crazy long to run. Need to sort it instead!


                if (string.Join(",", incorrect) == "97,75,47,61,53")
                {
                    var a = 0;
                }
                foreach (var number in incorrect)
                {   
                    var idx = incorrect.IndexOf(number);

                    var matchingRules1 = rules.Where(p => p.Page1 == number);
                    foreach (var rule in matchingRules1)
                    {
                        var idx2 = incorrect.IndexOf(rule.Page2);
                        if (idx2 > -1 && idx2 < idx)
                        {
                            isValid = false;
                            break;

                        }
                    }

                    var matchingRules2 = rules.Where(p => p.Page2 == number);
                    foreach (var rule in matchingRules2)
                    {
                        var idx2 = incorrect.IndexOf(rule.Page1);
                        if (idx2 > -1 && idx2 > idx)
                        {
                            isValid = false;
                            break;
                        }
                    }
                }
                if (isValid)
                {
                    Console.WriteLine("Fixed!");
                    corrected.Add(incorrect);
                    break;
                }                
            }
        }

        var midSum = 0;
        foreach (var correctOrder in corrected)
        {
            int mid = correctOrder[correctOrder.Count / 2];
            midSum += mid;
        }

        Console.WriteLine(midSum);
    }

    private static (List<Order> rules, List<List<int>> pageUpdateNumbers) GetRulesAndPages(string[] input)
    {
        {
            var rules = new List<Order>();
            var pageUpdateNumbers = new List<List<int>>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i].Contains("|"))
                {
                    var pair = input[i].Split("|");
                    rules.Add(new Order(int.Parse(pair[0]), int.Parse(pair[1])));
                }
                else if (!string.IsNullOrEmpty(input[i]))
                {
                    pageUpdateNumbers.Add(input[i].Split(",").Select(x => int.Parse(x)).ToList());
                }
            }
            return (rules, pageUpdateNumbers);
        }
    }

    private static (List<List<int>> correct, List<List<int>> incorrect) GetStuff(string[] input)
    {
        (var rules, var pageUpdateNumbers) = GetRulesAndPages(input);
        var correctOrderUpdates = new List<List<int>>();
        var inCorrectOrderUpdates = new List<List<int>>();
        foreach (var page in pageUpdateNumbers)
        {
            var isValid = true;
            foreach (var number in page)
            {
                var idx = page.IndexOf(number);

                var matchingRules1 = rules.Where(p => p.Page1 == number);
                foreach (var rule in matchingRules1)
                {
                    var idx2 = page.IndexOf(rule.Page2);
                    if (idx2 > -1 && idx2 < idx)
                    {
                        isValid = false;
                        break;

                    }
                }

                var matchingRules2 = rules.Where(p => p.Page2 == number);
                foreach (var rule in matchingRules2)
                {
                    var idx2 = page.IndexOf(rule.Page1);
                    if (idx2 > -1 && idx2 > idx)
                    {
                        isValid = false;
                        break;

                    }
                }
            }
            if (isValid)
            {                
                correctOrderUpdates.Add(page);
            }
            else
            {                
                inCorrectOrderUpdates.Add(page);
            }
        }

        return (correctOrderUpdates, inCorrectOrderUpdates);
    }

    private static void ShuffleList(List<int> list)
    {
        Random random = new Random();
        
        var cnt = list.Count;

        for (int i = list.Count - 1; i > 1; i--)
        {
            var rnd = random.Next(i + 1);

            var value = list[rnd];
            list[rnd] = list[i];
            list[i] = value;
        }
    }

    private record Order(int Page1, int Page2);
}
