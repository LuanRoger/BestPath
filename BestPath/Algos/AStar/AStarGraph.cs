using System.Diagnostics;
using BestPath.Graph.Base;

namespace BestPath.Algos.AStar;

public class AStarGraph : Graph<AStarNode, AStarEdge>, IAlgorithmGraph
{
    private PriorityQueue<AStarNode, uint> queue { get; set; } = 
        new(Comparer<uint>.Create((u, u1) => (int)(u1 - u)));
    private bool run { get; set; }
    private AStarResultSnapshot? resultCache { get; set; }
    
    public IResultSnapshot RunAlgo(NodeRef start, NodeRef goal)
    {
        if(run)
            return resultCache!;
        Stopwatch stopwatch = Stopwatch.StartNew();
        queue = new();
        AStarNode root = nodes[start.id];
        AStarNode goalNode = nodes[goal.id];
        AStarNode? result = null;
        int expandedNodes = 0;
        root.visited = true;
        queue.Enqueue(root, 0);
        while (queue.Count != 0)
        {
            NodeRef currentNode = (NodeRef)queue.Dequeue();
            if(currentNode == goal)
            {
                result = GetNode(currentNode);
                break;
            }
            foreach ((NodeRef nodeRef, uint wieght) in GetNodesPath(currentNode))
            {
                AStarNode node = GetNode(nodeRef);
                if (node.visited) continue;
                node.visited = true;
                node.parent = currentNode;
                
                node.CalcualteDistance(goalNode, out double heuristicValue);
                uint priority = (uint)(heuristicValue + wieght);
                queue.Enqueue(node, priority);
            }
            expandedNodes++;
        }
        
        run = true;
        stopwatch.Stop();
        resultCache = new()
        {
            meta = result!,
            path = result is not null ? ConstructPath(result) : new(),
            elapsedTime = stopwatch.Elapsed,
            expandedNodes = expandedNodes
        };
        return resultCache;
    }

    private Stack<NodeRef> ConstructPath(AStarNode goal)
    {
        Stack<NodeRef> path = new();
        NodeRef? currentRef = (NodeRef)goal;
        while (currentRef is not null)
        {
            path.Push(currentRef);
            AStarNode current = GetNode(currentRef);
            currentRef = current.parent;
        }
        
        return path;
    }
}