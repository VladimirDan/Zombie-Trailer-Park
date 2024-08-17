using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "Entities/Entity Data")]
public class EntityData : HealthData
{
    public float CreatureSpeed;
    public float CreatureHorizontalMovementDirection;
    public float AttackRange;
    public float AttackDamage;
    public float AttackSpeed;
}