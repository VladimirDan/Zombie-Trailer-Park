using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;

namespace LevelsParameters
{

    [System.Serializable]
    public class UnitSpawnCycleParameters
    {
        public float timing;
        public float spawnCooldown;
        public int unitCount;
    }

    [System.Serializable]
    public class UnitsSpawnCycleParameters
    {
        public UnitType unitType;
        public UnitSpawnCycleParameters[] unitSpawnCycleParameters;
    }
}