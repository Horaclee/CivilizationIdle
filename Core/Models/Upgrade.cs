using Core.Enums;

namespace Core.Models;

public class Upgrade
{
    public string Name;

    public bool IsUnlocked;
    public bool IsPurchased;

    public double Cost;

    public BuildingType TargetBuilding;
    
    public double ProductionMultiplier;

    public void Unlock()
    {
        
    }

    public void Purchase()
    {
        
    }

    public void ApplyEffect()
    {
        
    }
}