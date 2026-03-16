using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using App.Managers;
using Core.Enums;
using Core.Models;

namespace WPF.View;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private GameManager _gameManager;
    private DispatcherTimer _timer;
    
    public MainWindow()
    {
        InitializeComponent();
        
        _gameManager = new GameManager();
        _gameManager.StartGame();

        UpdateUi();
        
        _timer = new DispatcherTimer();
        _timer.Interval = TimeSpan.FromSeconds(1);
        _timer.Tick += (s, e) => GameTick();
        _timer.Start();
    }

    private void GameTick()
    {
        _gameManager.UpdateGame();
        UpdateUi();
    }

    private void UpdateUi()
    {
        var state = _gameManager.GetGameState();

        FoodText.Text = $"Food: {state.GetResources(ResourceType.Food).ToString(CultureInfo.CurrentCulture)}";
        WoodText.Text =  $"Wood: {state.GetResources(ResourceType.Wood).ToString(CultureInfo.CurrentCulture)}";
        StoneText.Text = $"Stone: {state.GetResources(ResourceType.Stone).ToString(CultureInfo.CurrentCulture)}";
        GoldText.Text =  $"Gold: {state.GetResources(ResourceType.Gold).ToString(CultureInfo.CurrentCulture)}";
        PopulationText.Text = $"Population: {state.GetResources(ResourceType.Population).ToString(CultureInfo.CurrentCulture)}";
        
        AddBuildingPanel(state);
        AddUpgradePanel(state);
    }

    private void AddBuildingPanel(GameState state)
    {
        BuildingPanel.Children.Clear();
        foreach (var building in state.Buildings)
        {
            string GetCostText()
            {
                var costs = building.Definition.Costs
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => Math.Round(kvp.Value * Math.Pow(building.Definition.CostMultiplier, building.Count), 0, MidpointRounding.AwayFromZero)
                    );
                return string.Join(", ", costs.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
            }
            
            var btn = new Button
            {
                Content = $"{building.Definition.Name} ({building.Count}) - Costs : {GetCostText()}",
                FontSize = 16,
                Padding = new Thickness(10, 5, 10, 5),
                Margin = new Thickness(5),
                Background = Brushes.DarkSlateGray,
                Foreground = Brushes.White,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            
            btn.Click += (s, e) =>
            {
                _gameManager.BuyBuilding(building.Definition.Type);
                UpdateUi();
            };
            BuildingPanel.Children.Add(btn);
        }
    }

    private void AddUpgradePanel(GameState state)
    {
        UpgradesPanel.Children.Clear();

        foreach (var upgrade in state.Upgrades.Where(u => !u.IsPurchased))
        {
            var btn = new Button
            {
                Content = $"{upgrade.Definition.Name} - Costs : {GetCostText()}",
                FontSize = 16,
                Padding = new Thickness(10, 5, 10, 5),
                Margin = new Thickness(5),
                Background = Brushes.DarkGoldenrod,
                Foreground = Brushes.White,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };

            btn.Click += (s, e) =>
            {
                _gameManager.BuyUpgrade(state, upgrade.Definition.Id);
                UpdateUi();
            };

            UpgradesPanel.Children.Add(btn);
            continue;

            string GetCostText()
            {
                return string.Join(", ",
                    upgrade.Definition.Costs.Select(kvp => $"{kvp.Key}: {kvp.Value}")
                );
            }
        }
    }
    
    private async void CollectFood_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        button?.IsEnabled = false;
        
        _gameManager.GatherResource(ResourceType.Food, 1);
        UpdateUi();
        
        await Task.Delay(1000);

        button?.IsEnabled = true;
    }
    private async void CollectWood_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;

        button?.IsEnabled = false;
        
        _gameManager.GatherResource(ResourceType.Wood, 1);
        UpdateUi();
        
        await Task.Delay(1000);

        button?.IsEnabled = true;
    }
    /*private void CollectStone_Click(object sender, RoutedEventArgs e)
    {
        _gameManager.GatherResource(ResourceType.Stone, 1);
        UpdateUi();
    }
    private void CollectGold_Click(object sender, RoutedEventArgs e)
    {
        _gameManager.GatherResource(ResourceType.Gold, 1);
        UpdateUi();
    }*/
}