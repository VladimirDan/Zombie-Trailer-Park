using System;
using UnityEngine;
using CreaturesData;
using System.Linq;
using Services;
using Gameplay.GameParameters.EntitiesParameters;

namespace Gameplay.Bomb
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private BombParameters bombParameters;
        [SerializeField] private float depth = 10f;
        
        [SerializeField] private Animator animator;
        private AudioManager audioManager;

        public void Start()
        {
            audioManager = FindObjectOfType<AudioManager>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (IsTouchingLayer(collision.gameObject, bombParameters.explosionLayerTrigger))
            {
                Explode();
                audioManager.PlayBombExplosionClip(bombParameters.bombType);
            }
        }
        
        bool IsTouchingLayer(GameObject otherObject, LayerMask targetLayerMask)
        {
            return (targetLayerMask.value & (1 << otherObject.layer)) != 0;
        }
        
        public void Explode()
        {
            DealDamage(FindTargets());
            Destroy(gameObject);
        }
        
        public void DealDamage(GameObject[] targets)
        {
            foreach (GameObject target in targets)
            {
                HealthModel opponentHealth = target.GetComponent<HealthModel>();
                opponentHealth.ReduceHealth(bombParameters.damage);
            }
        }
        
        public GameObject[] FindTargets()
        {
            GameObject[] opponents;
            
            float radius = bombParameters.explosionRadius;
            Vector3 explosionCenter = transform.position;

            Collider[] opponentUnitsColliders = Physics.OverlapBox(explosionCenter, new Vector3(radius, radius, depth), Quaternion.identity, bombParameters.enemyUnitLayer);
            Collider[] opponentBaseColliders = Physics.OverlapBox(explosionCenter, new Vector3(radius, radius, depth), Quaternion.identity, bombParameters.enemyBaseLayer);

            if (opponentUnitsColliders.Length != 0)
            {
                opponents = opponentUnitsColliders.FindNearestColliders(transform, bombParameters.maxTargetsCount).Select(o => o.gameObject).ToArray();;
            }
            else
            {
                opponents = opponentBaseColliders.FindNearestColliders(transform,  bombParameters.maxTargetsCount).Select(o => o.gameObject).ToArray();
            }

            return opponents;
        }
    }
}