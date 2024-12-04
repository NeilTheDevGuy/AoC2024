namespace AoC2024.Days;

public static class Day4
{    
    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(4);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //2654
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //1990
    }

    private static async Task PartOne(string[] input)
    {
        var xmasCount = 0;        
        for (int y = 0; y < input.Length; y++)
        {            
            for (int x = 0; x < input[y].Length; x++)
            {
                if (input[y][x] != 'X') continue;                
                                
                //Left-Right
                if (input[y].Length - x > 3)
                {
                    if (input[y][x + 1] == 'M' & input[y][x + 2] == 'A' && input[y][x + 3] == 'S') xmasCount++;
                }

                //Right-Left
                if (x >= 3)
                {
                    if (input[y][x - 1] == 'M' & input[y][x - 2] == 'A' && input[y][x - 3] == 'S') xmasCount++;                    
                }

                //Up
                if (y >= 3)
                {
                    if (input[y - 1][x] == 'M' && input[y - 2][x] == 'A' && input[y - 3][x] == 'S') xmasCount++;                    
                }

                //Down
                if (input.Length - y >= 3)
                {
                    if (input[y + 1][x] == 'M' && input[y + 2][x] == 'A' && input[y + 3][x] == 'S') xmasCount++;                    
                }

                //Diag Down L-R
                if (input.Length - y > 3 && input[y].Length - x > 3)
                {
                    if (input[y + 1][x + 1] == 'M' && input[y + 2][x + 2] == 'A' && input[y + 3][x + 3] == 'S') xmasCount++;                    
                }

                //Diag Down R-L
                if (input.Length - y > 3 && x >= 3)
                {
                    if (input[y + 1][x - 1] == 'M' && input[y + 2][x - 2] == 'A' && input[y + 3][x - 3] == 'S') xmasCount++;                    
                }

                //Diag Up L-R
                if (y >= 3 && input[y].Length - x > 3)
                {
                    if (input[y - 1][x + 1] == 'M' && input[y - 2][x + 2] == 'A' && input[y - 3][x + 3] == 'S') xmasCount++;                    
                }

                //Diag Up R-L
                if (y >= 3 && x >= 3)
                {
                    if (input[y - 1][x - 1] == 'M' && input[y - 2][x - 2] == 'A' && input[y - 3][x - 3] == 'S') xmasCount++;                    
                }
            }
        }
        Console.WriteLine($"Found {xmasCount} XMAS");
    }

    private static async Task PartTwo(string[] input)
    {
        var masCount = 0;
        for (int y = 1; y < input.Length - 1; y++)
        {
            for (int x = 1; x < input[y].Length - 1; x++)
            {
                if (input[y][x] != 'A') continue;

                //Check both combinations of MAS and SAM in both diagonals
                if ((input[y - 1][x - 1] == 'M' && input[y - 1][x + 1] == 'M' && input[y + 1][x - 1] == 'S' && input[y + 1][x + 1] == 'S') ||
                    (input[y - 1][x - 1] == 'S' && input[y - 1][x + 1] == 'M' && input[y + 1][x - 1] == 'S' && input[y + 1][x + 1] == 'M') ||
                    (input[y - 1][x - 1] == 'S' && input[y - 1][x + 1] == 'S' && input[y + 1][x - 1] == 'M' && input[y + 1][x + 1] == 'M') ||
                    (input[y - 1][x - 1] == 'M' && input[y - 1][x + 1] == 'S' && input[y + 1][x - 1] == 'M' && input[y + 1][x + 1] == 'S')) masCount++;                
            }

        }
        Console.WriteLine($"Found {masCount} MAS crosses");
    }    
}
