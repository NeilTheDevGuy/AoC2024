namespace AoC2024.Days;

public static class Day8
{
    static int _maxX;
    static int _maxY;
    static int _minX;
    static int _minY;

    public static async Task Run()
    {
        var input = await InputGetter.GetFromLinesAsString(8);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(() => PartOne(input)); //247
        await timedExecutor.ExecuteTimed(() => PartTwo(input)); //861
    }

    private static async Task PartOne(string[] input)
    {
        _minX = 0;
        _minY = 0;
        _maxX = input[0].Length - 1;
        _maxY = input.Length - 1;
        var grid = BuildGrid(input);
        var frequencies = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
        var freqPoints = grid.Where(g => frequencies.Contains(g.Frequency)).ToList();
        foreach (var freqPoint in freqPoints)
        {
            //Find matching frequencies
            var matchingFrequencies = grid.Where(g => g.Frequency == freqPoint.Frequency && g.X != freqPoint.X && g.Y != freqPoint.Y).ToList();
            foreach (var matchingFreq in matchingFrequencies)
            {
                var xDiff = matchingFreq.X - freqPoint.X;
                var yDiff = matchingFreq.Y - freqPoint.Y;
                var antiNode1 = new GridPoint { X = freqPoint.X - xDiff, Y = freqPoint.Y - yDiff, IsAntiNode = true };
                var antiNode2 = new GridPoint { X = matchingFreq.X + xDiff, Y = matchingFreq.Y + yDiff, IsAntiNode = true };

                var existingPoint1 = grid.FirstOrDefault(g => g.X == antiNode1.X && g.Y == antiNode1.Y);
                if (existingPoint1 != null)
                {
                    existingPoint1.IsAntiNode = true;
                }
                else if (IsInBounds(antiNode1.X, antiNode1.Y))
                {
                    grid.Add(antiNode1);
                }

                var existingPoint2 = grid.FirstOrDefault(g => g.X == antiNode2.X && g.Y == antiNode2.Y);
                if (existingPoint2 != null)
                {
                    existingPoint2.IsAntiNode = true;
                }
                else if (IsInBounds(antiNode2.X, antiNode2.Y))
                {
                    grid.Add(antiNode2);
                }
            }
        }
        var antiNodeCount = grid.Count(g => g.IsAntiNode);
        Console.WriteLine(antiNodeCount);
    }

    private static async Task PartTwo(string[] input)
    {
        _minX = 0;
        _minY = 0;
        _maxX = input[0].Length - 1;
        _maxY = input.Length - 1;
        var grid = BuildGrid(input);
        var frequencies = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
        var freqPoints = grid.Where(g => frequencies.Contains(g.Frequency)).ToList();
        foreach (var freqPoint in freqPoints)
        {
            //Find matching frequencies
            var matchingFrequencies = grid.Where(g => g.Frequency == freqPoint.Frequency && g.X != freqPoint.X && g.Y != freqPoint.Y).ToList();
            matchingFrequencies.ForEach(m => m.IsAntiNode = true); //Any matching frequency is an antenna
            foreach (var matchingFreq in matchingFrequencies)
            {
                var xDiff = matchingFreq.X - freqPoint.X;
                var yDiff = matchingFreq.Y - freqPoint.Y;

                while (true)
                {
                    var antiNode1 = new GridPoint { X = freqPoint.X - xDiff, Y = freqPoint.Y - yDiff, IsAntiNode = true };
                    var antiNode2 = new GridPoint { X = matchingFreq.X + xDiff, Y = matchingFreq.Y + yDiff, IsAntiNode = true };

                    var existingPoint1 = grid.FirstOrDefault(g => g.X == antiNode1.X && g.Y == antiNode1.Y);
                    if (existingPoint1 != null)
                    {
                        existingPoint1.IsAntiNode = true;
                    }
                    else if (IsInBounds(antiNode1.X, antiNode1.Y))
                    {
                        grid.Add(antiNode1);
                    }
                    
                    var existingPoint2 = grid.FirstOrDefault(g => g.X == antiNode2.X && g.Y == antiNode2.Y);
                    if (existingPoint2 != null)
                    {
                        existingPoint2.IsAntiNode = true;
                    }
                    else if (IsInBounds(antiNode2.X, antiNode2.Y))
                    {
                        grid.Add(antiNode2);
                    }                    

                    if (!IsInBounds(antiNode1.X, antiNode1.Y) && !IsInBounds(antiNode2.X, antiNode2.Y)) break;

                    xDiff += matchingFreq.X - freqPoint.X;
                    yDiff += matchingFreq.Y - freqPoint.Y;
                }
            }
        }
        var antiNodeCount = grid.Count(g => g.IsAntiNode);
        Console.WriteLine(antiNodeCount);
    }

    private static bool IsInBounds(int x, int y)
    {
        return x >= _minX && x <= _maxX && y >= _minY && y <= _maxY;
    }

    public static List<GridPoint> BuildGrid(string[] input)
    {
        var grid = new List<GridPoint>();
        for (int x = 0; x < input[0].Length; x++)
        {
            for (int y = 0; y < input.Length; y++) 
            {
                grid.Add(new GridPoint { X = x, Y = y, Frequency = input[y][x], IsAntiNode = false });
            }
        }
        return grid;
    }

    public class GridPoint()
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Frequency { get; set; }
        public bool IsAntiNode { get; set; }
    }
}
