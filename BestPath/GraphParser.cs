using BestPath.Algos.AStar;
using BestPath.Algos.Bfs;
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
        
        graph.AddNodeRange(await coordnatesTask);
        graph.AddEdgeRange(await distanceTask);
        
        return graph;
    }
    
    public async Task<UcsGraph> ParseToUcsGraph()
    {
        UcsGraph graph = new();
        
        var coordnatesTask = ParseCoordinatesAsUcsNodes();
        var distanceTask = ParseDistanceAsUcsEdges();
        
        await Task.WhenAll(coordnatesTask, distanceTask);
        
        graph.AddNodeRange(await coordnatesTask);
        graph.AddEdgeRange(await distanceTask);
        
        return graph;
    }
    
    public async Task<AStarGraph> ParseToAStarGraph()
    {
        AStarGraph graph = new();
        
        var coordnatesTask = ParseCoordinatesAsAStarNodes();
        var distanceTask = ParseDistanceAsAStarEdges();
        
        await Task.WhenAll(coordnatesTask, distanceTask);
        
        graph.AddNodeRange(await coordnatesTask);
        graph.AddEdgeRange(await distanceTask);
        
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
    
    private async Task<IEnumerable<AStarNode>> ParseCoordinatesAsAStarNodes()
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
            int x = int.Parse(lineData[2]);
            int y = int.Parse(lineData[3]);
            
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