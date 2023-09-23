using BestPath.Algos.Bfs;
using BestPath.Graph.Base;
using BestPath.Models;

namespace BestPath.Algos.AStar;

public class AStarNode : Node
{
    public bool visited { get; set; }
    public NodeRef? parent { get; set; }
    private double? distance { get; set; }
    private NodeRef? to { get; set; }
    
    public AStarNode(uint id) : base(id) { }

    public AStarNode(uint id, object data) : base(id, data) { }
    
    public bool CalcualteDistance(AStarNode node, out double heuristicValue)
    {
        heuristicValue = 0;
        if(data is null || node.data is null) return false;
        
        Coordinate thisCoordinate = (Coordinate) data;
        Coordinate toCoordinate = (Coordinate) node.data;
        
        heuristicValue = Math.Sqrt(Math.Pow(toCoordinate.x - thisCoordinate.x, 2) + 
                                   Math.Pow(toCoordinate.y - thisCoordinate.y, 2));
        distance = heuristicValue;
        to = (NodeRef)node;
        
        return true;
    }
}