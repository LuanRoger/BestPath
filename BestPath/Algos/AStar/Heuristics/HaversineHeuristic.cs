using BestPath.Models;

namespace BestPath.Algos.AStar.Heuristics;

public class HaversineHeuristic : IAStarHeuristic
{
    public double CalculateHeuristicValue(AStarNode currentNode, AStarNode goalNode)
    {
        if(currentNode.data is null || goalNode.data is null) return 0;
        
        Coordinate originCoordinate = (Coordinate)currentNode.data; 
        Coordinate destinyCoordinate = (Coordinate)goalNode.data;
        double latitudeOrigen = originCoordinate.y;
        double longitudeOrigen = originCoordinate.x;
        double latitudeDestiny = destinyCoordinate.y;
        double longitudeDestiny = destinyCoordinate.x;

        const double R = 6371.01;

        double deltaLatitude = latitudeDestiny - latitudeOrigen;
        double deltaLongitude = longitudeDestiny - longitudeOrigen;

        double a = Math.Pow(Math.Sin(deltaLatitude / 2.0), 2) + Math.Cos(latitudeOrigen) * Math.Cos(latitudeDestiny) * Math.Pow(Math.Sin(deltaLongitude / 2.0), 2);
        double c = 2 * Math.Asin(Math.Sqrt(a));

        double distancia = R * c;

        return distancia;
    }
}