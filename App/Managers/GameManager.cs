using Core.Enums;
using Core.Models;
using Core.Systems;
using Infrastructure.Persistence;

namespace App.Managers;

public class GameManager
{
    public GameState State { get; private set; }
    public event Action<GameState>? StateChanged;
    
    private readonly ProductionSystem _productionSystem;
    private readonly ResourceManager _resourceManager;
    private readonly BuildingManager _buildingManager;
    private readonly UpgradeManager _upgradeManager;
    private readonly SaveSystem _saveSystem;

    public GameManager(SaveSystem? saveSystem = null, ResourceManager? resourceManager = null,
        BuildingManager? buildingManager = null, 
        ProductionSystem? productionSystem = null,
        UpgradeManager? upgradeManager = null)
    {
        State = new GameState();
        
        _saveSystem = saveSystem ?? new SaveSystem();
        _productionSystem = productionSystem ?? new ProductionSystem();
        _resourceManager = resourceManager ?? new ResourceManager();
        _buildingManager = buildingManager ?? new BuildingManager();
        _upgradeManager = upgradeManager ?? new UpgradeManager();
    }

    public void StartGame()
    {
        State.Init();
        BuildingManager.InitBuildings(State);
        UpgradeManager.InitUpgrades(State);
        StateChanged?.Invoke(State);
    } 
        
    public GameState GetGameState() => State;

    public void UpdateGame()
    {
        _productionSystem.ApplyProduction(State);
    }
    
    public void SaveGame()
    {
        State.UpdateLastSaved();
        _saveSystem.Save(State);
    }
    public void LoadGame()
    {
        State = _saveSystem.Load();
        if (State.Buildings.Count == 0 && State.Upgrades.Count == 0)
        {
            StartGame();
            return;
        }
        StateChanged?.Invoke(State);
    }

    public void ResetGame()
    {
        State.Reset();
        StateChanged?.Invoke(State);
    }
    
    public bool BuyBuilding(BuildingType type) 
        => _buildingManager.BuyBuilding(State, type);
    
    public void GatherResource(ResourceType type, double amount) 
        => _resourceManager.AddResource(State, type, amount);

    public void BuyUpgrade(GameState state, int definitionId)
        => _upgradeManager.BuyUpgrade(state, definitionId);
    
}
