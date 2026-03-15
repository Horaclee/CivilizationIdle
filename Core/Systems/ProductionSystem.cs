using Core.Models;

namespace Core.Systems;

public class ProductionSystem
{
    public void ApplyProduction(GameState state)
    {
        foreach (var building in state.Buildings)
        {
            if (building.Count <= 0 ) continue;
            
            foreach (var (resource, value) in building.Definition.Production)
            {
                if (value == 0) continue;
                
                var amount = value * building.Count;
                var current = state.GetResources(resource);
                state.SetResources(resource, current + amount);
            }
        }
    }
}