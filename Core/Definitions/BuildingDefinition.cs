using Core.Enums;

namespace Core.Definitions;

public class BuildingDefinition
{
    public required string Name { get; init; }
    public BuildingType Type { get; init; }
    public required IReadOnlyDictionary<ResourceType, double> Costs { get; init; }
    public double CostMultiplier { get; init; }
    public required IReadOnlyDictionary<ResourceType, double> Production { get; init; }
}