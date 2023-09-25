using BestPath.Graph.Base;

namespace BestPath.Algos.Ucs;

public class UcsNode : Node
{
    public bool visited { get; set; }
    public NodeRef? parent { get; set; }
    public int sum { get; set; }
    
    public UcsNode(uint id) : base(id)
    { }

    public UcsNode(uint id, object data) : base(id, data)
    { }
}