using BestPath.Graph.Base;

namespace BestPath.Algos;

public interface IResultSnapshot
{
    public string algoSource { get; init; }
    public Stack<NodeRef> path { get; init; }
    public TimeSpan elapsedTime { get; init; }
    public int expandedNodes { get; init; }
    public float branchingFactor { get; init; }
    
    public string ToString();
}