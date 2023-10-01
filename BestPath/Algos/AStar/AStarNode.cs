using BestPath.Graph.Base;
using BestPath.Models;

namespace BestPath.Algos.AStar;

public class AStarNode : Node
{
    public bool visited { get; set; }
    public NodeRef? parent { get; set; }
    
    public AStarNode(uint id) : base(id) { }

    public AStarNode(uint id, object data) : base(id, data) { }
}