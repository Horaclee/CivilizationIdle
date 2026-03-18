using Core.Definitions;
using Core.Enums;

namespace Core.Models;

public class Civilization
{
    public required CivilizationDefinition Definition { get; init; }
    public bool IsUnlocked { get; set; }
}