namespace BestPath.Graph.Base;

public readonly struct NodeRef(uint id) : IEquatable<NodeRef>
{
    public uint id { get; } = id;
    
    public static bool operator ==(NodeRef left, NodeRef right) => left.id == right.id;
    public static bool operator !=(NodeRef left, NodeRef right) => left.id != right.id;
    public bool Equals(NodeRef other) => id == other.id;
    public override bool Equals(object? obj) => obj is NodeRef other && Equals(other);
    public override int GetHashCode() => (int)id;
}