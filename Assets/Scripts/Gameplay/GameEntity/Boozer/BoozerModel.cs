using Assets.Scripts.StateMachine.States;
using UnityEngine;
using Services;
using Enums;

namespace Gameplay.GameEntity.Boozer
{
    public class BoozerModel : UnitModel
    {
        public GameObject bombPrefab;
        public float bombThrowHeight;
        private float gravityScale = 1f;

        public override void Attack(GameObject target)
        {
            Vector3 boozerPosition = transform.position;
            Vector3 boozerSize = transform.localScale;

            Vector3 spawnPosition = new Vector3(
                boozerPosition.x + boozerSize.x / 2,
                boozerPosition.y + boozerSize.y / 2,
                boozerPosition.z
            );
            
            GameObject bomb = Instantiate(bombPrefab, spawnPosition, Quaternion.identity);
            Rigidbody bombRigidBody = bomb.GetComponent<Rigidbody>();
            
            float throwDistance = Mathf.Abs(boozerPosition.x - target.transform.position.x);
            Vector3 throwForce = CalculateThrowForce(throwDistance);
            bombRigidBody.AddForce(throwForce, ForceMode.Impulse);
            
        }
        
        public Vector3 CalculateThrowForce(float jumpDistance)
        {
            float g = Mathf.Abs(Physics.gravity.y) * gravityScale;
            float verticalVelocity = Mathf.Sqrt(2 * g * bombThrowHeight);
            
            float timeToApex = verticalVelocity / g;
            float horizontalVelocity = jumpDistance / (2 * timeToApex) * CreatureHorizontalMovementDirection;

            return new Vector3(horizontalVelocity, verticalVelocity, EntityRigidbody.velocity.z);
        }
    }
}