using Gameplay.GameEntity.AirStrikePlane;
using Game.Code.Common.CoroutineRunner;
using Services;
using UnityEngine;
using Enums;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class AirStrikePlaneSpawner : StandartUnitSpawner
    {
        private CoroutineRunner coroutineRunner;
        
        public override void SpawnAndInitializeUnit(UnitType entityType, Vector3 spawnPosition, PlayerBankModel playerBankModel)
        {
            GameObject airStrikePlanePrefab = dataProvider.GetAirStrikePlanePrefab();
            
            GameObject airStrikePlaneObject = UnityEngine.Object.Instantiate(airStrikePlanePrefab, spawnPosition, 
                Quaternion.Euler(airStrikePlanePrefab.transform.rotation.eulerAngles));

            AirStrikePlane airStrikePlane = airStrikePlaneObject.GetComponent<AirStrikePlane>();
            airStrikePlane.coroutineRunner = coroutineRunner;
            
            airStrikePlane.Initialize();
        }

        public AirStrikePlaneSpawner(DataProvider dataProvider, PlayerBankModel playerBankModel,
            CoroutineRunner coroutineRunner) : base(dataProvider, playerBankModel)
        {
            this.coroutineRunner = coroutineRunner;
        }
    }
}