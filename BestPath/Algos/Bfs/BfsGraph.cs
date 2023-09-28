using System.Diagnostics;
using BestPath.Graph.Base;

namespace BestPath.Algos.Bfs;

public class BfsGraph : Graph<BfsNode, BfsEdge>, IAlgorithmGraph
{
    private Queue<BfsNode> queue { get; set; } = new();
    private bool run { get; set; }
    private BfsResultSnapshot? resultCache { get; set; }

    public string algorithmName => "BFS";
    public event IAlgorithmGraph.OnFinishEventHandler? OnFinish;

    public IResultSnapshot RunAlgo(NodeRef start, NodeRef goal)
    {
        if(run)
            return resultCache!;
        Stopwatch stopwatch = Stopwatch.StartNew();
        queue = new();
        BfsNode root = nodes[start.id];
        BfsNode? result = null;
        int expandedNodes = 0;
        root.visited = true;
        queue.Enqueue(root);
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
                BfsNode node = GetNode(nodeRef);
                if (node.visited) continue;
                node.visited = true;
                node.parent = currentNode;
                queue.Enqueue(node);
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
        OnFinish?.Invoke(this, new()
        {
            elapsedTime = stopwatch.Elapsed
        });
        return resultCache;
    }

    public IResultSnapshot RunAlgo(uint nodeFrom, uint nodeTo) =>
        RunAlgo(GetSomeNodeRef(nodeFrom), GetSomeNodeRef(nodeTo));

    private Stack<NodeRef> ConstructPath(BfsNode goal)
    {
        Stack<NodeRef> path = new();
        NodeRef? currentRef = (NodeRef)goal;
        while (currentRef is not null)
        {
            path.Push(currentRef);
            BfsNode current = GetNode(currentRef);
            currentRef = current.parent;
        }
        
        return path;
    }
}