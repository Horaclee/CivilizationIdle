using Core.Definitions;
using Core.Enums;

namespace Core.Models;

public class Upgrade
{
    public required UpgradeDefinition Definition { get; init; }
    public bool IsPurchased { get; set; }

    public string GetCostText()
        => string.Join(", ", Definition.Costs.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
}