using BestPath.Graph.Base;
using BestPath.Models.EventArgs;

namespace BestPath.Algos;

public interface IAlgorithmGraph 
{
    public string algorithmName { get; }
    public delegate void OnFinishEventHandler(object sender, AlgosPartialResultSnapshotEventArgs e);
    public event OnFinishEventHandler? OnFinish;
    public IResultSnapshot RunAlgo(NodeRef nodeFrom, NodeRef nodeTo);
    public IResultSnapshot RunAlgo(uint nodeFrom, uint nodeTo);
}