using UnityEngine;

namespace GameParameters
{
    [CreateAssetMenu(fileName = "UnitSpawnTimings", menuName = "Level/Unit Spawn Timings")]

    public class UnitsSpawnTimings : ScriptableObject
    {
        public UnitsSpawnCycleParameters[] unitsSpawnTimings;
    }
}
