namespace BestPath;

public class GraphInputs
{
    private const string NY_COORDINATES = @".\inputs\ny\USA-ny-coordinates";
    private const string NY_DISTANCE = @".\inputs\ny\USA-ny-distance";
    
    private const string WEST_COORDINATES = @".\inputs\west\USA-west-coordinates";
    private const string WEST_DISTANCE = @".\inputs\west\USA-west-distance";
    
    private const string EAST_COORDINATES = @".\inputs\eastern\USA-east-coordinates";
    private const string EAST_DISTANCE = @".\inputs\eastern\USA-east-distance";
    
    private const string CENTER_COORDINATES = @".\inputs\center\USA-center-coordinates";
    private const string CENTER_DISTANCE = @".\inputs\center\USA-center-distance";
    
    public readonly string[] locations = {
        "New York",
        "West",
        "East",
        "Center"
    };
    
    public (string, string) GetLocationFiles(string location)
    {
        return location switch
        {
            "New York" => (NY_COORDINATES, NY_DISTANCE),
            "West" => (WEST_COORDINATES, WEST_DISTANCE),
            "East" => (EAST_COORDINATES, EAST_DISTANCE),
            "Center" => (CENTER_COORDINATES, CENTER_DISTANCE),
            _ => throw new ArgumentException("Invalid location")
        };
    }
}