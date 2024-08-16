using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ColliderArrayExtension
{
    public static Collider FindNearestCollider(this Collider[] array, Transform referenceTransform)
    {
        Collider nearestCollider = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider collider in array)
        {
            float distance = Vector3.Distance(referenceTransform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestCollider = collider;
            }
        }

        return nearestCollider;
    }
}
