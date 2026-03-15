using Core.Enums;

namespace Core.Definitions;

public class BuildingDefinition
{
    public BuildingType Type { get; set; }
    public string Name { get; set; }
    
    public Dictionary<ResourceType, double> Costs { get; set; } = new();
    public double CostMultiplier { get; set; }
    public Dictionary<ResourceType, double> Production { get; set; }
}