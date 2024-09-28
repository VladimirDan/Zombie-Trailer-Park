using System.Collections.Generic;
using Game.Code.Common.CoroutineRunner;
using UnityEngine;
using Gameplay;
using System.Collections;

namespace Services
{
    [System.Serializable]
    public class PlayerBankModel
    {
        [SerializeField] public CreditModel money;
        [SerializeField] public CreditModel yeeHawPoints;
        [SerializeField] public CreditModel armyCapacity;
        
        [SerializeField] public float moneyIncome = 50;
        [SerializeField] public float moneyIncomeFrequency = 2;
        
        public PlayerBankModel(CreditModel money, CreditModel yeeHawPoints, CreditModel armyCapacity)
        {
            this.money = money;
            this.yeeHawPoints = yeeHawPoints;
            this.armyCapacity = armyCapacity;
        }

        public IEnumerator StartMoneyIncome()
        {
            while (true)
            {
                GainMoney();
                
                yield return new WaitForSeconds(moneyIncomeFrequency);
            }
        }

        public void GainMoney()
        {
            money.AddCredits(moneyIncome);
        }
    }
}