using Core.Definitions;
using Core.Enums;

namespace Core.Models;

public class Upgrade
{
    public UpgradeDefinition Definition { get; set; }
    public bool IsPurchased { get; set; }
    
}