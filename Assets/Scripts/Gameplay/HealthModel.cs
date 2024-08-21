using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HealthModel;

public class HealthModel : MonoBehaviour
{
    [SerializeField] private float HealthPoints;
    public delegate void OnHealthChange(float health);
    public event OnHealthChange onHealthChange;
    private void DisplayMessage(float healthPoints) => Debug.Log(healthPoints);

    public void setHealth(float healthPoints)
    {
        this.HealthPoints = healthPoints;
        onHealthChange?.Invoke(HealthPoints);
    }

    public void ReduceHealth(float damagePoints)
    {
        HealthPoints -= damagePoints;
        onHealthChange?.Invoke(HealthPoints);
    }

    public bool isAlive()
    {
        return HealthPoints > 0;
    }
}
