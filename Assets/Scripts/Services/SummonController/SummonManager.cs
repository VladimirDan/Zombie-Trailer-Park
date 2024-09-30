using UI;
using UI.Buttons;
using UnityEngine;
using System.Collections;
using Gameplay;
using Enums;
using Game.Code.Common.CoroutineRunner;

namespace Services
{
    public class SummonManager
    {
        private DataProvider dataProvider;
        private SpawnManager spawnManager;
        private UIManager uiManager;

        private PlayerBankModel playerBank;
        
        private float salvageYardsCount = 0;
        private float trailersCount = 0;
        private float farmHousesCount = 0;
        private float stillsCount = 0;
        private float garagesCount = 0;
        private float chapelsCount = 0;
        
        private bool isFarmHouseBuilt = false;
        private bool isStillBuilt = false;
        private bool isGarageBuilt = false;
        private bool isChapelBuilt = false;
        
        public SummonManager(DataProvider dataProvider, SpawnManager spawnManager, UIManager uiManager, PlayerBankModel playerBank)
        {
            this.dataProvider = dataProvider;
            this.spawnManager = spawnManager;
            this.uiManager = uiManager;
            this.playerBank = playerBank;
        }

        public IEnumerator SummonEntity(UnitType unitType, ButtonWithCooldown button, float summonCooldown)
        {
            uiManager.PlayButtonSummonEffect(button, summonCooldown);
            
            yield return new WaitForSeconds(summonCooldown);
            
            spawnManager.SpawnUnitOnRandomRow(unitType, spawnManager.baseSpawnPosition);
        }
        
        public IEnumerator SummonEntity(BuildingType buildingType, ButtonWithCooldown button, float summonCooldown)
        {
            uiManager.PlayButtonSummonEffect(button, summonCooldown);
            yield return new WaitForSeconds(summonCooldown);
            
            switch(buildingType)
            {
                case BuildingType.SalvageYard:
                    playerBank.moneyIncomeFrequency -= 0.2f;
                    break;
                
                case BuildingType.Trailer:
                    playerBank.ExpandArmyCapacity(5);
                    break;
                
                case BuildingType.FarmHouse:
                    isFarmHouseBuilt = true;
                    break;
                
                case BuildingType.Still:
                    isStillBuilt = true;
                    break;
                
                case BuildingType.Garage:
                    isGarageBuilt = true;
                    break;
                
                case BuildingType.Chapel:
                    isChapelBuilt = true;
                    break;
            }
        }

        public void PayForSummon(UnitType unitType)
        {
            float unitPrice = dataProvider.GetSummonPrice(unitType);
            float unitCapacity = dataProvider.GetUnitCapacity(unitType);
            
            playerBank.ReduceMoney(unitPrice);
            playerBank.AddArmyCapacity(unitCapacity);
        }
        
        public void PayForSummon(BuildingType buildingType)
        {
            float unitPrice = dataProvider.GetSummonPrice(buildingType);
            
            playerBank.ReduceMoney(unitPrice);
            
            switch(buildingType)
            {
                case BuildingType.SalvageYard:
                    salvageYardsCount++;
                    break;
                
                case BuildingType.Trailer:
                    trailersCount++;
                    break;
                
                case BuildingType.FarmHouse:
                    farmHousesCount++;
                    break;
                
                case BuildingType.Still:
                    stillsCount++;
                    break;
                
                case BuildingType.Garage:
                    garagesCount++;
                    break;
                
                case BuildingType.Chapel:
                    chapelsCount++;
                    break;
            }
        }
        
        public bool AreResourcesEnoughForSummoning(UnitType unitType)
        {
            float moneyPrice = dataProvider.GetSummonPrice(unitType);
            float unitCapacity = dataProvider.GetUnitCapacity(unitType);

            float playerMoney = playerBank.GetMoneyAmount();
            float playerAvailableArmyCapacity = playerBank.GetArmyMaxCapacity() -
                                       playerBank.GetArmyCapacity();

            bool hasEnoughMoney = playerMoney - moneyPrice >= 0;
            bool hasEnoughArmyCapacity = playerAvailableArmyCapacity - unitCapacity >= 0;
            bool isUnitRequirementAccomplished = IsSummonRequirementAccomplished(unitType);
            return hasEnoughMoney && hasEnoughArmyCapacity && isUnitRequirementAccomplished;
        }
        
        public bool AreResourcesEnoughForSummoning(BuildingType buildingType)
        {
            float moneyPrice = dataProvider.GetSummonPrice(buildingType);

            float playerMoney = playerBank.GetMoneyAmount();

            bool hasEnoughMoney = playerMoney - moneyPrice >= 0;
            bool isUnitRequirementAccomplished = IsSummonRequirementAccomplished(buildingType);
            return hasEnoughMoney && isUnitRequirementAccomplished;
        }

        public bool IsSummonRequirementAccomplished(UnitType unitType)
        {
            return unitType switch
            {
                UnitType.Digger => true,
                UnitType.Shooter => isFarmHouseBuilt,
                UnitType.Boozer => isStillBuilt,
                UnitType.SurvivalistCar => isGarageBuilt,
                UnitType.Cleric => isChapelBuilt,
                _ => true
            };
        }
        
        public bool IsSummonRequirementAccomplished(BuildingType buildingType)
        {
            return buildingType switch
            {
                BuildingType.SalvageYard => salvageYardsCount < dataProvider.GetBuildingCountLimit(BuildingType.SalvageYard),
                BuildingType.Trailer => trailersCount < dataProvider.GetBuildingCountLimit(BuildingType.Trailer),
                BuildingType.FarmHouse => farmHousesCount < dataProvider.GetBuildingCountLimit(BuildingType.FarmHouse),
                BuildingType.Still => stillsCount < dataProvider.GetBuildingCountLimit(BuildingType.Still) && isFarmHouseBuilt,
                BuildingType.Garage => garagesCount < dataProvider.GetBuildingCountLimit(BuildingType.Garage) && isFarmHouseBuilt,
                BuildingType.Chapel => chapelsCount < dataProvider.GetBuildingCountLimit(BuildingType.Chapel) && isStillBuilt && isGarageBuilt,
                _ => true
            };
        }
    }
}