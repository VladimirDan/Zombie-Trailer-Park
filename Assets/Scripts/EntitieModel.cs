using Assets.Scripts.StateMachine.States;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Entitie : MonoBehaviour
{
    private StateMachine stateMachine;
    [SerializeField] public float CreatureSpeed;
    [SerializeField] public float CreatureHorizontalMovementDirection;
    [SerializeField] public float AttackRange;
    [SerializeField] public float AttackDamage;
    [SerializeField] public float AttackSpeed;
    [SerializeField] public LayerMask OpponentLayer; 
    public Coroutine attackCoroutine;

    private Rigidbody2D entitieRigidbody2D;



    public void Walk(float speed, float movementDirection)
    {
        Vector2 currentVelocity = new Vector2(movementDirection, entitieRigidbody2D.velocity.y);
        entitieRigidbody2D.velocity = currentVelocity * speed;
    }

    public bool isEnemyInAttackRange()
    {
        Vector2 direction = new Vector2(CreatureHorizontalMovementDirection, transform.position.y);
        RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, direction, AttackRange, OpponentLayer);

        return hit.collider != null;
    }

    public IEnumerator Attack(RaycastHit2D hit) 
    {
        GameObject opponent = hit.collider.gameObject;
        HealthModel opponentHealth = opponent.GetComponent<HealthModel>();
        Entitie opponentEntitieModel = opponent.GetComponent<Entitie>();

        while (opponentHealth.isAlive())
        {
            opponentHealth.ReduceHealth(AttackDamage);
            yield return new WaitForSeconds(AttackSpeed);
        }

        opponentEntitieModel.Die();
        yield break;
    }

    public void Die() 
    {
        Destroy(this);
    }

    void Start()
    {
        entitieRigidbody2D = GetComponent<Rigidbody2D>();

        stateMachine = new StateMachine(this, new AFK(this));
    }

    void Update()
    {
        stateMachine.CheckoutCurrentState();  
    }

}
