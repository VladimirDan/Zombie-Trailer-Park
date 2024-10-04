using Enums;
using UnityEngine;

namespace Services.SpawnManager
{
    public class YeeHawSpawnManager
    {
        private DataProvider dataProvider;
        private UnitsSpawnManager unitsSpawnManager;

        public YeeHawSpawnManager(UnitsSpawnManager unitsSpawnManager, DataProvider dataProvider)
        {
            this.unitsSpawnManager = unitsSpawnManager;
            this.dataProvider = dataProvider;
        }
        
        public void SummonYeeHawPower(YeeHawActionType yeeHawActionType)
        {
            switch (yeeHawActionType)
            {
                case YeeHawActionType.Harvester:
                    SpawnHarvester();
                    break;
                
                case YeeHawActionType.Bombardment:
                    SpawnAirStrikePlane();
                    break;
                
                case YeeHawActionType.CrowdSummon:
                    SpawnCrowd();
                    break;
                
                default:
                    break;
            }
        }
        
        public void SpawnHarvester()
        {
            unitsSpawnManager.SpawnUnitOnRandomRow(UnitType.Harvester, unitsSpawnManager.baseSpawnPosition); 
        }
        
        public void SpawnAirStrikePlane()
        {
            Vector3 spawnPosition = unitsSpawnManager.baseSpawnPosition;
            spawnPosition.y += 10;
            unitsSpawnManager.SpawnUnitOnRandomRow(UnitType.AirStrikePlane, spawnPosition); 
        }

        public void SpawnCrowd()
        {
            unitsSpawnManager.OrderUnitsSpawn(dataProvider.GetCrowdSpawnOrder());
        }
    }
}