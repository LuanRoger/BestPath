using BestPath.Models;

namespace BestPath.Algos.AStar.Heuristics;

public class HaversineHeuristic : IAStarHeuristic
{
    public double CalculateHeuristicValue(AStarNode currentNode, AStarNode goalNode)
    {
        if(currentNode.data is null || goalNode.data is null) return 0;
        
        const double radius = 6378100;
        Coordinate thisCoordinate = (Coordinate) currentNode.data;
        Coordinate toCoordinate = (Coordinate) goalNode.data;
        
        double sdlat = Math.Sin((toCoordinate.y - thisCoordinate.y) / 2);
        double sdlon = Math.Sin((toCoordinate.x - thisCoordinate.x) / 2);
        double q = sdlat * sdlat + Math.Cos(toCoordinate.y) * Math.Cos(thisCoordinate.y) * sdlon * sdlon;
        double d = 2 * radius * Math.Asin(Math.Sqrt(q));

        return d;
    }
}