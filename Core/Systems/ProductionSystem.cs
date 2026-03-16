using Core.Enums;
using Core.Models;

namespace Core.Systems;

public class ProductionSystem
{
    public void ApplyProduction(GameState state)
    {
        var bonuses = CalculateProductionBonuses(state);
        
        foreach (var building in state.Buildings)
        {
            if (building.Count <= 0 ) continue;
            
            var bonus = bonuses.GetValueOrDefault(building.Definition.Type, 1.0);
            
            foreach (var (resource, value) in building.Definition.Production)
            {
                if (value == 0) continue;
                
                var amount = value * building.Count * bonus;
                var current = state.GetResources(resource);
                
                state.SetResources(resource, current + amount);
            }
        }
    }
    
    private Dictionary<BuildingType, double> CalculateProductionBonuses(GameState state)
    {
        var bonuses = new Dictionary<BuildingType, double>();

        foreach (var upgrade in state.Upgrades)
        {
            var target = upgrade.Definition.TargetBuilding;
            var multiplier = upgrade.Definition.ProductionMultiplier;

            if (!bonuses.ContainsKey(target))
                bonuses[target] = 1.0;

            bonuses[target] *= multiplier;
        }

        return bonuses;
    }
}