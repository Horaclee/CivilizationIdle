using Core.Enums;
using Core.Models;
using Core.Systems;
using Infrastructure.Config;

namespace App.Managers;

public class BuildingManager
{
    private readonly EconomySystem _economySystem = new EconomySystem();

    public bool BuyBuilding(GameState state, BuildingType type)
    {
        var building = state.Buildings.FirstOrDefault(b => b.Definition.Type == type);
        if (building == null)
        {
            var def = BuildingDefinitions.All.FirstOrDefault(b => b.Type == type);
            if (def == null) return false;
            
            building = new Building { Definition = def, Count = 0 };
            state.Buildings.Add(building);
        }

        var currentCost = _economySystem.CalculateBuildingCost(building);

        foreach (var cost in currentCost)
        {
            if (!state.Resources.ContainsKey(cost.Key) || state.Resources[cost.Key] < cost.Value)
                return false;
        }
        
        foreach (var cost in currentCost)
            state.Resources[cost.Key] -= cost.Value;
        
        building.Count++;
        
        return true;
    }
    
    
    public static void InitBuildings(GameState state)
    {
        state.Buildings = new List<Building>();

        foreach (var def in BuildingDefinitions.All)
        {
            state.Buildings.Add(new Building
            {
                Definition = def,
                Count = 0
            });
        }
    }
}