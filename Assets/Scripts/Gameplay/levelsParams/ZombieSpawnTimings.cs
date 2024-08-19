using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "ZombieSpawnTimings", menuName = "Level/Zombie Spawn Timings")]

    public class ZombieSpawnTimings : ScriptableObject
    {
        public FloatFloatTuple[] zombieSpawmCooldownChange;
        public UnitTypeFloatArrayTuple[] specialZombieSpawnTimings;
    }

    [System.Serializable]
    public class FloatFloatTuple
    {
        public float timing;
        public float spawnCooldown;
    }

    [System.Serializable]
    public class UnitTypeFloatArrayTuple
    {
        public UnitType specialZombieType;
        public float[] spawnTimings;
    }
}