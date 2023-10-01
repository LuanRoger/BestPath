using System.Text;
using BestPath.Graph.Base;

namespace BestPath.Algos.Bfs;

public class BfsResultSnapshot : IResultSnapshot
{
    public required string algoSource { get; init; }
    public required Stack<NodeRef> path { get; init; }
    public required TimeSpan elapsedTime { get; init; }
    public required int expandedNodes { get; init; }
    public required float branchingFactor { get; init; }

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