using System.Diagnostics;

namespace AoC2024.Days;

public static class Day6
{
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(6);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //5212
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //
    }

    private static async Task PartOne(string[] input)
    {
        var maxX = input[0].Length -1;
        var maxY = input.Length -1;

        var dirs = new List<char> { '^', '>', 'v', '<' };

        //Find guard
        var guardX = 0;
        var guardY = 0;
        var guardDir = '^';
        for (int x = 0; x < maxX; x++)
        {
            for (int y = 0; y < maxY; y++)
            {
                if (dirs.Contains(input[y][x]))
                {
                    guardX = x;
                    guardY = y;
                    guardDir = input[y][x];
                    break;
                }
                if (guardX != 0) break;
            }
        }

        var positionsVisited = new HashSet<(int,int)>();
        while (true)
        {
            if (IsBlocked(guardX, guardY, maxX, maxY, guardDir, input))
            {
                guardDir = RotateGuard(guardDir);
                Console.WriteLine($"Reached Block. Changed direction to {guardDir}");
                continue;
            }
            positionsVisited.Add((guardX, guardY));
            (guardX, guardY) = MoveGuard(guardX, guardY, guardDir);
            if (OutOfBounds(guardX, guardY, maxX, maxY)) break;
            positionsVisited.Add((guardX, guardY));
            Console.WriteLine($"Moved guard to {guardX}, {guardY}. Positions Visited: {positionsVisited.Count}");
        }
        Console.WriteLine(positionsVisited.Count);
    }

    private static async Task PartTwo(string[] input)
    {

    }

    private static (int x, int y) MoveGuard(int x, int y, char dir)
    {
        if (dir == '^') return (x, --y);
        if (dir == '>') return (++x, y);
        if (dir == 'v') return (x, ++y);
        //if (dir == '<')
        return (--x, y);
    }

    private static bool IsBlocked(int x, int y, int maxX, int maxY, char dir, string[] input)
    {
        var nextPos = MoveGuard(x, y, dir);
        if (OutOfBounds(nextPos.x, nextPos.y, maxX, maxY))
        {
            return false;
        }
        return input[nextPos.y][nextPos.x] == '#';
    }


    private static bool OutOfBounds(int x, int y, int maxX, int maxY)
    {
        return x < 0 || y < 0 || x > maxX || y > maxY;
    }

    private static char RotateGuard(char dir) => dir switch
    {
        '^' => '>',
        '>' => 'v',
        'v' => '<',
        '<' => '^'
    };
}
