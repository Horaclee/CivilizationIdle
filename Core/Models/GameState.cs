using Core.Enums;

namespace Core.Models;

public class GameState
{
    public Dictionary<ResourceType, double> Resources { get; set; }

    public List<Building> Buildings { get; set; }
    public List<Upgrade> Upgrades { get; set; }

    public CivilizationStage Stage { get; set; }

    private DateTime LastSaved { get; set; }

    public GameState()
    {
        Init();
    }

    public void Init()
    {
        Resources = new Dictionary<ResourceType, double>()
        {
            { ResourceType.Food, 100 },
            { ResourceType.Wood, 100 },
            { ResourceType.Stone, 100 },
            { ResourceType.Gold, 100 },
            { ResourceType.Population, 100 },
        };

        Stage = CivilizationStage.Tribe;

        Buildings = new List<Building>();
        
        Upgrades = new List<Upgrade>();
        
        LastSaved = DateTime.Now;
    }

    public void Reset()
    {
        Init();
    }

    public double GetResources(ResourceType type) => Resources[type];
    
    public void SetResources(ResourceType type, double amount) => Resources[type] = amount;
    
    public void UpdateLastSaved()
    {
        LastSaved = DateTime.Now;
    }
}