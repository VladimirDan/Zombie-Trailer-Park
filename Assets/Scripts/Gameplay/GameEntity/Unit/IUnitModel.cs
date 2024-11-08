using Services;
using System.Collections;
using Enums;
using UnityEngine;
using Gameplay.GameEntity.Entity;

public interface IUnitModel : IEntityModel<UnitType>
{
    //public UnitType UnitType { get; set; }
    public float CreatureSpeed { get; set; }
    public float CreatureHorizontalMovementDirection { get; set; }
    public float AttackRange { get; set; }
    public float AttackDamage { get; set; }
    public float AttackCooldown { get; set; }
    public LayerMask OpponentLayer { get; set; }
    public LayerMask OpponentBaseLayer{ get; set; }
    public Rigidbody EntityRigidbody { get; set; }
    public Transform EntityTransform { get; set; }
    public Animator Animator{ get; set; }
    public AudioManager AudioManager{ get; set; }

    public void Walk(float speed, float movementDirection);
    public GameObject FindOpponent(float range);
    public bool isEnemyInAttackRange();
    public void Attack(GameObject target);
    public IEnumerator Fight();

}


