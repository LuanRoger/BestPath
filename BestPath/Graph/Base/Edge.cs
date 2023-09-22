namespace BestPath.Graph.Base;

public abstract class Edge
{
    public NodeRef to { get; }
    public NodeRef from { get; }
    public uint wheight { get; }
    
    public Edge(NodeRef from, NodeRef to, uint wheight = 0)
    {
        this.wheight = wheight;
        this.to = to;
        this.from = from;
    }
}