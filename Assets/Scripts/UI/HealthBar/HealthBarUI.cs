using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI.HealthBar
{
    public class HealthBarUI : MonoBehaviour
    {
        private HealthModel health;
        [SerializeField] private Slider healthSlider;
        private Action OnHealthChange;

        public void SetHealth(int currentHealth)
        {
            healthSlider.value = currentHealth;
        }
        
        public void Initialize(HealthModel health)
        {
            this.health = health;
            healthSlider.maxValue = 1f;
            health.onHealthChange += () => healthSlider.value = health.GetHealthPercentage();
        }
    }
}