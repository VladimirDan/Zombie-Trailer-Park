using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "BombThrowerUnitParameters", menuName = "Entities/BombThrowerUnitParameters")]
    public class BombThrowerUnitParameters: UnitParametersData
    {
        public GameObject bombPrefab;
        public float bombThrowHeight;
    }
}