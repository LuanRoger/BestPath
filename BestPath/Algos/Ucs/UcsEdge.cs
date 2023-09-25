using BestPath.Graph.Base;

namespace BestPath.Algos.Ucs;

public class UcsEdge : Edge
{
    public UcsEdge(uint from, uint to, uint weight = 0) : base(from, to, weight)
    { }

    public UcsEdge(NodeRef from, NodeRef to, uint weight = 0) : base(from, to, weight)
    { }
}