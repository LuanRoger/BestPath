using System.Text;
using BestPath.Graph.Base;

namespace BestPath.Algos.AStar;

public class AStarResultSnapshot : IResultSnapshot
{
    public required AStarNode meta { get; init; }
    public required Stack<NodeRef> path { get; init; }
    public required TimeSpan elapsedTime { get; init; }
    public required int expandedNodes { get; init; }
    public float branchingFactor => (float)expandedNodes / path.Count;

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("Path: [ ");
        builder.AppendJoin(' ', path);
        builder.AppendLine(" ]");
        builder.AppendLine($"Expanded Nodes: {expandedNodes}");
        builder.AppendLine($"Branching Factor: {branchingFactor}");
        builder.AppendLine($"Elapsed Time: {elapsedTime}");
        
        return builder.ToString();
    }
}