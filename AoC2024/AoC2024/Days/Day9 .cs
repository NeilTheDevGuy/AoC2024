namespace AoC2024.Days;

public static class Day9
{
    public static async Task Run()
    {
        var input = await InputGetter.GetAllAsString(9);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //6401092019345
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //6431472344710
    }

    private static async Task PartOne(string input)
    {
        var map = GetFileSystem(input);

        var left = 0;
        var right = map.Count - 1;
        while (true)
        {
            if (left == right) break;
            
            //Account for uneven numbers of freespace and blocks
            if (map[right] == ".") 
            {
                right--; 
                continue;
            }
            
            if (map[left] != ".") 
            {
                left++; continue;
            }

            //Swap them
            map[left] = map[right];
            map[right] = ".";
            left++;
            right--;
        }
        
        var checkSum = CalculateChecksum(map);
        Console.WriteLine(checkSum);
    }


    private static async Task PartTwo(string input)
    {

    }

    private static List<string> GetFileSystem(string input)
    {
        var isBlock = true;
        var map = new List<string>();
        var id = 0;

        foreach (var digit in input)
        {
            if (isBlock)
            {
                for (int b = 0; b < int.Parse(digit.ToString()); b++)
                {
                    map.Add(id.ToString());
                }

                id++;
                isBlock = false;
            }
            else
            {
                for (int d = 0; d < int.Parse(digit.ToString()); d++)
                {
                    map.Add(".");
                }
                isBlock = true;
            }
        }
        return map;
    }

    private static long CalculateChecksum(List<string> map)
    {
        return map
            .Where(m => m != ".")
            .Select((c, i) => long.Parse(c) * i)
            .Sum();
    }

}
