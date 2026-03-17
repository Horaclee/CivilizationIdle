using Core.Definitions;

namespace Core.Models;

public class Building
{
    public required BuildingDefinition Definition { get; init; }

    public int Count { get; set; }
    
    public void IncreaseCount() => Count++;
}
