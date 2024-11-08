using Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace Services.SpawnManager
{
    public class YeeHawSpawnManager
    {
        private DataProvider dataProvider;
        private UnitsSpawnManager unitsSpawnManager;
        private AudioManager audioManager;
        private Vector2 plane1SpawnCoords = new Vector2(-2,11);
        private Vector2 plane2SpawnCoords = new Vector2(-8,10);
        private Vector2 plane3SpawnCoords = new Vector2(-6,15);

        public YeeHawSpawnManager(UnitsSpawnManager unitsSpawnManager, DataProvider dataProvider, AudioManager audioManager)
        {
            this.unitsSpawnManager = unitsSpawnManager;
            this.dataProvider = dataProvider;
            this.audioManager = audioManager;
        }
        
        public void SummonYeeHawPower(YeeHawActionType yeeHawActionType)
        {
            audioManager.PlayAddYeeHawPowerSpawnSound(yeeHawActionType);
            switch (yeeHawActionType)
            {
                case YeeHawActionType.Harvester:
                    SpawnHarvester();
                    break;
                
                case YeeHawActionType.Bombardment:
                    SpawnAirStrikePlane(plane1SpawnCoords);
                    SpawnAirStrikePlane(plane2SpawnCoords);
                    SpawnAirStrikePlane(plane3SpawnCoords);
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
            unitsSpawnManager.SpawnUnitOnRandomRow(UnitType.Harvester, unitsSpawnManager.spawnPosition); 
        }
        
        public void SpawnAirStrikePlane(Vector2 spawnCoords)
        {
            Vector3 spawnPosition = unitsSpawnManager.spawnPosition;
            spawnPosition.y += spawnCoords.y;
            spawnPosition.x += spawnCoords.x;
            unitsSpawnManager.SpawnUnitOnRandomRow(UnitType.AirStrikePlane, spawnPosition); 
        }

        public void SpawnCrowd()
        {
            unitsSpawnManager.OrderUnitsSpawn(dataProvider.GetCrowdSpawnOrder());
        }
    }
}