using Core.Enums;
using Core.Models;

namespace App.Managers;

public class ResourceManager
{
    public void AddResource(GameState state, ResourceType type, double amount)
    {
        var current = state.GetResources(type);
        state.SetResources(type, current + amount);
    }

    public void AddResources(GameState state, Dictionary<ResourceType, double> resources)
    {
        foreach (var (type, amount) in resources)
            AddResource(state, type, amount);
    }

    public void ConsumeResource(GameState state, ResourceType type, double amount)
    {
        var current = state.GetResources(type);
        state.SetResources(type, Math.Max(0, current - amount));
    }
    
    public double GetResource(GameState state, ResourceType type) => state.GetResources(type);
    public bool HasEnoughResource(GameState state, ResourceType type, double amount) => state.GetResources(type) >= amount;
}