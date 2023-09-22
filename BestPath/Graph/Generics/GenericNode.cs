namespace BestPath.Graph.Generics;

class GenericNode : Base.Node
{
    public uint id { get; set; }

    public GenericNode(uint id) : base(id) { }
}