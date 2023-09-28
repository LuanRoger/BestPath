namespace BestPath.Models;

public class Preferences
{
    public TimeSpan runLimit { get; init; }
    public int dfsMaxDepth { get; init; }
}