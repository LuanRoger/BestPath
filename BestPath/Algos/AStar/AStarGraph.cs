using System.Diagnostics;
using BestPath.Algos.AStar.Heuristics;
using BestPath.Graph.Base;

namespace BestPath.Algos.AStar;

public class AStarGraph : Graph<AStarNode, AStarEdge>, IAlgorithmGraph
{
    private PriorityQueue<AStarNode, uint> queue { get; set; } = 
        new(Comparer<uint>.Create((u, u1) => (int)(u1 - u)));
    private bool run { get; set; }
    private AStarResultSnapshot? resultCache { get; set; }

    public string algorithmName => "A*";
    public event IAlgorithmGraph.OnFinishEventHandler? OnFinish;
    private IAStarHeuristic heuristic { get; set; } = new FlatEarthHeuristic();

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
        int childrenFounded = 0;
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
            var children = GetNodesPath(currentNode).ToList();
            childrenFounded += children.Count;
            foreach ((NodeRef nodeRef, uint wieght) in children)
            {
                AStarNode node = GetNode(nodeRef);
                if (node.visited) continue;
                node.visited = true;
                node.parent = currentNode;
                
                double heuristicValue = heuristic.CalculateHeuristicValue(node, goalNode);
                uint priority = (uint)(heuristicValue + wieght);
                queue.Enqueue(node, priority);
            }
            expandedNodes++;
        }
        
        run = true;
        stopwatch.Stop();
        resultCache = new()
        {
            algoSource = algorithmName,
            path = result is not null ? ConstructPath(result) : new(),
            elapsedTime = stopwatch.Elapsed,
            expandedNodes = expandedNodes,
            branchingFactor = childrenFounded / (float)expandedNodes
        };
        OnFinish?.Invoke(this, new()
        {
            elapsedTime = stopwatch.Elapsed
        });
        return resultCache;
    }

    public IResultSnapshot RunAlgo(uint nodeFrom, uint nodeTo) => 
        RunAlgo(GetSomeNodeRef(nodeFrom), GetSomeNodeRef(nodeTo));

    private Stack<NodeRef> ConstructPath(AStarNode goal)
    {
        Stack<NodeRef> path = new();
        NodeRef currentRef = (NodeRef)goal;
        while (currentRef != default)
        {
            path.Push(currentRef);
            AStarNode current = GetNode(currentRef);
            currentRef = current.parent ?? default;
        }
        
        return path;
    }
    
    public void Heuristic(IAStarHeuristic newHeuristic) => heuristic = newHeuristic;
    
    public void CleanResult()
    {
        run = false;
        resultCache = null;
    }
}