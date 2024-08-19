using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZombieJumperModel : UnitModel
{
    public float jumpLenght;
    public float jumpCoolDown;
    public float oponentBaseXCoord;

    public float jumpHeight = 5f;
    public float gravityScale = 1f;

    public float lastJumpTime = 0f;

    public ZombieJumperModel(float jumpLenght, float jumpCoolDown, float oponentBaseXCoord, float creatureSpeed, 
                        float creatureHorizontalMovementDirection, float attackRange,
                        float attackDamage, float attackSpeed, LayerMask opponentLayer,
                        Rigidbody EntityRigidbody, Transform EntityTransform)
        : base(creatureSpeed, creatureHorizontalMovementDirection, attackRange,
            attackDamage, attackSpeed, opponentLayer, EntityRigidbody, EntityTransform) 
    { 
        this.jumpLenght = jumpLenght;
        this.jumpCoolDown = jumpCoolDown;
        this.oponentBaseXCoord = oponentBaseXCoord;
    }

    public void Jump()
    {
        EntityRigidbody.velocity = CalculateJumpForce(jumpLenght);
    }

    public Vector3 CalculateJumpForce(float jumpDistance)
    {
        float g = Mathf.Abs(Physics.gravity.y) * gravityScale;
        float verticalVelocity = Mathf.Sqrt(2 * g * jumpHeight);
        float timeToApex = verticalVelocity / g;
        float horizontalVelocity = jumpDistance / (2 * timeToApex) * CreatureHorizontalMovementDirection;

        return new Vector3(horizontalVelocity, verticalVelocity, EntityRigidbody.velocity.z);
    }

    public void Land()
    {
        EntityRigidbody.velocity = new Vector3(CreatureHorizontalMovementDirection, EntityRigidbody.velocity.y, EntityRigidbody.velocity.z);
    }

    public GameObject FindOpponentOutOffAttackRange(float range)
    {
        GameObject opponent;
        float width = range;
        float depth = 10f;
        float height = 1f;

        Vector3 attackRangeEdge = new Vector3(EntityTransform.position.x + AttackRange * CreatureHorizontalMovementDirection, EntityTransform.position.y, EntityTransform.position.z);

        Vector3 boxCenter = attackRangeEdge +
                            (Vector3.right * CreatureHorizontalMovementDirection * (width / 2)) +
                            (Vector3.forward * (depth / 2));

        Collider[] colliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentLayer);

        opponent = colliders.Length == 0 ? null : colliders.FindNearestCollider(EntityTransform).gameObject;

        return opponent;
    }

    public bool isEnemyInJumpDistanceRange()
    {
        return FindOpponentOutOffAttackRange(jumpLenght) != null;
    }

    public bool isJumpPossible()
    {
        return EntityTransform.position.x + (AttackRange + jumpLenght) * CreatureHorizontalMovementDirection > oponentBaseXCoord;
    }

    public bool isGrounded()
    {
        Collider collider = EntityTransform.GetComponent<Collider>();
        LayerMask groundLayer = LayerMask.GetMask("Floor");

        return Physics.CheckBox(collider.bounds.center, collider.bounds.extents, Quaternion.identity, groundLayer);
    }
}
