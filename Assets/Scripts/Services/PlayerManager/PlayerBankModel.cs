using Game.Code.Common.CoroutineRunner;
using UnityEngine;
using Gameplay;
using System.Collections;
using GameParameters;
using UI;

namespace Services
{
    [System.Serializable]
    public class PlayerBankModel
    {
        private CoroutineRunner coroutineRunner;
        private DataProvider dataProvider;
        private LevelUIManager levelUIManager;
        
        public CreditModel money;
        public CreditModel yeeHawPoints;
        public CreditModel armyCapacity;
        
        public float moneyIncome;
        public float moneyIncomeCooldown;
        public float moneyIncomeCooldownAccelerationTime;

        public float armyCapacityUpgradeValue;
        
        public PlayerBankModel(CoroutineRunner coroutineRunner, DataProvider dataProvider, LevelUIManager levelUIManager)
        {
            this.coroutineRunner = coroutineRunner;
            this.dataProvider = dataProvider;
            this.levelUIManager = levelUIManager;
        }

        public void Initialize()
        {
            money = new CreditModel(0, 99999);
            yeeHawPoints = new CreditModel(0, 99999);
            armyCapacity = new CreditModel(0, 0);

            AddResources(dataProvider.GetPlayerStartResources());

            levelUIManager.UpdateAllResourcesInfo(this);

            MoneyIncomeParameters moneyIncomeParameters = dataProvider.GetMoneyIncomeParameters();
            ArmyCapacityUpgradeParameters armyCapacityUpgradeParameters = dataProvider.GetArmyCapacityUpgradeParameters();
            
            this.moneyIncome = moneyIncomeParameters.moneyIncome;
            this.moneyIncomeCooldown = moneyIncomeParameters.moneyIncomeCooldown;
            this.moneyIncomeCooldownAccelerationTime = moneyIncomeParameters.moneyIncomeCooldownAccelerationTime;
            this.armyCapacityUpgradeValue = armyCapacityUpgradeParameters.upgradeValue;
            
            StartMoneyIncomeCycle();
        }
        
        public void StartMoneyIncomeCycle()
        {
            coroutineRunner.RunCoroutine(GainMoneyCycle());
        }

        public IEnumerator GainMoneyCycle()
        {
            while (true)
            {
                AddMoney(moneyIncome);
                
                yield return new WaitForSeconds(moneyIncomeCooldown);
            }
        }

        public void AddResources(PlayerResources playerResources)
        {
            AddMoney(playerResources.money.GetCreditsAmount());
            ExpandArmyCapacity(playerResources.armyCapacity.GetCreditsCapacity());
            AddYeeHawPoints(playerResources.yeeHawPoints.GetCreditsAmount());
        }

        public void AddMoney(float amount)
        {
            money.AddCredits(amount);
            levelUIManager.UpdateMoneyInfo(this);
        }
        
        public void ReduceMoney(float amount)
        {
            money.ReduceCredits(amount);
            levelUIManager.UpdateMoneyInfo(this);
        }

        public float GetMoneyAmount()
        {
            return money.GetCreditsAmount();
        }
        public void AddYeeHawPoints(float amount)
        {
            yeeHawPoints.AddCredits(amount);
            levelUIManager.UpdateYeeHawPointsInfo(this);
        }
        
        public void ReduceYeeHawPoints(float amount)
        {
            yeeHawPoints.ReduceCredits(amount);
            levelUIManager.UpdateYeeHawPointsInfo(this);
        }
        
        public float GetYeeHawPointsAmount()
        {
            return yeeHawPoints.GetCreditsAmount();
        }
        
        public void AddArmyCapacity(float amount)
        {
            armyCapacity.AddCredits(amount);
            levelUIManager.UpdateArmyCapacityInfo(this);
        }

        public void ReduceArmyCapacity(float amount)
        {
            armyCapacity.ReduceCredits(amount);
            levelUIManager.UpdateArmyCapacityInfo(this);
        }

        public float GetArmyCapacity()
        {
            return armyCapacity.GetCreditsAmount();
        }

        public void UpgradeArmyCapacity()
        {
            ExpandArmyCapacity(armyCapacityUpgradeValue);
        }
        
        public void ExpandArmyCapacity(float amount)
        {
            armyCapacity.AddCreditsCapacity(amount);
            levelUIManager.UpdateArmyCapacityInfo(this);
        }

        public float GetArmyMaxCapacity()
        {
            return armyCapacity.GetCreditsCapacity();
        }

        public void AccelerateMoneyIncomeTime()
        {
            moneyIncomeCooldown -= moneyIncomeCooldownAccelerationTime;
        }
    }
}