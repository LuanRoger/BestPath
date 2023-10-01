using System.Diagnostics;
using BestPath.Graph.Base;

namespace BestPath.Algos.Bfs;

public class BfsGraph : Graph<BfsNode, BfsEdge>, IAlgorithmGraph
{
    private PriorityQueue<BfsNode, uint> queue { get; set; } = new(Comparer<uint>.Create((u, u1) => (int)(u1 - u)));
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
        int childrenFounded = 0;
        root.visited = true;
        queue.Enqueue(root, 0);
        while (queue.Count != 0)
        {
            NodeRef currentNode = (NodeRef)queue.Dequeue();
            var children = GetNodesPath(currentNode).ToList();
            childrenFounded += children.Count;
            foreach ((NodeRef nodeRef, uint wieght) in children)
            {
                BfsNode node = GetNode(nodeRef);
                if (node.visited) continue;
                node.visited = true;
                node.parent = currentNode;
                if(nodeRef == goal)
                {
                    result = GetNode(currentNode);
                    goto ComputeResult;
                }
                queue.Enqueue(node, wieght);
            }
            expandedNodes++;
        }
        ComputeResult:
        
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

    private Stack<NodeRef> ConstructPath(BfsNode goal)
    {
        Stack<NodeRef> path = new();
        NodeRef currentRef = (NodeRef)goal;
        while (currentRef != default)
        {
            path.Push(currentRef);
            BfsNode current = GetNode(currentRef);
            currentRef = current.parent ?? default;
        }
        
        return path;
    }
}