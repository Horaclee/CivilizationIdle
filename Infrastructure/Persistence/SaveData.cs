using Core.Enums;
using Core.Models;
using System;

namespace Infrastructure.Persistence;

public class SaveData
{
    public DateTime LastSavedUtc { get; set; }

    public Dictionary<ResourceType, double> Resources { get; set; } = new ();

    public List<BuildingSave> Buildings { get; set; } = [];

    public List<UpgradeSave> Upgrades { get; set; } = [];
    

    public class BuildingSave
    {
        public BuildingType Type { get; set; }
        public int Count { get; set; }
    }

    public class UpgradeSave
    {
        public int Id  { get; set; }
        public bool IsPurchased { get; set; }
    }
}
