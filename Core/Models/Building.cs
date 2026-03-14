using Core.Enums;

namespace Core.Models;

public class Building
{
    public BuildingType Type;
    public string Name;

    public int Count;
    
    public double BaseCost;
    public double CostMultiplier;

    public Dictionary<ResourceType, double> Production;

    public void GetCost()
    {
        
    }

    public void GetNextCost()
    {
        
    }

    public void IncreaseCount()
    {
        
    }
    
    public void GetProduction()
    {
        
    }
    
}