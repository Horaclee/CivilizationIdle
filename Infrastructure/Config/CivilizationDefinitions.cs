using Core.Definitions;
using Core.Enums;
using Core.Models;

namespace Infrastructure.Config;

public static class CivilizationDefinitions
{
    public static List<CivilizationDefinition> All = new()
    {
        new CivilizationDefinition
        {
            Stage = CivilizationStage.Tribe,
            Name = "Stone Age",
            UnlockCondition = s => true,
            ProductionMultiplier = 1.0
        },
        new CivilizationDefinition
        {
            Stage = CivilizationStage.Village,
            Name = "Agriculture Age",
            UnlockCondition = s => s.GetResources(ResourceType.Food) >= 100,
            ProductionMultiplier = 1.2
        },
        new CivilizationDefinition
        {
            Stage = CivilizationStage.Town,
            Name = "Antiquity",
            UnlockCondition = s => s.Buildings.Any(b => b.Count >= 61),
            ProductionMultiplier = 2
        }
    };
}