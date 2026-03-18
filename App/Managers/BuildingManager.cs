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
            if (def.UnlockStage > state.Stage) return false;
            
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

    public static void UnlockBuildingForStage(GameState state)
    {
        var unlocked = BuildingDefinitions.All
            .Where(def => def.UnlockStage <= state.Stage)
            .ToList();

        foreach (var def in unlocked)
        {
            if (state.Buildings.Any(b => b.Definition.Type == def.Type)) continue;
            state.Buildings.Add(new Building { Definition = def, Count = 0 });
        }
    }
    
    public static void InitBuildings(GameState state)
    {
        state.Buildings = new List<Building>();

        foreach (var def in BuildingDefinitions.All)
        {
            if (def.UnlockStage > state.Stage) continue;
            state.Buildings.Add(new Building
            {
                Definition = def,
                Count = 0
            });
        }
    }

    public static void SyncBuildingsForStage(GameState state)
    {
        var unlockedDefs = BuildingDefinitions.All
            .Where(def => def.UnlockStage <= state.Stage)
            .ToList();

        state.Buildings.RemoveAll(b => b.Definition.UnlockStage > state.Stage);

        foreach (var def in unlockedDefs)
        {
            if (state.Buildings.Any(b => b.Definition.Type == def.Type)) continue;
            state.Buildings.Add(new Building { Definition = def, Count = 0 });
        }
    }
}
