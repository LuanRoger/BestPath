using BestPath.Graph.Base;

namespace BestPath.Algos.Bfs;

public class BfsEdge : Edge
{
    public BfsEdge(uint from, uint to, uint weight = 0) : base(from, to, weight) { }
    public BfsEdge(NodeRef from, NodeRef to, uint weight = 0) : base(from, to, weight) { }
}