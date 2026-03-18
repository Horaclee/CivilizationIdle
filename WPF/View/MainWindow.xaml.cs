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
    private readonly DispatcherTimer _autoSaveTimer;
    private GameState _gameState;
    private readonly Dictionary<Building, Button> _buildingsButtons = new();
    private readonly Dictionary<Upgrade, Button> _upgradeButtons = new();
    private readonly EconomySystem _economySystem = new EconomySystem();
    
    public MainWindow()
        : this(loadGame: true)
    {
    }

    public MainWindow(bool loadGame)
    {
        InitializeComponent();
        
        _gameManager = new GameManager();
        _gameManager.StateChanged += OnStateChanged;
        if (loadGame)
            _gameManager.LoadGame();
        else
            _gameManager.StartGame();

        Closing += (_, _) => _gameManager.SaveGame();
        
        _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _timer.Tick += (_, _) => GameTick();
        _timer.Start();

        _autoSaveTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
        _autoSaveTimer.Tick += (_, _) => _gameManager.SaveGame();
        _autoSaveTimer.Start();
    }

    private void OnStateChanged(GameState state)
    {
        _gameState = state;
        InitBuildingPanel(_gameState);
        InitUpgradePanel(_gameState);
        UpdateUi();
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
        UpdateCivilizationText(_gameState);
    }
    
    private void UpdateResourcesText(GameState state)
    {
        FoodText.Text = $"Food: {FormatCompact(state.GetResources(ResourceType.Food))}";
        WoodText.Text =  $"Wood: {FormatCompact(state.GetResources(ResourceType.Wood))}";
        StoneText.Text = $"Stone: {FormatCompact(state.GetResources(ResourceType.Stone))}";
        GoldText.Text =  $"Gold: {FormatCompact(state.GetResources(ResourceType.Gold))}";
        PopulationText.Text = $"Population: {FormatCompact(state.GetResources(ResourceType.Population))}";
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
                Content = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Children =
                    {
                        new TextBlock { Text = $"{building.Definition.Name} ({building.Count})" },
                        new TextBlock { Text = $"Costs: {GetBuildingCostText(building)}", Opacity = 0.8 }
                    }
                },
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
            if (!_buildingsButtons.TryGetValue(building, out var btn))
            {
                var localBuilding = building;
                btn = new Button
                {
                    Content = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Children =
                        {
                            new TextBlock { Text = $"{building.Definition.Name} ({building.Count})" },
                            new TextBlock { Text = $"Costs: {GetBuildingCostText(building)}" }
                        }
                    },
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

            if (btn.Content is StackPanel panel && panel.Children.Count >= 2)
            {
                if (panel.Children[0] is TextBlock nameText)
                    nameText.Text = $"{building.Definition.Name} ({building.Count})";
                if (panel.Children[1] is TextBlock costText)
                    costText.Text = $"Costs: {GetBuildingCostText(building)}";
            }
        }
    }

    private void InitUpgradePanel(GameState state)
    {
        UpgradesPanel.Children.Clear();
        _upgradeButtons.Clear();

        foreach (var upgrade in state.Upgrades)
        {
            var localUpgrade = upgrade;
            
            var btn = new Button
            {
                Content = new StackPanel
                {
                    Orientation = Orientation.Vertical,
                    Children =
                    {
                        new TextBlock { Text = upgrade.Definition.Name },
                        new TextBlock { Text = $"Costs: {localUpgrade.GetCostText()}", Opacity = 0.8 }
                    }
                },
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
            
            if (btn.Content is StackPanel panel && panel.Children.Count >= 2)
            {
                if (panel.Children[0] is TextBlock nameText)
                    nameText.Text = upgrade.Definition.Name;
                if (panel.Children[1] is TextBlock costText)
                    costText.Text = $"Costs: {upgrade.GetCostText()}";
            }
            btn.IsEnabled = !upgrade.IsPurchased;
        }
    }

    private string GetBuildingCostText(Building building)
    {
        var costs = _economySystem.CalculateBuildingCost(building);
        return string.Join(", ", costs.Select(kvp =>
            $"{kvp.Key}: {FormatCompact(kvp.Value)}"));
    }

    private static string FormatCompact(double value)
    {
        var abs = Math.Abs(value);
        var culture = CultureInfo.CurrentCulture;

        if (abs < 1000)
            return value.ToString("N0", culture);

        string[] suffixes = ["K", "M", "B", "T"];
        var divisor = 1000.0;
        var suffixIndex = -1;

        while (abs >= divisor && suffixIndex < suffixes.Length - 1)
        {
            divisor *= 1000.0;
            suffixIndex++;
        }

        var scaled = value / (divisor / 1000.0);
        return $"{scaled.ToString("0.#", culture)}{suffixes[suffixIndex]}";
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
                
                var perSec = value * building.Count * bonus * _gameManager.GetCurrentMultiplier();
                productions.Add($"{resource}: {perSec:F1} / sec");
            }
        }
        
        ProductionPanel.ItemsSource = productions;
    }

    public void UpdateCivilizationText(GameState state)
    {
        if (state.Stage.ToString() != Civilization.Text) Civilization.Text = state.Stage.ToString(); 
    }
}
