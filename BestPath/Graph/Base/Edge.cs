namespace BestPath.Graph.Base;

public abstract class Edge
{
    public NodeRef to { get; }
    public NodeRef from { get; }
    public uint weight { get; }
    
    public Edge(NodeRef from, NodeRef to, uint weight = 0)
    {
        this.weight = weight;
        this.to = to;
        this.from = from;
    }
}