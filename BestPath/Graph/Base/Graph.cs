namespace BestPath.Graph.Base;

public abstract class Graph<N, E> where N : Node where E : Edge
{
    protected Dictionary<uint, N> nodes { get; }
    protected List<E> edges { get; }

    public Graph()
    {
        nodes = new();
        edges = new();
    }
    public Graph(List<N> nodes, List<E> edges)
    {
        this.nodes = nodes.ToDictionary(node => node.id, node => node);
        this.edges = edges;
    }
    
    public NodeRef GetSomeNodeRef(uint id) => (NodeRef)nodes[id];
    
    public NodeRef AddNode(N node)
    {
        bool isOnGraph = nodes.TryGetValue(node.id, out N? existingNode);
        if(isOnGraph) return new(existingNode!.id);
        
        nodes.Add(node.id, node);
        return (NodeRef)node;
    }
    
    public void AddNodeRange(IEnumerable<N> nodes)
    {
        foreach (N node in nodes)
            this.nodes.TryAdd(node.id, node);
    }

    public void AddEdge(E edge) => edges.Add(edge);
    
    public void AddEdgeRange(IEnumerable<E> edges) => this.edges.AddRange(edges);
    
    protected N GetNode(NodeRef nodeRef) => nodes[nodeRef.id];
    
    protected IEnumerable<N> GetAdjacentNodes(NodeRef nodeRef) =>
        edges
            .FindAll(edge => edge.from == nodeRef.id)
            .Select(edge => nodes[edge.to]);
    
    protected IEnumerable<(NodeRef, uint)> GetNodesPath(NodeRef nodeRef) => 
        edges
        .FindAll(edge => edge.from == nodeRef.id)
        .Select(edge => ((NodeRef)nodes[edge.to], edge.weight));
}