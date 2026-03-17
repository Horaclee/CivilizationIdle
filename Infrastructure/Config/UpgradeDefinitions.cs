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
            Name = "Iron Plow",
            TargetBuilding = BuildingType.Farm,
            ProductionMultiplier = 1.5,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 40},
                {ResourceType.Stone, 30}
            }
        },
        new UpgradeDefinition
        {
            Id = 2,
            Name = "Granaries",
            TargetBuilding = BuildingType.Farm,
            ProductionMultiplier = 1.25,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 120},
                {ResourceType.Stone, 80},
                {ResourceType.Gold, 25}
            }
        },
        new UpgradeDefinition
        {
            Id = 3,
            Name = "Sharpened Axe",
            TargetBuilding = BuildingType.LumberMill,
            ProductionMultiplier = 1.5,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 60},
                {ResourceType.Stone, 40}
            }
        },
        new UpgradeDefinition
        {
            Id = 4,
            Name = "Sawmill Blades",
            TargetBuilding = BuildingType.LumberMill,
            ProductionMultiplier = 1.25,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 140},
                {ResourceType.Stone, 90},
                {ResourceType.Gold, 30}
            }
        },
        new UpgradeDefinition
        {
            Id = 5,
            Name = "Steel Pickaxe",
            TargetBuilding = BuildingType.Quarry,
            ProductionMultiplier = 1.5,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 50},
                {ResourceType.Stone, 80}
            }
        },
        new UpgradeDefinition
        {
            Id = 6,
            Name = "Reinforced Drills",
            TargetBuilding = BuildingType.Quarry,
            ProductionMultiplier = 1.25,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 130},
                {ResourceType.Stone, 140},
                {ResourceType.Gold, 35}
            }
        },
        new UpgradeDefinition
        {
            Id = 7,
            Name = "Market Stall Licenses",
            TargetBuilding = BuildingType.Market,
            ProductionMultiplier = 1.4,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 150},
                {ResourceType.Wood, 120},
                {ResourceType.Stone, 120},
                {ResourceType.Gold, 50}
            }
        },
        new UpgradeDefinition
        {
            Id = 8,
            Name = "Merchant Guild",
            TargetBuilding = BuildingType.Market,
            ProductionMultiplier = 1.25,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 220},
                {ResourceType.Wood, 200},
                {ResourceType.Stone, 180},
                {ResourceType.Gold, 90}
            }
        },
        new UpgradeDefinition
        {
            Id = 9,
            Name = "Housing Plan",
            TargetBuilding = BuildingType.House,
            ProductionMultiplier = 1.3,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 180},
                {ResourceType.Wood, 200},
                {ResourceType.Stone, 160},
                {ResourceType.Gold, 40}
            }
        },
        new UpgradeDefinition
        {
            Id = 10,
            Name = "Community Wells",
            TargetBuilding = BuildingType.House,
            ProductionMultiplier = 1.2,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 260},
                {ResourceType.Wood, 220},
                {ResourceType.Stone, 240},
                {ResourceType.Gold, 75}
            }
        }
    };
}
