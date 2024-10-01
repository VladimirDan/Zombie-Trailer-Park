using UI;
using UI.Buttons;
using UnityEngine;
using System.Collections;
using Gameplay;
using Enums;
using Game.Code.Common.CoroutineRunner;
using Services.SpawnManager;

namespace Services
{
    public class SummonManager
    {
        private DataProvider dataProvider;
        private UnitsSpawnManager unitsSpawnManager;
        private BuildingsSpawnManager buildingsSpawnManager;
        private UIManager uiManager;

        private PlayerBankModel playerBank;
        
        public SummonManager(DataProvider dataProvider, UnitsSpawnManager unitsSpawnManager, BuildingsSpawnManager buildingsSpawnManager,
            UIManager uiManager, PlayerBankModel playerBank)
        {
            this.dataProvider = dataProvider;
            this.unitsSpawnManager = unitsSpawnManager;
            this.buildingsSpawnManager = buildingsSpawnManager;
            this.uiManager = uiManager;
            this.playerBank = playerBank;
        }

        public IEnumerator SummonEntity(UnitType unitType, ButtonWithCooldown button, float summonCooldown)
        {
            uiManager.PlayButtonSummonEffect(button, summonCooldown);
            
            yield return new WaitForSeconds(summonCooldown);
            
            unitsSpawnManager.SpawnUnitOnRandomRow(unitType, unitsSpawnManager.baseSpawnPosition);
        }
        
        public IEnumerator SummonEntity(BuildingType buildingType, ButtonWithCooldown button, float summonCooldown)
        {
            uiManager.PlayButtonSummonEffect(button, summonCooldown);
            yield return new WaitForSeconds(summonCooldown);
            
            buildingsSpawnManager.SpawnBuilding(buildingType);
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
            
            buildingsSpawnManager.OccupyBuildingSlot(buildingType);
        }
        
        public bool AreResourcesEnoughForSummoning(UnitType unitType, TooltipUIManager tooltipUIManager)
        {
            float moneyPrice = dataProvider.GetSummonPrice(unitType);
            float unitCapacity = dataProvider.GetUnitCapacity(unitType);

            float playerMoney = playerBank.GetMoneyAmount();
            float playerAvailableArmyCapacity = playerBank.GetArmyMaxCapacity() -
                                       playerBank.GetArmyCapacity();

            bool hasEnoughMoney = playerMoney - moneyPrice >= 0;
            bool hasEnoughArmyCapacity = playerAvailableArmyCapacity - unitCapacity >= 0;
            bool isUnitRequirementAccomplished = IsSummonRequirementAccomplished(unitType);

            tooltipUIManager.isMoneyEnough = hasEnoughMoney;
            tooltipUIManager.isArmyCapacityEnough = hasEnoughArmyCapacity;
            tooltipUIManager.isSummonRequirementAccomplished = isUnitRequirementAccomplished;
            
            return hasEnoughMoney && hasEnoughArmyCapacity && isUnitRequirementAccomplished;
        }
        
        public bool AreResourcesEnoughForSummoning(BuildingType buildingType, TooltipUIManager tooltipUIManager)
        {
            float moneyPrice = dataProvider.GetSummonPrice(buildingType);

            float playerMoney = playerBank.GetMoneyAmount();

            bool hasEnoughMoney = playerMoney - moneyPrice >= 0;
            bool isUnitRequirementAccomplished = IsSummonRequirementAccomplished(buildingType);
            
            tooltipUIManager.isMoneyEnough = hasEnoughMoney;
            tooltipUIManager.isSummonRequirementAccomplished = isUnitRequirementAccomplished;
            
            return hasEnoughMoney && isUnitRequirementAccomplished;
        }

        public bool IsSummonRequirementAccomplished(UnitType unitType)
        {
            return unitType switch
            {
                UnitType.Digger => true,
                UnitType.Shooter => buildingsSpawnManager.IsThereBuildingOfType(BuildingType.FarmHouse),
                UnitType.Boozer => buildingsSpawnManager.IsThereBuildingOfType(BuildingType.Still),
                UnitType.SurvivalistCar => buildingsSpawnManager.IsThereBuildingOfType(BuildingType.Garage),
                UnitType.Cleric => buildingsSpawnManager.IsThereBuildingOfType(BuildingType.Chapel),
                _ => true
            };
        }
        
        public bool IsSummonRequirementAccomplished(BuildingType buildingType)
        {
            return buildingType switch
            {
                BuildingType.SalvageYard => buildingsSpawnManager.IsThereFreeSlot(BuildingType.SalvageYard),
                BuildingType.Trailer => buildingsSpawnManager.IsThereFreeSlot(BuildingType.Trailer),
                BuildingType.FarmHouse => buildingsSpawnManager.IsThereFreeSlot(BuildingType.FarmHouse),
                BuildingType.Still => buildingsSpawnManager.IsThereFreeSlot(BuildingType.Still) && buildingsSpawnManager.IsThereBuildingOfType(BuildingType.FarmHouse),
                BuildingType.Garage => buildingsSpawnManager.IsThereFreeSlot(BuildingType.Garage) && buildingsSpawnManager.IsThereBuildingOfType(BuildingType.FarmHouse),
                BuildingType.Chapel =>buildingsSpawnManager.IsThereFreeSlot(BuildingType.Chapel) && buildingsSpawnManager.IsThereBuildingOfType(BuildingType.Still) && buildingsSpawnManager.IsThereBuildingOfType(BuildingType.Garage),
                _ => true
            };
        }
    }
}