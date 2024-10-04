using System.Collections;
using System.Linq;
using UnityEngine;

public class SplashDamageUnitModel : UnitModel
{
    public int maxSplashAttackTargets;

    public void Attack(GameObject[] targets)
    {
        foreach (GameObject target in targets)
        {
            HealthModel opponentHealth = target.GetComponent<HealthModel>();
            opponentHealth.ReduceHealth(AttackDamage);
        }
    }

    public override IEnumerator Fight()
    {
        GameObject[] targets = FindOpponents(AttackRange, maxSplashAttackTargets);
        yield return new WaitForSeconds(AttackSpeed);

        while (true)
        {
            if (targets.Any(item => item == null) && isEnemyInAttackRange())
            {
                targets = FindOpponents(AttackRange, maxSplashAttackTargets);
                Attack(targets);
            }
            else if (targets != null)
            {
                Attack(targets);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(AttackSpeed);
        }
    }

    public GameObject[] FindOpponents(float range, int maxOponentsCount)
    {
        GameObject[] opponents;
        float width = range;
        float depth = 10f;
        float height = 1f;

        Vector3 boxCenter = EntityTransform.position +
                            (Vector3.right * CreatureHorizontalMovementDirection * (width / 2)) +
                            (Vector3.forward * (depth / 2));

        Collider[] opponentUnitsColliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentLayer);
        Collider[] opponentBaseColliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentBaseLayer);

        if (opponentUnitsColliders.Length != 0)
        {
            opponents = opponentUnitsColliders.FindNearestColliders(EntityTransform, maxOponentsCount).Select(o => o.gameObject).ToArray();;
        }
        else
        {
            opponents = opponentBaseColliders.FindNearestColliders(EntityTransform, maxOponentsCount).Select(o => o.gameObject).ToArray();
        }

        return opponents;
    }
}

