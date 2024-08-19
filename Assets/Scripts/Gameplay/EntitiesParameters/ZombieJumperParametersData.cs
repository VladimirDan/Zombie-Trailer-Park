using UnityEngine;

namespace CreaturesData
{

    [CreateAssetMenu(fileName = "ZombieJumperParametersData", menuName = "Entities/ZombieJumper Parameters Data")]

    public class ZombieJumperParametersData : UnitParametersData
    {
        public float jumpLenght;
        public float jumpCoolDown;
        public float oponentBaseXCoord;
    }
}
