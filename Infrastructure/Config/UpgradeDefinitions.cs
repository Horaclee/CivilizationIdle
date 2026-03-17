using Core.Definitions;
using Core.Enums;

namespace Infrastructure.Config;

public static class UpgradeDefinitions
{
    public static readonly List<UpgradeDefinition> All = new()
    {
        new UpgradeDefinition
        {
            Id = 1,
            Name = "Axe",
            TargetBuilding = BuildingType.LumberMill,
            ProductionMultiplier = 2,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 10},
                {ResourceType.Stone, 20}
            }
        },
        new UpgradeDefinition
        {
            Id = 2,
            Name = "Pickaxe",
            TargetBuilding = BuildingType.Quarry,
            ProductionMultiplier = 2,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 10},
                {ResourceType.Stone, 20}
            }
        },
        new UpgradeDefinition
        {
            Id = 3,
            Name = "Hoe",
            TargetBuilding = BuildingType.Farm,
            ProductionMultiplier = 2,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 10},
                {ResourceType.Stone, 20}
            }
        }
    };
}