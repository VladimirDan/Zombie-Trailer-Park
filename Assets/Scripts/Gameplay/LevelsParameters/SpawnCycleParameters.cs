using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[System.Serializable]
public class ZombieSpawnCycleParameters
{
    public float timing;
    public float spawnCooldown;
    public int zombieCount;
}

[System.Serializable]
public class ZombiesSpawnCycleParameters
{
    public UnitType zombieType;
    public ZombieSpawnCycleParameters[] zombieSpawnCycleParameters;
}
