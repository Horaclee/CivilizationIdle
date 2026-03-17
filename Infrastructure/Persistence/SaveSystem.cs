using System.Text.Json;
using Core.Definitions;
using Core.Models;
using System;
using System.IO;
using Infrastructure.Config;

namespace Infrastructure.Persistence;

public class SaveSystem
{
    private static readonly string SavePath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "CivilizationIdle/Infrastructure/Persistence",
        "save.json");
    
    private static void EnsureSaveDirectoryExists()
    {
        var directory = Path.GetDirectoryName(SavePath);
        if (string.IsNullOrWhiteSpace(directory)) return;
        Directory.CreateDirectory(directory);
    }

    public void Save(GameState state)
    {
        EnsureSaveDirectoryExists();
        var saveData = new SaveData
        {
            Resources = state.Resources,
            Buildings = state.Buildings.Select(b => new SaveData.BuildingSave
            {
                Type = b.Definition.Type,
                Count = b.Count
            }).ToList(),
            Upgrades = state.Upgrades.Select(u => new SaveData.UpgradeSave
            {
                Id = u.Definition.Id,
                IsPurchased = u.IsPurchased
            }).ToList()
        };
        
        var json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
        
        File.WriteAllText(SavePath, json);
    }

    public GameState Load()
    {
        if (!File.Exists(SavePath)) return new GameState();

        var json = File.ReadAllText(SavePath);
        var saveData = JsonSerializer.Deserialize<SaveData>(json);

        var state = new GameState();

        state.Resources = saveData.Resources;

        foreach (var b in saveData.Buildings)
        {
            var def = BuildingDefinitions.All.First(x => x.Type == b.Type);
            
            state.Buildings.Add(new Building
            {
                Definition = def,
                Count = b.Count
            });
        }

        foreach (var u in saveData.Upgrades)
        {
            var upgrade = UpgradeDefinitions.All.First(x => x.Id == u.Id);
            state.Upgrades.Add(new Upgrade
            {
                Definition = upgrade,
                IsPurchased = u.IsPurchased
            });
        }
        
        return state;
    }
}
