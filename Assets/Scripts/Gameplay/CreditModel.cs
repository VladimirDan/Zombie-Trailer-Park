using UnityEngine;

namespace Gameplay
{
    [System.Serializable]
    public class CreditModel
    {
        [SerializeField] private float credits;
        [SerializeField] private float creditsMaxCapacity;

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
            if (credits + amount <= creditsMaxCapacity)
            {
                credits += amount;
            }
        }
        
        public void ReduceCredits(float amount)
        {
            this.credits -= amount;
        }

        public CreditModel(float credits, float capacity)
        {
            SetCredits(credits);
            SetCreditsCapacity(capacity);
        }
    }
}