using BestPath.Graph.Base;

namespace BestPath.Algos.Bfs;

public class BfsNode : Node
{
    public bool visited { get; set; }
    public NodeRef? parent { get; set; }
    
    public BfsNode(uint id) : base(id) {}
}