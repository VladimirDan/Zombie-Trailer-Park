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

    public void ReduceHealth(float damagePoints)
    {
        HealthPoints -= damagePoints;
        onHealthChange?.Invoke(HealthPoints);
    }
    public bool isAlive()
    {
        return HealthPoints > 0;
    }
    void Start()
    {
        onHealthChange += DisplayMessage;
    }

    void Update()
    {

    }

    HealthModel(float healthPoints)
    {
        HealthPoints = healthPoints;
        //onHealthChange += DisplayMessage;
    }
}
