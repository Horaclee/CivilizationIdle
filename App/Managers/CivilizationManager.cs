using Core.Definitions;
using Core.Enums;
using Core.Models;
using Infrastructure.Config;

namespace App.Managers;

public class CivilizationManager
{
    public void UpdateCivilization(GameState state)
    {
        var nextStage = CivilizationDefinitions.All
            .Where(def => def.UnlockCondition(state))
            .OrderBy(def => def.Stage)
            .Last();

        if (nextStage.Stage == state.Stage) return;
        
        state.Stage = nextStage.Stage;
        Console.WriteLine($"Stage {nextStage.Stage} is now {nextStage.Stage}");
    }

    public double GetCurrentMultiplier(GameState state)
    {
        return CivilizationDefinitions.All
            .First(def => def.Stage == state.Stage)
            .ProductionMultiplier;
    }
}
