using CreaturesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class SplashDamageUnitSpawner : EntitySpawner
    {

        public override void SpawnEntity(UnitType entityType, Vector3 spawnPosition)
        {
            GameObject prefab = dataProvider.GetUnitPrefab(entityType);
            SplashDamageUnitParametersData unitData = (SplashDamageUnitParametersData)dataProvider.GetUnitData(entityType);

            GameObject unit = UnityEngine.Object.Instantiate(prefab, spawnPosition, Quaternion.Euler(prefab.transform.rotation.eulerAngles));

            unit.GetComponent<HealthModel>().setHealth(unitData.health);

            SplashDamageUnit unitObject = unit.GetComponent<SplashDamageUnit>();
            unitObject.OpponentLayer = unitData.OpponentLayer;
            unitObject.CreatureSpeed = unitData.CreatureSpeed;
            unitObject.CreatureHorizontalMovementDirection = unitData.CreatureHorizontalMovementDirection;
            unitObject.AttackDamage = unitData.AttackDamage;
            unitObject.AttackRange = unitData.AttackRange;
            unitObject.AttackSpeed = unitData.AttackSpeed;
            unitObject.maxSplashAttackTargets = unitData.maxSplashAttackTargets;

            unitObject.Initialize();
            unitObject.isInitialized = true;
        }

        public SplashDamageUnitSpawner(DataProvider dataProvider) : base(dataProvider) { }
    }
}
