using Core.Definitions;
using Core.Enums;

namespace Core.Models;

public class Building
{
    public BuildingDefinition Definition { get; set; }

    public int Count { get; set; }

    public void IncreaseCount() => Count++;
    
}