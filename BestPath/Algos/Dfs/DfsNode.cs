using BestPath.Algos.Dfs.Enums;
using BestPath.Graph.Base;

namespace BestPath.Algos.Dfs;

public class DfsNode : Node
{
    public int grayAt { get; set; }
    public int blackAt { get; set; }
    public Color color { get; set; } = Color.White;
    
    public DfsNode(uint id) : base(id) { }
}