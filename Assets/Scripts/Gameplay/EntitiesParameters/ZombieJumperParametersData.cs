using UnityEngine;

namespace CreaturesData
{

    [CreateAssetMenu(fileName = "ZombieJumperParametersData", menuName = "Entities/ZombieJumper Parameters Data")]

    public class ZombieJumperParametersData : EntityParametersData
    {
        public float jumpLenght;
        public float jumpCoolDown;
        public float oponentBaseXCoord;
    }
}
