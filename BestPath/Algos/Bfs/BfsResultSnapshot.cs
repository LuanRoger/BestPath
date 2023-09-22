using System.Text;
using BestPath.Graph.Base;

namespace BestPath.Algos.Bfs;

public class BfsResultSnapshot
{
    public required BfsNode meta { get; init; }
    public required Stack<NodeRef> path { get; init; }
    public required int expandedNodes { get; set; }
    public float branchingFactor { get; set; }

    public override string ToString()
    {
        StringBuilder builder = new();
        builder.Append("Path: [ ");
        builder.AppendJoin(' ', path);
        builder.AppendLine(" ]");
        builder.AppendLine($"Expanded Nodes: {expandedNodes}");
        builder.AppendLine($"Branching Factor: {branchingFactor}");
        
        return builder.ToString();
    }
}