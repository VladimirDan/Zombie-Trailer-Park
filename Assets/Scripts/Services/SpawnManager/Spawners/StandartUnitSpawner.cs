using CreaturesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class StandartUnitSpawner : EntitySpawner        //Every unit that only deals damage or walk is standart unit
    {

        public override void SpawnEntity(string entityName, Vector3 spawnPosition) {
            GameObject prefab = dataProvider.GetUnitPrefab(entityName);
            EntityParametersData unitData = (EntityParametersData)dataProvider.GetUnitData(entityName);

            GameObject unit = UnityEngine.Object.Instantiate(prefab, spawnPosition, Quaternion.Euler(prefab.transform.rotation.eulerAngles));

            unit.GetComponent<HealthModel>().setHealth(unitData.health);

            Unit unitObject = unit.GetComponent<Unit>();
            unitObject.CreatureSpeed = unitData.CreatureSpeed;
            unitObject.CreatureHorizontalMovementDirection = unitData.CreatureHorizontalMovementDirection;
            unitObject.AttackDamage = unitData.AttackDamage;
            unitObject.AttackRange = unitData.AttackRange;
            unitObject.AttackSpeed = unitData.AttackSpeed;

            unitObject.Initialize();
        }

        public StandartUnitSpawner(DataProvider dataProvider) : base(dataProvider) { }
    }
}
