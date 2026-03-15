using System;
using Core.Enums;
using Core.Models;
using App.Managers;
using Core.Systems;

namespace GameOfLifeConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialisation
            var GameManager = new GameManager();
            GameManager.StartGame();

            var resourceManager = new ResourceManager();
            var productionSystem = new ProductionSystem();
            var buildingManager = new BuildingManager();

            Console.WriteLine("=== Test Console Civilization Idle ===\n");

            var generations = 5;

            
            
            for (var x = 1; x <= generations; x++)
            {
                Console.WriteLine($"--- Génération {x} ---");

                // Production automatique via bâtiments
                productionSystem.ApplyProduction(GameManager.State);

                // Affiche les ressources
                foreach (ResourceType type in Enum.GetValues(typeof(ResourceType)))
                {
                    Console.WriteLine($"{type}: {GameManager.State.GetResources(type)}");
                }

                // Essayons d'acheter un bâtiment
                var firstBuilding = GameManager.State.Buildings[0];
                bool bought = buildingManager.BuyBuilding(GameManager.State, firstBuilding.Definition.Type);

                if (bought)
                    Console.WriteLine($"Achat réussi : {firstBuilding.Definition.Name}");
                else
                    Console.WriteLine($"Pas assez de ressources pour acheter : {firstBuilding.Definition.Name}");

                Console.WriteLine();
            }

            Console.WriteLine("=== Simulation terminée ===");
            Console.ReadLine();
        }
    }
}