namespace BestPath.Algos.AStar.Heuristics;

public interface IAStarHeuristic
{
    public double CalculateHeuristicValue(AStarNode currentNode, AStarNode goalNode);
}