﻿namespace BestPath.Graph.Base;

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
    
    public virtual NodeRef AddNode(N node)
    {
        bool isOnGraph = nodes.TryGetValue(node.id, out N? existingNode);
        if(isOnGraph) return new(existingNode!.id);
        
        nodes.Add(node.id, node);
        return (NodeRef)node;
    }

    public virtual void AddEdge(E edge)
    {
        edges.Add(edge);
    }
    
    protected N GetNode(NodeRef nodeRef)
    {
        return nodes[nodeRef.id];
    }
    
    protected IEnumerable<N> GetDestinyNodes(NodeRef nodeRef) =>
        edges
            .FindAll(edge => edge.from.id == nodeRef.id)
            .Select(edge => nodes[edge.to.id]);
    
    protected Dictionary<NodeRef, uint> GetNodesPath(NodeRef nodeRef) => 
        edges
        .FindAll(edge => edge.from.id == nodeRef.id)
        .ToDictionary(edge => (NodeRef)nodes[edge.to.id], e => e.weight);
}