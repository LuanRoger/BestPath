namespace BestPath.Models;

public record struct RawDistance
{
    public required uint from { get; init; }
    public required uint to { get; init; }
    public required uint distance { get; init; }
}