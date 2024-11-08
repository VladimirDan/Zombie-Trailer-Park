using UnityEngine;
using Enums;
using UnityEngine.Serialization;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "UnitParametersData", menuName = "Entities/Unit Parameters Data")]

    public class UnitParametersData : EntityBasicData
    {
        public UnitType unitType;
        public LayerMask OpponentLayer;
        public LayerMask OpponentBaseLayer;
        public float CreatureSpeed;
        public float CreatureHorizontalMovementDirection;
        public float AttackRange;
        public float AttackDamage;
        public float AttackCooldown;
        public float YeeHawPointsDrop;
    }
}