namespace BestPath.Models;

public record struct RawCoordinates
{
    public required uint location { get; init; }
    public required int x { get; init; }
    public required int y { get; init; }
}