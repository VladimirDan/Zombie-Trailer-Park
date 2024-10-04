using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "AirStrikePlaneParameters", menuName = "Entities/AirStrikePlaneParameters")]
    public class AirStrikePlaneParameters : ScriptableObject
    {
        public GameObject bombPrefab;
        public float attackSpeed;
        public float moveSpeed;
        public float bombDropCooldown;
    }
}