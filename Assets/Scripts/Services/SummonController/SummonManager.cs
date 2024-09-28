using UI;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game.Code.Common.CoroutineRunner;
using Gameplay;
using System;
using System.Diagnostics;
using Enums;

namespace Services
{
    public class SummonManager
    {
        private DataProvider dataProvider;
        private SpawnManager spawnManager;

        private bool isFarmHouseBuilt;
        private bool isGarageBuilt;
        private bool isStillBuilt;
        private bool isChapelBuilt;

        public SummonManager(DataProvider dataProvider, SpawnManager spawnManager)
        {
            this.dataProvider = dataProvider;
            this.spawnManager = spawnManager;
        }

        public void SummonUnit(UnitType unitType)
        {
            spawnManager.SpawnUnitOnRandomRow(unitType, spawnManager.baseSpawnPosition);
        }

        public bool AreResourcesEnoughForUnit(UnitType unitType, PlayerBankModel playerBank)
        {
            float unitPrice = dataProvider.GetUnitPrice(unitType);
            float unitCapacity = dataProvider.GetUnitCapacity(unitType);

            float playerMoney = playerBank.money.GetCreditsAmount();
            float playerAvailableArmyCapacity = playerBank.armyCapacity.GetCreditsCapacity() -
                                       playerBank.armyCapacity.GetCreditsAmount();

            bool hasEnoughMoney = playerMoney - unitPrice >= 0;
            bool hasEnoughArmyCapacity = playerAvailableArmyCapacity - unitCapacity >= 0;
            bool isUnitRequirementAccomplished = IsUnitRequirementAccomplished(unitType);
            return hasEnoughMoney && hasEnoughArmyCapacity && isUnitRequirementAccomplished;
        }

        public bool IsUnitRequirementAccomplished(UnitType unitType)
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
    }
}