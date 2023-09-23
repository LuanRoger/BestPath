using BestPath.Graph.Base;

namespace BestPath.Algos.Bfs;

public class BfsGraph : Graph<BfsNode, BfsEdge>
{
    private PriorityQueue<BfsNode, uint> queue { get; set; } = new();
    private bool run { get; set; }
    private BfsResultSnapshot? resultCache { get; set; }
    
    public BfsResultSnapshot RunAlgo(NodeRef start, NodeRef goal)
    {
        if(run)
            return resultCache!;
        queue = new();
        BfsNode root = nodes[start.id];
        BfsNode? result = null;
        int expandedNodes = 0;
        root.visited = true;
        queue.Enqueue(root, 0);
        while (queue.Count != 0)
        {
            NodeRef currentNode = (NodeRef)queue.Dequeue();
            expandedNodes++;
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
                queue.Enqueue(node, wieght);
            }
        }
        
        run = true;
        resultCache = new()
        {
            meta = result!,
            path = result is not null ? ConstructPath(result) : new(),
            expandedNodes = expandedNodes
        };
        return resultCache;
    }
    
    public Stack<NodeRef> ConstructPath(BfsNode goal)
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