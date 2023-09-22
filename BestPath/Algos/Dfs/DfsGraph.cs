using BestPath.Algos.Dfs.Enums;
using BestPath.Graph.Base;

namespace BestPath.Algos.Dfs;

public class DfsGraph : Graph<DfsNode, DfsEdge>
{
    private int mark { get; set; }
    private Stack<DfsNode> stack { get; } = new();
    private bool run { get; set; }
    private DfsResultSnapshot? resultCache { get; set; }
    
    public DfsResultSnapshot RunAlgo(NodeRef start)
    {
        if(run)
            return resultCache!;
        
        DfsVisit(start);
        foreach ((uint _, DfsNode node) in nodes)
        {
            if(start is not null && node.id == start.id) 
                continue;
            if(node.color == Color.White)
                DfsVisit((NodeRef)node);
        }
        
        run = true;
        resultCache = new()
        {
            stack = stack,
            steps = mark,
            metaVariant = false
        };
        
        return resultCache;
    }
    
    public DfsResultSnapshot RunAlgoMeta(NodeRef start, NodeRef goal)
    {
        if(run)
            return resultCache!;
        if(goal is null)
            throw new ArgumentException("Goal node is not set", nameof(goal));

        bool findResultAtStart = DfsVisitMeta(start, goal, out var path);
        if(!findResultAtStart)
            foreach ((uint _, DfsNode node) in nodes)
            {
                if (start is not null && node.id == start.id || 
                    node.color != Color.White) continue;
                
                if(DfsVisitMeta((NodeRef)node, goal, out path)) break;
            }
        
        run = true;
        resultCache = new()
        {
            stack = path,
            steps = mark,
            metaVariant = true
        };
        
        return resultCache;
    }
    
    private void DfsVisit(NodeRef nodeRef)
    {
        DfsNode node = nodes[nodeRef.id];
        mark++;
        node.color = Color.Gray;
        node.grayAt = mark;

        foreach (DfsNode adjacentNode in GetAdjacents(nodeRef)
                     .Select(edge => nodes[edge.from.id])
                     .Where(adjacentNode => adjacentNode.color == Color.White))
            DfsVisit((NodeRef)adjacentNode);
        
        mark++;
        node.color = Color.Black;
        node.blackAt = mark;
        stack.Push(node);
    }
    
    private bool DfsVisitMeta(NodeRef nodeRef, NodeRef goal, out Stack<DfsNode> path)
    {
        DfsNode node = nodes[nodeRef.id];
        mark++;
        node.color = Color.Gray;
        node.grayAt = mark;
        
        path = new();
        path.Push(node);
        
        var adjacents = GetAdjacentsMeta(nodeRef);
        foreach (DfsNode adjacentNode in adjacents
                     .Where(adjacentNode => adjacentNode.color == Color.White))
        {
            if(adjacentNode.id == goal.id)
            {
                path.Push(adjacentNode);
                return true;
            }
            DfsVisit((NodeRef)adjacentNode);
        }
        
        mark++;
        node.color = Color.Black;
        node.blackAt = mark;
        stack.Push(node);
        return false;
    }
    
    private IEnumerable<DfsNode> GetAdjacentsMeta(NodeRef nodeRef) => 
        edges.FindAll(edge => edge.from.id == nodeRef.id)
            .Select(edge => nodes[edge.to.id]);
    private IEnumerable<DfsEdge> GetAdjacents(NodeRef nodeRef) => 
        edges.FindAll(edge => edge.to.id == nodeRef.id);
    
    public void Reset()
    {
        run = false;
        mark = 0;
        stack.Clear();
        resultCache = null;
    }
}