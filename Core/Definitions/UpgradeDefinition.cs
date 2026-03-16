using Core.Enums;

namespace Core.Definitions;

public class UpgradeDefinition
{
    public int Id { get; set; }
    public string Name { get; set; }
    public BuildingType TargetBuilding { get; set; }
    public double ProductionMultiplier { get; set; }
    public Dictionary<ResourceType, double> Costs { get; set; } = new();
}