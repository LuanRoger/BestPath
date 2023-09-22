namespace BestPath.Graph.Base;

public abstract class Node
{
    public uint id { get; }

    public Node(uint id)
    {
        this.id = id;
    }
    
    public static explicit operator NodeRef(Node node) => new(node.id);
}