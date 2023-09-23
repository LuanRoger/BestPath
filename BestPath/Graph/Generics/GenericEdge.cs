using BestPath.Graph.Base;

namespace BestPath.Graph.Generics;

class GenericEdge : Edge
{
    public GenericEdge(uint to, uint from, uint wheight = 0) : base(to, from, wheight) { }
    public GenericEdge(NodeRef to, NodeRef from, uint wheight = 0) : base(to, from, wheight) { }
}