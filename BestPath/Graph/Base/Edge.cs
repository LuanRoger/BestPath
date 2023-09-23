namespace BestPath.Graph.Base;

public abstract class Edge
{
    public uint to { get; }
    public uint from { get; }
    public uint weight { get; }
    
    public Edge(uint from, uint to, uint weight = 0)
    {
        this.weight = weight;
        this.to = to;
        this.from = from;
    }
    public Edge(NodeRef from, NodeRef to, uint weight = 0)
    {
        this.weight = weight;
        this.to = to.id;
        this.from = from.id;
    }
}