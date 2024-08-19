using Assets.Scripts.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ColliderArrayExtensions
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

    public static Collider[] FindNearestColliders(this Collider[] colliders, Transform referenceTransform, int maxCount)
    {
        Array.Sort(colliders, (a, b) =>
        {
            float distanceA = Vector3.Distance(referenceTransform.position, a.transform.position);
            float distanceB = Vector3.Distance(referenceTransform.position, b.transform.position);
            return distanceA.CompareTo(distanceB);
        });

        if (colliders.Length <= maxCount)
        {
            return colliders;
        }

        Collider[] nearestColliders = new Collider[maxCount];
        Array.Copy(colliders, nearestColliders, maxCount);

        return nearestColliders;
    }

}
