using Core.Enums;
using Core.Models;
using Core.Systems;

namespace App.Managers;

public class GameManager
{
    public GameState State { get; private set; }
    
    private readonly ProductionSystem _productionSystem;
    private readonly ResourceManager _resourceManager;
    private readonly BuildingManager _buildingManager;

    public GameManager(
        ResourceManager? resourceManager = null,
        BuildingManager? buildingManager = null, 
        ProductionSystem? productionSystem = null)
    {
        State = new GameState();
        
        _productionSystem = productionSystem ?? new ProductionSystem();
        _resourceManager = resourceManager ?? new ResourceManager();
        _buildingManager = buildingManager ?? new BuildingManager();
    }

    public void StartGame()
    {
        State.Init();
        BuildingManager.InitBuildings(State);
    } 
        
    public GameState GetGameState() => State;

    public void UpdateGame()
    {
        _productionSystem.ApplyProduction(State);
    }

    public void ResetGame() => State.Reset();
    
    public bool BuyBuilding(BuildingType type) 
        => _buildingManager.BuyBuilding(State, type);
    
    
    public void GatherResource(ResourceType type, double amount) 
        => _resourceManager.AddResource(State, type, amount);
}