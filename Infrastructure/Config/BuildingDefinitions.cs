using Core.Definitions;
using Core.Enums;

namespace Infrastructure.Config;

public static class BuildingDefinitions
{
    public static readonly List<BuildingDefinition> All = new()
    {
        new BuildingDefinition
        {
            Name = "Farm",
            Type = BuildingType.Farm,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 10},
                {ResourceType.Wood, 20}
            },
            CostMultiplier = 1.10,
            Production = new Dictionary<ResourceType, double>
            {
                { ResourceType.Food, 2 }
            }
        },

        new BuildingDefinition
        {
            Name = "Lumber Mill",
            Type = BuildingType.LumberMill,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 20},
                {ResourceType.Wood, 50}
            },
            CostMultiplier = 1.12,
            Production = new Dictionary<ResourceType, double>
            {
                { ResourceType.Wood, 3 }
            }
        },

        new BuildingDefinition
        {
            Name = "Quarry",
            Type = BuildingType.Quarry,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 50}, 
                {ResourceType.Wood, 50}
            },
            CostMultiplier = 1.15,
            Production = new Dictionary<ResourceType, double>
            {
                { ResourceType.Stone, 4 }
            }
        }, 
        
        new BuildingDefinition
        {
            Name = "Market",
            Type = BuildingType.Market,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 150}, 
                {ResourceType.Wood, 150}, 
                {ResourceType.Stone, 100}
            },
            CostMultiplier = 1.20,
            Production = new Dictionary<ResourceType, double>
            {
                { ResourceType.Gold, 2 }
            }
        },
        
        new BuildingDefinition
        {
            Name = "House",
            Type = BuildingType.House,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Food, 100}, 
                {ResourceType.Wood, 100}, 
                {ResourceType.Stone, 100}, 
                {ResourceType.Gold, 20}
            },
            CostMultiplier = 1.30,
            Production = new Dictionary<ResourceType, double>
            {
                { ResourceType.Population, 1 }
            }
        }
    };
}