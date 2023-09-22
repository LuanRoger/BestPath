using BestPath.Algos.Dfs;
using BestPath.Graph.Base;

DfsGraph graph = new();

DfsNode node1 = new(1);
DfsNode node2 = new(2);
DfsNode node3 = new(3);
DfsNode node4 = new(4);
DfsNode node5 = new(5);
DfsNode node6 = new(6);
DfsNode node7 = new(7);
DfsNode node8 = new(8);

NodeRef node1Ref = graph.AddNode(node1);
NodeRef node2Ref = graph.AddNode(node2);
NodeRef node3Ref = graph.AddNode(node3);
NodeRef node4Ref = graph.AddNode(node4);
NodeRef node5Ref = graph.AddNode(node5);
NodeRef node6Ref = graph.AddNode(node6);
NodeRef node7Ref = graph.AddNode(node7);
NodeRef node8Ref = graph.AddNode(node8);

graph.AddEdge(new(node1Ref, node2Ref));
graph.AddEdge(new(node2Ref, node3Ref));
graph.AddEdge(new(node3Ref, node4Ref));
graph.AddEdge(new(node4Ref, node3Ref));
graph.AddEdge(new(node2Ref, node5Ref));
graph.AddEdge(new(node2Ref, node6Ref));
graph.AddEdge(new(node3Ref, node7Ref));
graph.AddEdge(new(node4Ref, node8Ref));
graph.AddEdge(new(node5Ref, node1Ref));
graph.AddEdge(new(node5Ref, node6Ref));
graph.AddEdge(new(node6Ref, node7Ref));
graph.AddEdge(new(node7Ref, node6Ref));
graph.AddEdge(new(node7Ref, node8Ref));
graph.AddEdge(new(node8Ref, node8Ref));

DfsResultSnapshot result = graph.RunAlgo(node1Ref);

Console.WriteLine(result);