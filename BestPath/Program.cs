using BestPath.Algos.Bfs;
using BestPath.Algos.Dfs;
using BestPath.Graph.Base;

Console.WriteLine("BFS:");
RunBfs();
Console.WriteLine("========================================");
Console.WriteLine("DFS:");
RunDfs();

void RunBfs()
{
    BfsGraph graph = new();

    BfsNode node0 = new(0);
    BfsNode node1 = new(1);
    BfsNode node2 = new(2);
    BfsNode node3 = new(3);
    BfsNode node4 = new(4);
    BfsNode node5 = new(5);
    BfsNode node6 = new(6);
    BfsNode node7 = new(7);

    NodeRef node0Ref = graph.AddNode(node0);
    NodeRef node1Ref = graph.AddNode(node1);
    NodeRef node2Ref = graph.AddNode(node2);
    NodeRef node3Ref = graph.AddNode(node3);
    NodeRef node4Ref = graph.AddNode(node4);
    NodeRef node5Ref = graph.AddNode(node5);
    NodeRef node6Ref = graph.AddNode(node6);
    NodeRef node7Ref = graph.AddNode(node7);

    graph.AddEdge(new(node0Ref, node1Ref));
    graph.AddEdge(new(node1Ref, node3Ref));
    graph.AddEdge(new(node2Ref, node1Ref));
    graph.AddEdge(new(node3Ref, node2Ref));
    graph.AddEdge(new(node3Ref, node4Ref));
    graph.AddEdge(new(node4Ref, node5Ref));
    graph.AddEdge(new(node5Ref, node7Ref));
    graph.AddEdge(new(node7Ref, node6Ref));
    graph.AddEdge(new(node6Ref, node4Ref));

    BfsResultSnapshot result = graph.RunAlgo(node1Ref, node6Ref);


    Console.WriteLine(result);
}

void RunDfs()
{
    DfsGraph graph = new();

    DfsNode node0 = new(0);
    DfsNode node1 = new(1);
    DfsNode node2 = new(2);
    DfsNode node3 = new(3);
    DfsNode node4 = new(4);
    DfsNode node5 = new(5);
    DfsNode node6 = new(6);
    DfsNode node7 = new(7);

    NodeRef node0Ref = graph.AddNode(node0);
    NodeRef node1Ref = graph.AddNode(node1);
    NodeRef node2Ref = graph.AddNode(node2);
    NodeRef node3Ref = graph.AddNode(node3);
    NodeRef node4Ref = graph.AddNode(node4);
    NodeRef node5Ref = graph.AddNode(node5);
    NodeRef node6Ref = graph.AddNode(node6);
    NodeRef node7Ref = graph.AddNode(node7);

    graph.AddEdge(new(node0Ref, node1Ref));
    graph.AddEdge(new(node1Ref, node3Ref));
    graph.AddEdge(new(node2Ref, node1Ref));
    graph.AddEdge(new(node3Ref, node2Ref));
    graph.AddEdge(new(node3Ref, node4Ref));
    graph.AddEdge(new(node4Ref, node5Ref));
    graph.AddEdge(new(node5Ref, node7Ref));
    graph.AddEdge(new(node7Ref, node6Ref));
    graph.AddEdge(new(node6Ref, node4Ref));

    DfsResultSnapshot result = graph.RunAlgo(node1Ref, node6Ref);

    Console.WriteLine(result);
}