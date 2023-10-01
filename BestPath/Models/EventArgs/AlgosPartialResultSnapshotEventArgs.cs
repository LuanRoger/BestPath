namespace BestPath.Models.EventArgs;

public class AlgosPartialResultSnapshotEventArgs : System.EventArgs
{
    public required TimeSpan elapsedTime { get; init; }
}