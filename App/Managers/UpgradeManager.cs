using Core.Models;
using Infrastructure.Config;

namespace App.Managers;

public class UpgradeManager
{
    public void UnlockUpgrade(GameState state, string upgradeName){}
    
    public bool BuyUpgrade(GameState state, int upgradeId)
    {
        var upgrade = state.Upgrades.FirstOrDefault(upgrade => upgrade.Definition.Id == upgradeId);
        
        if (upgrade is null || upgrade.IsPurchased) return false;

        foreach (var cost in upgrade.Definition.Costs)
            if (!state.Resources.ContainsKey(cost.Key) || state.Resources[cost.Key] < cost.Value) return false;

        foreach (var cost in upgrade.Definition.Costs)
            state.Resources[cost.Key] -= cost.Value;
        
        upgrade.IsPurchased = true;
        return true;
    }
    public void ApplyUpgradeEffects(GameState state){}
    public void GetAvailableUpgrades(GameState state){}

    public static void InitUpgrades(GameState state)
    {
        state.Upgrades = new List<Upgrade>();

        foreach (var def in UpgradeDefinitions.All)
        {
            state.Upgrades.Add(new Upgrade
            {
                Definition = def,
                IsPurchased = false
            });
        }
    }
}