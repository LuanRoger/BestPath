using BestPath.Graph.Base;

namespace BestPath.Algos.AStar;

public class AStarEdge : Edge
{
    public AStarEdge(uint from, uint to, uint weight = 0) : base(from, to, weight) { }

    public AStarEdge(NodeRef from, NodeRef to, uint weight = 0) : base(from, to, weight) { }
}