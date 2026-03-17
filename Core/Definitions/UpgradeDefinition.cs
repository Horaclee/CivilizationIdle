using Core.Enums;

namespace Core.Definitions;

public class UpgradeDefinition
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public BuildingType TargetBuilding { get; init; }
    public double ProductionMultiplier { get; init; }
    public Dictionary<ResourceType, double> Costs { get; init; } = new();
}