using Core.Enums;

namespace Core.Models;

public class GameState
{
    public Dictionary<ResourceType, double> Resources;
    public int Population { get; set; }
    public CivilizationStage Stage { get; set; }

    public List<Building> Buildings;
    public List<Upgrade> Upgrades;

    private DateTime LastSaved;

    private void Init()
    {
        
    }

    private void Reset()
    {
        
    }

    public void GetResources(ResourceType resource)
    {
        
    }
    
    public void SetResources(ResourceType resource, double amount)
    {
        
    }
    
    public void UpdateLastSaved()
    {
        LastSaved = DateTime.Now;
    }
}