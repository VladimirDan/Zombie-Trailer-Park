using System;
using UnityEngine;

namespace Services
{
    public class SummonController : MonoBehaviour
    {
        public SpawnManager spawnManager;

        public void Initialize(SpawnManager spawnManager)
        {
            this.spawnManager = spawnManager;
        }

        public void SummonUnit(UnitType unitType)
        {
            spawnManager.SpawnUnitOnRandomRow(unitType, spawnManager.baseSpawnPosition);
        }
    }
}