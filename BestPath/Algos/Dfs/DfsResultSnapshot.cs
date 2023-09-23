using System.Collections;
using System.Text;
using BestPath.Graph.Base;

namespace BestPath.Algos.Dfs;

public record DfsResultSnapshot
{
    public required Stack<NodeRef> path { get; init; }
    public required int steps { get; init; }
    public required int expandedNodes { get; set; }

    public float branchingFactor => (float)expandedNodes / path.Count;

    public override string ToString()
    {
        StringBuilder builder = new();
        
        builder.AppendLine($"Steps: {steps}");
        builder.Append("Path: [");
        builder.AppendJoin(' ', path.Reverse());
        builder.AppendLine(" ]");
        builder.AppendLine($"Expanded nodes: {expandedNodes}");
        builder.AppendLine($"Branching factor: {branchingFactor}");
        
        return builder.ToString();
    }
}