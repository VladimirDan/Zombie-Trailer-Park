using CreaturesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class ZombieJumperSpawner : EntitySpawner
    {

        public override void SpawnEntity(UnitType entityType, Vector3 spawnPosition) {
            GameObject prefab = dataProvider.GetUnitPrefab(entityType);
            ZombieJumperParametersData unitData = (ZombieJumperParametersData)dataProvider.GetUnitData(entityType);

            GameObject unit = UnityEngine.Object.Instantiate(prefab, spawnPosition, Quaternion.Euler(prefab.transform.rotation.eulerAngles));

            unit.GetComponent<HealthModel>().setHealth(unitData.health);

            ZombieJumper unitObject = unit.GetComponent<ZombieJumper>();
            unitObject.CreatureSpeed = unitData.CreatureSpeed;
            unitObject.CreatureHorizontalMovementDirection = unitData.CreatureHorizontalMovementDirection;
            unitObject.AttackDamage = unitData.AttackDamage;
            unitObject.AttackRange = unitData.AttackRange;
            unitObject.AttackSpeed = unitData.AttackSpeed;
            unitObject.jumpLenght = unitData.jumpLenght;
            unitObject.jumpCoolDown = unitData.jumpCoolDown;
            unitObject.oponentBaseXCoord = unitData.oponentBaseXCoord;


            unitObject.Initialize();
        }

        public ZombieJumperSpawner(DataProvider dataProvider) : base(dataProvider) { }
    }
}
