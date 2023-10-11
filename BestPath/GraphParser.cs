using BestPath.Algos.AStar;
using BestPath.Algos.Bfs;
using BestPath.Algos.Dfs;
using BestPath.Algos.Ucs;
using BestPath.Models;

namespace BestPath;

public class GraphParser
{
    public string coordinatesFilePath { get; }
    public string distanceFilePath { get; }
    
    private const char COORDINATES_INDICATOR = 'v';
    private const char DISTANCE_INDICATOR = 'a';

    public GraphParser(string coordinatesFilePath, string distanceFilePath)
    {
        this.coordinatesFilePath = coordinatesFilePath;
        this.distanceFilePath = distanceFilePath;
    }
    
    public async Task<BfsGraph> ParseToBfsGraph()
    {
        BfsGraph graph = new();
        
        var coordnatesTask = ParseCoordinatesAsBfsNodes();
        var distanceTask = ParseDistanceAsBfsEdges();
        
        await Task.WhenAll(coordnatesTask, distanceTask);
        
        graph.AddNodeRange(coordnatesTask.Result);
        graph.AddEdgeRange(distanceTask.Result);
        
        return graph;
    }
    
    public async Task<DfsGraph> ParseToDfsGraph()
    {
        DfsGraph graph = new();
        
        var coordnatesTask = ParseCoordinatesAsDfsNodes();
        var distanceTask = ParseDistanceAsDfsEdges();
        
        await Task.WhenAll(coordnatesTask, distanceTask);
        
        graph.AddNodeRange(coordnatesTask.Result);
        graph.AddEdgeRange(distanceTask.Result);
        
        return graph;
    }
    
    public async Task<UcsGraph> ParseToUcsGraph()
    {
        UcsGraph graph = new();
        
        var coordnatesTask = ParseCoordinatesAsUcsNodes();
        var distanceTask = ParseDistanceAsUcsEdges();
        
        await Task.WhenAll(coordnatesTask, distanceTask);
        
        graph.AddNodeRange(coordnatesTask.Result);
        graph.AddEdgeRange(distanceTask.Result);
        
        return graph;
    }
    
    public async Task<AStarGraph> ParseToAStarGraph(bool divideByMillion = true)
    {
        AStarGraph graph = new();
        
        var coordnatesTask = ParseCoordinatesAsAStarNodes(divideByMillion);
        var distanceTask = ParseDistanceAsAStarEdges();
        
        await Task.WhenAll(coordnatesTask, distanceTask);
        
        graph.AddNodeRange(coordnatesTask.Result);
        graph.AddEdgeRange(distanceTask.Result);
        
        return graph;
    }
    
    private async Task<IEnumerable<BfsNode>> ParseCoordinatesAsBfsNodes()
    {
        List<BfsNode> nodes = new();
        
        await using FileStream fileStream = File.OpenRead(coordinatesFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(COORDINATES_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint id = uint.Parse(lineData[1]);
            
            nodes.Add(new(id));
        }
        
        return nodes;
    }
    
    private async Task<IEnumerable<DfsNode>> ParseCoordinatesAsDfsNodes()
    {
        List<DfsNode> nodes = new();
        
        await using FileStream fileStream = File.OpenRead(coordinatesFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(COORDINATES_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint id = uint.Parse(lineData[1]);
            
            nodes.Add(new(id));
        }
        
        return nodes;
    }
    
    private async Task<IEnumerable<UcsNode>> ParseCoordinatesAsUcsNodes()
    {
        List<UcsNode> nodes = new();
        
        await using FileStream fileStream = File.OpenRead(coordinatesFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(COORDINATES_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint id = uint.Parse(lineData[1]);
            
            nodes.Add(new(id));
        }
        
        return nodes;
    }
    
    private async Task<IEnumerable<AStarNode>> ParseCoordinatesAsAStarNodes(bool divideByMillion = true)
    {
        List<AStarNode> nodes = new();
        
        await using FileStream fileStream = File.OpenRead(coordinatesFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(COORDINATES_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint id = uint.Parse(lineData[1]);
            float x = divideByMillion ? float.Parse(lineData[3]) / 1000000 : float.Parse(lineData[3]);
            float y = divideByMillion ? float.Parse(lineData[2]) / 1000000 : float.Parse(lineData[2]);
            
            nodes.Add(new(id, new Coordinate(x, y)));
        }
        
        return nodes;
    }
    
    private async Task<IEnumerable<BfsEdge>> ParseDistanceAsBfsEdges()
    {
        List<BfsEdge> edges = new();
        
        await using FileStream fileStream = File.OpenRead(distanceFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(DISTANCE_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint fromId = uint.Parse(lineData[1]);
            uint toId = uint.Parse(lineData[2]);
            uint weight = uint.Parse(lineData[3]);
            
            edges.Add(new(fromId, toId, weight));
        }
        
        return edges;
    }
    
    private async Task<IEnumerable<DfsEdge>> ParseDistanceAsDfsEdges()
    {
        List<DfsEdge> edges = new();
        
        await using FileStream fileStream = File.OpenRead(distanceFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(DISTANCE_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint fromId = uint.Parse(lineData[1]);
            uint toId = uint.Parse(lineData[2]);
            uint weight = uint.Parse(lineData[3]);
            
            edges.Add(new(fromId, toId, weight));
        }
        
        return edges;
    }
    
    private async Task<IEnumerable<UcsEdge>> ParseDistanceAsUcsEdges()
    {
        List<UcsEdge> edges = new();
        
        await using FileStream fileStream = File.OpenRead(distanceFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(DISTANCE_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint fromId = uint.Parse(lineData[1]);
            uint toId = uint.Parse(lineData[2]);
            uint weight = uint.Parse(lineData[3]);
            
            edges.Add(new(fromId, toId, weight));
        }
        
        return edges;
    }
    
    private async Task<IEnumerable<AStarEdge>> ParseDistanceAsAStarEdges()
    {
        List<AStarEdge> edges = new();
        
        await using FileStream fileStream = File.OpenRead(distanceFilePath);
        await using BufferedStream bufferedStream = new(fileStream);
        using StreamReader streamReader = new(bufferedStream);

        while (true)
        {
            string? line = await streamReader.ReadLineAsync();
            if (line is null) break;
            if (!line.StartsWith(DISTANCE_INDICATOR)) continue;
            
            string[] lineData = line.Split(" ");
            uint fromId = uint.Parse(lineData[1]);
            uint toId = uint.Parse(lineData[2]);
            uint weight = uint.Parse(lineData[3]);
            
            edges.Add(new(fromId, toId, weight));
        }
        
        return edges;
    }
}