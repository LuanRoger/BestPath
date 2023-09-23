namespace BestPath.Graph.Base;

public abstract class Node
{
    public uint id { get; }
    public object? data { get; set; }

    public Node(uint id)
    {
        this.id = id;
    }
    public Node(uint id, object data)
    {
        this.id = id;
        this.data = data;
    }
    
    public static explicit operator NodeRef(Node node) => new(node.id);
    public static bool operator ==(Node node, NodeRef nodeRef) => node.id == nodeRef.id;
    public static bool operator !=(Node node, NodeRef nodeRef) => node.id != nodeRef.id;
}