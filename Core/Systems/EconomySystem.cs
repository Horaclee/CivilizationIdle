using Core.Enums;
using Core.Models;

namespace Core.Systems;

public class EconomySystem
{
    public Dictionary<ResourceType, double> CalculateBuildingCost(Building building)
    {
        var newCosts = new Dictionary<ResourceType, double>();

        foreach (var cost in building.Definition.Costs)
        {
            var currentCost = cost.Value * Math.Pow(building.Definition.CostMultiplier, building.Count); 
            newCosts[cost.Key] = Math.Round(currentCost, 0,  MidpointRounding.AwayFromZero);
        }

        return newCosts;
    }

    public bool CanAfford(GameState state, double cost, ResourceType resource) => state.GetResources(resource) >= cost;
}