using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ClericModel : UnitModel
{
    public override void Attack(GameObject target) 
    {
        Unit unit = target.GetComponent<Unit>();

        target.layer = (int)Mathf.Log(unit.unitModel.OpponentLayer.value, 2);

        unit.OpponentLayer = this.OpponentLayer;
        unit.unitModel.OpponentLayer = this.OpponentLayer;

        unit.CreatureHorizontalMovementDirection = 1;
        unit.unitModel.CreatureHorizontalMovementDirection = 1;
    }

    public override GameObject FindOpponent(float range)
    {
        GameObject opponent;
        float width = range;
        float depth = 10f;
        float height = 1f;

        Vector3 boxCenter = EntityTransform.position +
                            (Vector3.right * CreatureHorizontalMovementDirection * (width / 2)) +
                            (Vector3.forward * (depth / 2));

        Collider[] colliders = Physics.OverlapBox(boxCenter, new Vector3(width, height, depth), Quaternion.identity, OpponentLayer);

        if (colliders.Length != 0)
        {
            opponent = colliders.FindNearestCollider(EntityTransform).gameObject;
            if (opponent.GetComponent<MainBuildingEntity>() != null)
            {
                opponent = null;
            }
        }

        else
        {
            opponent = null;
        }

        return opponent;
    }
}

