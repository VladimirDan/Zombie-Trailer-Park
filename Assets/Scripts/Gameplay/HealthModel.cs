using UnityEngine;
using System;

public class HealthModel : MonoBehaviour
{
    [SerializeField] private float HealthPoints;
    public float fullHealthValue;
    
    public event Action onHealthChange;
    private void DisplayMessage(float healthPoints) => Debug.Log(healthPoints);

    public void SetHealth(float healthPoints)
    {
        this.HealthPoints = healthPoints;
        fullHealthValue = healthPoints;
        
        onHealthChange?.Invoke();
    }

    public void ReduceHealth(float damagePoints)
    {
        HealthPoints -= damagePoints;
        onHealthChange?.Invoke();
    }

    public bool IsAlive()
    {
        return HealthPoints > 0;
    }

    public float GetHealthPercentage()
    {
        if(fullHealthValue != 0)
            return HealthPoints / fullHealthValue;
        return 0;
    }
}
