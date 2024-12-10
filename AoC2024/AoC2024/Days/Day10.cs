namespace AoC2024.Days;

public static class Day10
{
    private static int _maxX;
    private static int _maxY;
    private static string[] _input;
    private static List<TrailHead> _trailheads = new();

    public static async Task Run()
    {
        _input = await InputGetter.GetFromLinesAsString(10);
        var timedExecutor = new TimedExecutor();
        await timedExecutor.ExecuteTimed(PartOneAndTwo); //794 //1706
    }

    private static async Task PartOneAndTwo()
    {
        _maxX = _input[0].Length - 1;
        _maxY = _input.Length - 1;
        //get the trailheads to start with        
        for (int y = 0; y <= _maxY; y++)
        {
            for (int x = 0; x <= _maxX; x++)
            {
                if (_input[y][x] == '0')
                {
                    _trailheads.Add(new TrailHead { Point = new Point(x, y) });
                }
            }
        }        
        foreach (var trailhead in _trailheads)
        {         
            TraverseTrailhead(trailhead, trailhead.Point);         
        }
        Console.WriteLine($"Total Score: {_trailheads.Sum(t => t.EndPoints.Count)}");
        Console.WriteLine($"Total Distinct Score: {_trailheads.Sum(t => t.DistinctEndPoints.Count)}");
    }

    private static void TraverseTrailhead(TrailHead trailhead, Point point)
    {
        if (_input[point.Y][point.X] == '9')
        {            
            trailhead.EndPoints.Add(point);
            trailhead.DistinctEndPoints.Add(point);
            return;
        }

        //Can go u/d/l/r
        var possibleSteps = new List<Point>();

        var upPoint = new Point(point.X, point.Y - 1);
        var downPoint = new Point(point.X, point.Y + 1);
        var leftPoint = new Point(point.X - 1, point.Y);
        var rightPoint = new Point(point.X + 1, point.Y);
        var currentHeight = int.Parse(_input[point.Y][point.X].ToString());        

        //Up        
        if (InBounds(upPoint)) {
            var diffFromCurrentHeight = _input[point.Y - 1][point.X] == '.' ? 0 : int.Parse(_input[point.Y - 1][point.X].ToString()) - currentHeight;
            if (diffFromCurrentHeight == 1)
                possibleSteps.Add(new Point(point.X, point.Y - 1));
        }

        //Down
        if (InBounds(downPoint)) {
            var diffFromCurrentHeight = _input[point.Y + 1][point.X] == '.' ? 0 : int.Parse(_input[point.Y + 1][point.X].ToString()) - currentHeight;
            if (diffFromCurrentHeight == 1)
                possibleSteps.Add(new Point(point.X, point.Y + 1));
        }

        //Left
        if (InBounds(leftPoint)) {
            var diffFromCurrentHeight = _input[point.Y][point.X - 1] == '.' ? 0 : int.Parse(_input[point.Y][point.X - 1].ToString()) - currentHeight;
            if (diffFromCurrentHeight == 1)
                possibleSteps.Add(new Point(point.X - 1, point.Y));
        }

        //Right
        if (InBounds(rightPoint)) {
            var diffFromCurrentHeight = _input[point.Y][point.X + 1] == '.' ? 0 : int.Parse(_input[point.Y][point.X + 1].ToString()) - currentHeight;
            if (diffFromCurrentHeight == 1)
                possibleSteps.Add(new Point(point.X + 1, point.Y));
        }

        foreach (var step in possibleSteps)
        {
            TraverseTrailhead(trailhead, step);
        }        
    }

    private static bool InBounds(Point point)
    {
        return point.X >= 0 && point.X <= _maxX && point.Y >= 0 && point.Y <= _maxY;
    }

    private record Point (int X, int Y);

    private class TrailHead
    {
        public Point Point { get; set; }
        public HashSet<Point> EndPoints { get; set; } = new HashSet<Point>();
        public List<Point> DistinctEndPoints { get; set; } = new List<Point>();
    }
}
