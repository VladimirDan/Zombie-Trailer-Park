using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "ZombieSpawnTimings", menuName = "Level/Zombie Spawn Timings")]

    public class ZombiesSpawnTimings : ScriptableObject
    {
        public ZombiesSpawnCycleParameters[] zombiesSpawnTimings;
    }
}
