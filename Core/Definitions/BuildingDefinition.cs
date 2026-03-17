using Core.Enums;

namespace Core.Definitions;

public class BuildingDefinition
{
    public required string Name { get; init; }
    public BuildingType Type { get; init; }
    
    public Dictionary<ResourceType, double> Costs { get; init; } = new();
    public double CostMultiplier { get; init; }
    public required Dictionary<ResourceType, double> Production { get; init; }
}