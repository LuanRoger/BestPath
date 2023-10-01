using BestPath.Graph.Base;
using BestPath.Models;

namespace BestPath.Algos.AStar.Heuristics;

public class FlatEarthHeuristic : IAStarHeuristic
{
    public double CalculateHeuristicValue(AStarNode currentNode, AStarNode goalNode)
    {
        if(currentNode.data is null || goalNode.data is null) return 0;
        
        Coordinate thisCoordinate = (Coordinate) currentNode.data;
        Coordinate toCoordinate = (Coordinate) goalNode.data;
        
        double heuristicValue = Math.Sqrt(Math.Pow(toCoordinate.x - thisCoordinate.x, 2) + 
                                          Math.Pow(toCoordinate.y - thisCoordinate.y, 2));
        return heuristicValue;
    }
}