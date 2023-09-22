using BestPath.Graph.Base;

namespace BestPath.Algos.Dfs;

public class DfsEdge : Edge
{
    public DfsEdge(NodeRef from, NodeRef to, uint wheight = 0) : base(from, to, wheight) { }
}