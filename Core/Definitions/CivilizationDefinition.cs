using Core.Enums;
using Core.Models;

namespace Core.Definitions;

public class CivilizationDefinition
{
    public string Name { get; init; } = "";
    public CivilizationStage Stage { get; init; }
    public Func<GameState, bool> UnlockCondition { get; set; } = null!;
    public double ProductionMultiplier { get; set; } = 1.0;
}
