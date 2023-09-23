using BestPath.Graph.Base;
using BestPath.Models;

namespace BestPath.Algos.Bfs;

public class BfsNode : Node
{
    public bool visited { get; set; }
    public NodeRef? parent { get; set; }
    
    public BfsNode(uint id) : base(id) {}
    public BfsNode(uint id, object data) : base(id, data) {}
}