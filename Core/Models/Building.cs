using Core.Definitions;
using Core.Enums;

namespace Core.Models;

public class Building
{
    public required BuildingDefinition Definition { get; init; }

    public int Count { get; set; }
    
    public void IncreaseCount() => Count++;
    
    public string GetCostText()
    {
        var costs = Definition.Costs
            .ToDictionary(
                kvp => kvp.Key,
                kvp => Math.Round(
                    kvp.Value * Math.Pow(Definition.CostMultiplier, Count),
                    0,
                    MidpointRounding.AwayFromZero
                )
            );

        return string.Join(", ", costs.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
    }
}