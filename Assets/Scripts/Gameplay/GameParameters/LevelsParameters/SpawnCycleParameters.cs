using Enums;

namespace GameParameters
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