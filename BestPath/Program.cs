using BestPath;
using BestPath.Algos.AStar;
using BestPath.Algos.Bfs;
using BestPath.Algos.Ucs;
using BestPath.Graph.Base;

Console.WriteLine("Lendo grafo...");
var aStarParseTask = ReadAStarGraph();
var bfsParseTask = ReadBfsGraph();
var ucsParseTask = ReadUcsGraph();
await Task.WhenAll(aStarParseTask, ucsParseTask);
Console.WriteLine("Grafo lido!");

AStarGraph aStarGraph = aStarParseTask.Result; 
BfsGraph bfsGraph = bfsParseTask.Result;
UcsGraph ucsGraph = ucsParseTask.Result;
GC.Collect();

NodeRef nodeFromAStar = aStarGraph.GetSomeNodeRef(89);
NodeRef nodeToAStar = aStarGraph.GetSomeNodeRef(27);
NodeRef nodeFromBfs = bfsGraph.GetSomeNodeRef(89);
NodeRef nodeToBfs = bfsGraph.GetSomeNodeRef(27);
NodeRef nodeFromUcs = ucsGraph.GetSomeNodeRef(89);
NodeRef nodeToUcs = ucsGraph.GetSomeNodeRef(27);


Console.WriteLine("Executando algoritmos...");
var bfsAlgo = Task.Run(() => bfsGraph.RunAlgo(nodeFromBfs, nodeToBfs));
var ucsAlgo = Task.Run(() => ucsGraph.RunAlgo(nodeFromUcs, nodeToUcs));
var aStarAlgo = Task.Run(() => aStarGraph.RunAlgo(nodeFromAStar, nodeToAStar));
await Task.WhenAll(aStarAlgo, ucsAlgo);
Console.WriteLine("Algoritmos executados!");

Console.WriteLine("Resultado do A*: ");
Console.WriteLine(aStarAlgo.Result);
Console.WriteLine("Resultado do BFS: ");
Console.WriteLine(bfsAlgo.Result);
Console.WriteLine("Resultado UCS: ");
Console.WriteLine(ucsAlgo.Result);

return;

async Task<BfsGraph> ReadBfsGraph()
{
    GraphParser graphParser = new(@"C:\\Users\\luanr\\Documents\\Jobs\\c_sharp\\BestPath\\BestPath\\inputs\\ny\\USA-ny-coordinates", @"C:\\Users\\luanr\\Documents\\Jobs\\c_sharp\\BestPath\\BestPath\\inputs\\ny\\USA-ny-distance");
    return await graphParser.ParseToBfsGraph();
}

async Task<AStarGraph> ReadAStarGraph()
{
    GraphParser graphParser = new(@"C:\\Users\\luanr\\Documents\\Jobs\\c_sharp\\BestPath\\BestPath\\inputs\\ny\\USA-ny-coordinates", @"C:\\Users\\luanr\\Documents\\Jobs\\c_sharp\\BestPath\\BestPath\\inputs\\ny\\USA-ny-distance");
    return await graphParser.ParseToAStarGraph();
}

async Task<UcsGraph> ReadUcsGraph()
{
    GraphParser graphParser = new(@"C:\\Users\\luanr\\Documents\\Jobs\\c_sharp\\BestPath\\BestPath\\inputs\\ny\\USA-ny-coordinates", @"C:\\Users\\luanr\\Documents\\Jobs\\c_sharp\\BestPath\\BestPath\\inputs\\ny\\USA-ny-distance");
    return await graphParser.ParseToUcsGraph();
}