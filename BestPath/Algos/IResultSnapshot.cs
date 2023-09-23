using BestPath.Graph.Base;

namespace BestPath.Algos;

public interface IResultSnapshot
{
    public Stack<NodeRef> path { get; init; }
    public TimeSpan elapsedTime { get; init; }
    public int expandedNodes { get; init; }
    public float branchingFactor => (float)expandedNodes / path.Count;
    
    public string ToString();
}