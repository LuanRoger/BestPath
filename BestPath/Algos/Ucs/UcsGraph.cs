using System.Diagnostics;
using BestPath.Graph.Base;

namespace BestPath.Algos.Ucs;

public class UcsGraph : Graph<UcsNode, UcsEdge>, IAlgorithmGraph
{
    private PriorityQueue<UcsNode, int> queue { get; set; } = new(Comparer<int>.Create((u, u1) => u1 - u));
    private bool run { get; set; }
    private UcsResultSnapshot? resultCache { get; set; }

    public string algorithmName => "UCS";
    public event IAlgorithmGraph.OnFinishEventHandler? OnFinish;

    public IResultSnapshot RunAlgo(NodeRef start, NodeRef goal)
    {
        if(run)
            return resultCache!;
        Stopwatch stopwatch = Stopwatch.StartNew();
        queue = new();
        UcsNode root = nodes[start.id];
        UcsNode? result = null;
        int expandedNodes = 0;
        int childrenFounded = 0;
        root.visited = true;
        queue.Enqueue(root, 0);
        while (queue.Count != 0)
        {
            UcsNode currentNode = queue.Dequeue();
            NodeRef currentNodeRef = (NodeRef)currentNode;
            
            var children = GetNodesPath(currentNodeRef).ToList();
            childrenFounded += children.Count;
            foreach ((NodeRef nodeRef, uint wieght) in children)
            {
                UcsNode node = GetNode(nodeRef);
                if (node.visited) continue;
                int sum = currentNode.sum + (int)wieght;
                node.visited = true;
                node.parent = currentNodeRef;
                node.sum = sum;
                if(nodeRef == goal)
                {
                    result = GetNode(currentNodeRef);
                    goto ComputeResult;
                }
                queue.Enqueue(node, sum);
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

    private Stack<NodeRef> ConstructPath(UcsNode goal)
    {
        Stack<NodeRef> path = new();
        NodeRef currentRef = (NodeRef)goal;
        while (currentRef != default)
        {
            path.Push(currentRef);
            UcsNode current = GetNode(currentRef);
            currentRef = current.parent ?? default;
        }
        
        return path;
    }
}