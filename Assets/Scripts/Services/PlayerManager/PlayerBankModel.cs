using UnityEngine;
using Gameplay;
using UnityEngine.Serialization;

namespace Services
{
    [System.Serializable]
    public class PlayerBankModel
    {
        [SerializeField] public CreditModel money;
        [SerializeField] public CreditModel yeeHawPoints;
        [SerializeField] public CreditModel armyCapacity;
        
        public PlayerBankModel(CreditModel money, CreditModel yeeHawPoints, CreditModel armyCapacity)
        {
            this.money = money;
            this.yeeHawPoints = yeeHawPoints;
            this.armyCapacity = armyCapacity;
        }
    }
}