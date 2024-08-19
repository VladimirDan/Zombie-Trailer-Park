using CreaturesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class SplashDamageUnitSpawner : StandartUnitSpawner
    {

        public override void SpawnUnit(UnitType entityType, Vector3 spawnPosition)
        {
            base.SpawnUnit(entityType, spawnPosition);
        }

        public override void SetParameters(Unit unitObject, UnitParametersData unitData)
        {
            base.SetParameters(unitObject, unitData);
            ((SplashDamageUnit)unitObject).maxSplashAttackTargets = ((SplashDamageUnitParametersData)unitData).maxSplashAttackTargets;
        }

        public SplashDamageUnitSpawner(DataProvider dataProvider) : base(dataProvider) { }
    }
}
