using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using App.Managers;
using Core.Enums;
using Core.Models;
using Core.Systems;

namespace WPF.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly GameManager _gameManager;
    private readonly DispatcherTimer _timer;
    private readonly GameState _gameState;
    private readonly Dictionary<Building, Button> _buildingsButtons = new();
    private readonly Dictionary<Upgrade, Button> _upgradeButtons = new();
    private readonly EconomySystem _economySystem = new EconomySystem();
    
    public MainWindow()
    {
        InitializeComponent();
        
        _gameManager = new GameManager();
        _gameState = _gameManager.GetGameState();
        _gameManager.StartGame();
        
        InitBuildingPanel(_gameState);
        InitUpgradePanel(_gameState);

        UpdateUi();
        
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (_, _) => GameTick();
        _timer.Start();
    }

    private void GameTick()
    {
        _gameManager.UpdateGame();
        UpdateUi();
    }

    private void UpdateUi()
    {
        UpdateResourcesText(_gameState);
        UpdateBuildingPanel(_gameState);
        UpdateUpgradePanel(_gameState);
        UpdateProductionPerSecond(_gameState);
    }
    
    private void UpdateResourcesText(GameState state)
    {
        FoodText.Text = $"Food: {state.GetResources(ResourceType.Food).ToString(CultureInfo.CurrentCulture)}";
        WoodText.Text =  $"Wood: {state.GetResources(ResourceType.Wood).ToString(CultureInfo.CurrentCulture)}";
        StoneText.Text = $"Stone: {state.GetResources(ResourceType.Stone).ToString(CultureInfo.CurrentCulture)}";
        GoldText.Text =  $"Gold: {state.GetResources(ResourceType.Gold).ToString(CultureInfo.CurrentCulture)}";
        PopulationText.Text = $"Population: {state.GetResources(ResourceType.Population).ToString(CultureInfo.CurrentCulture)}";
    }

    private void InitBuildingPanel(GameState state)
    {
        BuildingPanel.Children.Clear();
        _buildingsButtons.Clear();
        foreach (var building in state.Buildings)
        {
            var localBuilding = building;

            var btn = new Button
            {
                Content = $"{building.Definition.Name} ({building.Count}) - Costs : {GetBuildingCostText(building)}",
                Style = (Style)FindResource("BuildingButtonStyle"),
            };

            btn.Click += (_, _) =>
            {
                _gameManager.BuyBuilding(localBuilding.Definition.Type);
                UpdateUi();
            };
            BuildingPanel.Children.Add(btn);
            _buildingsButtons.Add(building, btn);
        }
        UpdateBuildingPanel(state);
    }

    private void UpdateBuildingPanel(GameState state)
    {
        foreach (var building in state.Buildings)
        {
            if (_buildingsButtons.TryGetValue(building, out var btn))
            {
                btn.Content = $"{building.Definition.Name} ({building.Count}) - Costs : {GetBuildingCostText(building)}";
            }
        }
    }

    private void InitUpgradePanel(GameState state)
    {
        UpgradesPanel.Children.Clear();
        _upgradeButtons.Clear();

        foreach (var upgrade in state.Upgrades.Where(u => !u.IsPurchased))
        {
            var localUpgrade = upgrade;
            
            var btn = new Button
            {
                Content = $"{upgrade.Definition.Name} - Costs : {localUpgrade.GetCostText()}",
                Style = (Style)FindResource("UpgradeButtonStyle"),
            };

            btn.Click += (_, _) =>
            {
                _gameManager.BuyUpgrade(state, localUpgrade.Definition.Id);
                UpdateUi();
            };

            UpgradesPanel.Children.Add(btn);
            _upgradeButtons.Add(upgrade, btn);
        }
        UpdateUpgradePanel(state);
    }

    private void UpdateUpgradePanel(GameState state)
    {
        foreach (var upgrade in state.Upgrades)
        {
            if (!_upgradeButtons.TryGetValue(upgrade, out var btn)) continue;
            
            btn.Content = $"{upgrade.Definition.Name} - Costs: {upgrade.GetCostText()}";
            btn.IsEnabled = !upgrade.IsPurchased;
        }
    }

    private string GetBuildingCostText(Building building)
    {
        var costs = _economySystem.CalculateBuildingCost(building);
        return string.Join(", ", costs.Select(kvp =>
            $"{kvp.Key}: {kvp.Value.ToString(CultureInfo.CurrentCulture)}"));
    }
    
    private void CollectFood_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        button?.IsEnabled = false;
        
        _gameManager.GatherResource(ResourceType.Food, 1);
        UpdateUi();

        button?.IsEnabled = true;
    }
    
    private void CollectWood_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        button?.IsEnabled = false;
        
        _gameManager.GatherResource(ResourceType.Wood, 1);
        UpdateUi();
        

        button?.IsEnabled = true;
    }
    
    private void UpdateProductionPerSecond(GameState state)
    {
        var productions = new List<string>();
        
        foreach (var building in state.Buildings)
        {
            if (building.Count == 0) continue;
            
            var bonus = 1.0;

            foreach (var upgrade in state.Upgrades)
            {
                if (upgrade.IsPurchased && upgrade.Definition.TargetBuilding == building.Definition.Type)
                    bonus*= upgrade.Definition.ProductionMultiplier;
            }
            
            foreach (var (resource, value) in building.Definition.Production)
            {
                if (value == 0) continue;
                
                var perSec = value * building.Count * bonus;
                productions.Add($"{resource}: {perSec:F1} / sec");
            }
        }
        
        ProductionPanel.ItemsSource = productions;
    }
}
