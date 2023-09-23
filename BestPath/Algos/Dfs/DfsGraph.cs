using BestPath.Algos.Dfs.Enums;
using BestPath.Graph.Base;

namespace BestPath.Algos.Dfs;

public class DfsGraph : Graph<DfsNode, DfsEdge>
{
    private bool run { get; set; }
    private DfsResultSnapshot? resultCache { get; set; }
    
    public DfsResultSnapshot RunAlgo(NodeRef start, NodeRef goal)
    {
        if(run)
            return resultCache!;
        
        Stack<NodeRef> stack = new();
        int mark = 0;
        int expandedNodes = 0; 
        Stack<NodeRef> path = new();
        
        stack.Push(start);
        while (stack.Count != 0)
        {
            NodeRef currentNodeRef = stack.Pop();
            DfsNode currentNode = GetNode(currentNodeRef);
            if (currentNode.color != Color.White) continue;
            
            currentNode.color = Color.Gray;
            mark++;
            currentNode.grayAt = mark;
            path.Push(currentNodeRef);
            
            foreach (DfsNode adjacentNode in GetAdjacentNodes(currentNodeRef))
            {
                NodeRef adjacentNodeRef = (NodeRef)adjacentNode;
                if(adjacentNodeRef == goal)
                {
                    path.Push(adjacentNodeRef);
                    goto CreateResultSnapshot;
                }
                stack.Push(adjacentNodeRef);
            }
            
            currentNode.color = Color.Black;
            mark++;
            currentNode.blackAt = mark;
            expandedNodes++;
        }
        CreateResultSnapshot:
        
        run = true;
        resultCache = new()
        {
            steps = mark,
            expandedNodes = expandedNodes,
            path = path
        };
        
        return resultCache;
    }
    
    public void Reset()
    {
        run = false;
        resultCache = null;
    }
}