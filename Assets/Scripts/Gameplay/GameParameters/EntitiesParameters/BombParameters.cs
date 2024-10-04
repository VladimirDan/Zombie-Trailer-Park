using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "BombParameters", menuName = "Entities/BombParameters")]
    public class BombParameters : ScriptableObject
    {
        public float explosionRadius;
        public float damage;
        public LayerMask enemyUnitLayer;
        public LayerMask enemyBaseLayer;
        public int maxTargetsCount;
        public LayerMask explosionLayerTrigger;
    }
}