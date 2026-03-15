using Core.Definitions;
using Core.Enums;

namespace Infrastructure.Config;

public static class UpgradeDefinitions
{
    public static List<UpgradeDefinition> All = new()
    {
        new UpgradeDefinition
        {
            Name = "Axe",
            IsPurchased = false,
            TargetBuilding = BuildingType.LumberMill,
            ProductionMultiplier = 1.1,
            Costs = new Dictionary<ResourceType, double>
            {
                {ResourceType.Wood, 10},
                {ResourceType.Stone, 20}
            }
        }
    };
}