using UnityEngine;

public class ZombieJumperModel : UnitModel
{
    public float jumpLength;
    public float jumpCooldown;
    public float opponentBaseXCoord;

    private float jumpHeight = 3f;
    private float gravityScale = 1f;

    public float lastJumpTime = 0f;

    public void Jump()
    {
        EntityRigidbody.velocity = CalculateJumpForce(jumpLength);
    }

    public Vector3 CalculateJumpForce(float jumpDistance)
    {
        float g = Mathf.Abs(Physics.gravity.y) * gravityScale; // ���������, ��� `g` ��������� �������������
        float verticalVelocity = Mathf.Sqrt(2 * g * jumpHeight);

        // Time to apex (����� �� ���������� ������������ ������) ������ ���� ���������� ���������
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
        return FindOpponentOutOffAttackRange(jumpLength) != null;
    }

    public bool isJumpPossible()
    {
        return EntityTransform.position.x + (AttackRange + jumpLength) * CreatureHorizontalMovementDirection > opponentBaseXCoord;
    }

    public bool isGrounded()
    {
        Collider collider = EntityTransform.GetComponent<Collider>();
        LayerMask groundLayer = LayerMask.GetMask("Floor");

        return Physics.CheckBox(collider.bounds.center, collider.bounds.extents, Quaternion.identity, groundLayer);
    }
}
