using UI.Buttons;
using Enums;
using System.Collections.Generic;
using Gameplay.Building;

namespace Services.SpawnManager
{
    public class BuildingsSpawnManager
    {
        private DataProvider dataProvider;
        private PlayerBankModel playerBank;
        
        private Dictionary<BuildingType, Building> buildings;

        public BuildingsSpawnManager(DataProvider dataProvider, PlayerBankModel playerBank)
        {
            this.dataProvider = dataProvider;
            this.playerBank = playerBank;

            buildings = new Dictionary<BuildingType, Building>
            {
                { BuildingType.SalvageYard, new SalvageYardBuilding(dataProvider, playerBank) },
                { BuildingType.Trailer, new TrailerBuilding(dataProvider, playerBank) },
                { BuildingType.FarmHouse, new Building(dataProvider) },
                { BuildingType.Still, new Building(dataProvider) },
                { BuildingType.Garage, new Building(dataProvider) },
                { BuildingType.Chapel, new Building(dataProvider) }
            };
        }
        
        public void SpawnBuilding(BuildingType buildingType)
        {
            if (buildings.TryGetValue(buildingType, out var building))
            {
                building.Spawn();
            }
        }
        
        public void OccupyBuildingSlot(BuildingType buildingType)
        {
            if (buildings.TryGetValue(buildingType, out var building))
            {
                building.OccupySlot();
            }
        }
        
        public bool IsThereFreeSlot(BuildingType buildingType)
        {
            if (buildings.TryGetValue(buildingType, out var building))
            {
                return building.HasFreeSlot(buildingType);
            }

            return false;
        }
        
        public bool IsThereBuildingOfType(BuildingType buildingType)
        {
            if (buildings.TryGetValue(buildingType, out var building))
            {
                return building.IsBuilt();
            }

            return false;
        }
    }
}