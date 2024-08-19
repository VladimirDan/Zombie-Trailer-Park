using UnityEngine;

namespace CreaturesData
{

    [CreateAssetMenu(fileName = "ZombieJumperParametersData", menuName = "Entities/ZombieJumper Parameters Data")]

    public class ZombieJumperParametersData : UnitParametersData
    {
        public float jumpLength;
        public float jumpCooldown;
        public float opponentBaseXCoord;
    }
}
