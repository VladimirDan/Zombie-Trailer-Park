using CreaturesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class ZombieJumperSpawner : StandartUnitSpawner
    {
        public override void SetParameters(Unit unitObject, UnitParametersData unitData)
        {
            base.SetParameters(unitObject, unitData);

            ((ZombieJumper)unitObject).jumpLength = ((ZombieJumperParametersData)unitData).jumpLength;
            ((ZombieJumper)unitObject).jumpCooldown = ((ZombieJumperParametersData)unitData).jumpCooldown;
            ((ZombieJumper)unitObject).opponentBaseXCoord = ((ZombieJumperParametersData)unitData).opponentBaseXCoord;
        }

        public ZombieJumperSpawner(DataProvider dataProvider) : base(dataProvider) { }
    }
}
