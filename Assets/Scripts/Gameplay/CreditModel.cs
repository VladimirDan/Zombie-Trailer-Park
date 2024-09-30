using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class CreditModel
    {
        [SerializeField] private float credits;
        [SerializeField] private float creditsMaxCapacity = 0;
        [SerializeField] private float creditsMinCapacity = -9999;

        public void SetCredits(float anount)
        {
            this.credits = anount;
        }

        public float GetCreditsAmount()
        {
            return credits;
        }

        public void SetCreditsCapacity(float amount)
        {
            this.creditsMaxCapacity = amount;
        }

        public float GetCreditsCapacity()
        {
            return creditsMaxCapacity;
        }

        public void AddCredits(float amount)
        {
            //credits = Mathf.Clamp(credits + amount, creditsMinCapacity, creditsMaxCapacity);
            credits += amount;
        }

        public void ReduceCredits(float amount)
        {
            //credits = Mathf.Clamp(credits - amount, creditsMinCapacity, creditsMaxCapacity);
            credits -= amount;
        }

        public void AddCreditsCapacity(float amount)
        {
            creditsMaxCapacity += amount;
        }

        public void ReduceCreditsCapacity(float amount)
        {
            creditsMaxCapacity -= amount;
        }

        public CreditModel(float credits, float capacity)
        {
            SetCredits(credits);
            SetCreditsCapacity(capacity);
        }
    }
}