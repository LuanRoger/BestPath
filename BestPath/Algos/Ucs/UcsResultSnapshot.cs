using System.Text;
using BestPath.Graph.Base;

namespace BestPath.Algos.Ucs;

public class UcsResultSnapshot : IResultSnapshot
{
    public Stack<NodeRef> path { get; init; }
    public TimeSpan elapsedTime { get; init; }
    public int expandedNodes { get; init; }
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