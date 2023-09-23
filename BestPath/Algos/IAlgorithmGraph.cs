using BestPath.Graph.Base;

namespace BestPath.Algos;

public interface IAlgorithmGraph 
{
    public IResultSnapshot RunAlgo(NodeRef nodeFrom, NodeRef nodeTo);
}