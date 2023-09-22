using System.Text;

namespace BestPath.Algos.Dfs;

public record DfsResultSnapshot
{
    public required Stack<DfsNode> stack { get; init; }
    public required int steps { get; init; }
    public required bool metaVariant { get; init; }

    public override string ToString()
    {
        StringBuilder builder = new();
        
        builder.AppendLine($"Steps: {steps}");
        builder.Append("Stack: [");
        builder.AppendJoin(' ', stack.Select(node => node.id));
        builder.Append(']');
        
        return builder.ToString();
    }
}