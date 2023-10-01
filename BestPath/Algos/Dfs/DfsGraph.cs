using System.Diagnostics;
using BestPath.Algos.Dfs.Enums;
using BestPath.Graph.Base;

namespace BestPath.Algos.Dfs;

public class DfsGraph : Graph<DfsNode, DfsEdge>, IAlgorithmGraph
{
    private bool run { get; set; }
    private DfsResultSnapshot? resultCache { get; set; }

    public string algorithmName => "DFS";
    public event IAlgorithmGraph.OnFinishEventHandler? OnFinish;

    public IResultSnapshot RunAlgo(NodeRef start, NodeRef goal)
    {
        if(run)
            return resultCache!;
        
        Stopwatch stopwatch = Stopwatch.StartNew();
        Stack<NodeRef> stack = new();
        int mark = 0;
        int expandedNodes = 0;
        int childrenFounded = 0;
        Stack<NodeRef> path = new();
        
        stack.Push(start);
        while (stack.Count != 0)
        {
            NodeRef currentNodeRef = stack.Pop();
            DfsNode currentNode = GetNode(currentNodeRef);
            if (currentNode.color != Color.White) continue;
            
            mark++;
            currentNode.color = Color.Gray;
            currentNode.grayAt = mark;
            path.Push(currentNodeRef);
            
            var adjacents = GetAdjacentNodes(currentNodeRef).ToList();
            childrenFounded += adjacents.Count;
            foreach (DfsNode adjacentNode in adjacents)
            {
                NodeRef adjacentNodeRef = (NodeRef)adjacentNode;
                if(adjacentNodeRef == goal)
                {
                    path.Push(adjacentNodeRef);
                    goto CreateResultSnapshot;
                }
                stack.Push(adjacentNodeRef);
            }
            
            mark++;
            expandedNodes++;
            currentNode.color = Color.Black;
            currentNode.blackAt = mark;
        }
        CreateResultSnapshot:
        
        run = true;
        stopwatch.Stop();
        resultCache = new()
        {
            algoSource = algorithmName,
            steps = mark,
            expandedNodes = expandedNodes,
            path = path,
            elapsedTime = stopwatch.Elapsed,
            branchingFactor = childrenFounded / (float)expandedNodes
        };
        
        return resultCache;
    }
    public IResultSnapshot RunAlgo(uint nodeFrom, uint nodeTo) =>
        RunAlgo(new(nodeFrom), new NodeRef(nodeTo));
}