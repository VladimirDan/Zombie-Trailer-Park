using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "EntityParametersData", menuName = "Entities/Entity Parameters Data")]

    public class EntityParametersData : EntityBasicData
    {
        public float CreatureSpeed;
        public float CreatureHorizontalMovementDirection;
        public float AttackRange;
        public float AttackDamage;
        public float AttackSpeed;
    }
}