using UnityEngine;

namespace LevelsParameters
{
    [CreateAssetMenu(fileName = "UnitSpawnTimings", menuName = "Level/Unit Spawn Timings")]

    public class UnitsSpawnTimings : ScriptableObject
    {
        public UnitsSpawnCycleParameters[] unitsSpawnTimings;
    }
}
