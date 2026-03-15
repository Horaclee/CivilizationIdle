using Core.Enums;

namespace Core.Models;

public class Upgrade
{
    public string Name { get; set; }
    public bool IsUnlocked { get; set; }
    public bool IsPurchased { get; set; }
    public double Cost { get; set; }

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