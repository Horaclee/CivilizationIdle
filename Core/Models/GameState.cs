using Core.Definitions;
using Core.Enums;

namespace Core.Models;

public class GameState
{
    public Dictionary<ResourceType, double> Resources { get; set; }

    public List<Building> Buildings { get; set; }
    public List<Upgrade> Upgrades { get; set; }
    public CivilizationStage Stage { get; set; }

    public DateTime LastSavedUtc { get; set; }

    public GameState()
    {
        Init();
    }

    public void Init()
    {
        Resources = new Dictionary<ResourceType, double>()
        {
            { ResourceType.Food, 0 },
            { ResourceType.Wood, 0 },
            { ResourceType.Stone, 0 },
            { ResourceType.Gold, 0 },
            { ResourceType.Population, 0 },
        };

        Stage = CivilizationStage.Tribe;

        Buildings = new List<Building>();
        Upgrades = new List<Upgrade>();

        LastSavedUtc = DateTime.UtcNow;
    }

    public void Reset()
    {
        Init();
    }

    public double GetResources(ResourceType type) => Resources[type];
    
    public void SetResources(ResourceType type, double amount) => Resources[type] = amount;
    
    public void UpdateLastSaved()
    {
        LastSavedUtc = DateTime.UtcNow;
    }
}
