using Assets.Scripts.Extensions;
using LevelsParameters;
using CreaturesData;
using System.Collections.Generic;
using UnityEngine;
using System;
using Enums;
using Gameplay.GameParameters;

namespace Services
{
    public class DataProvider: MonoBehaviour
    {
        [SerializeField] private GameObject [] unitsPrefabs;
        [SerializeField] private UnitParametersData[] entityDataConfigs;
        
        [SerializeField] private UnitsSummonData unitsSummonData;
        [SerializeField] private BuildingsSummonData buildingsSummonData;

        [SerializeField] private PlayerResources playerStartResources;
        [SerializeField] private BuildingsEffects buildingsEffects;
        
        [SerializeField] private TooltipsContent tooltipsContent;
        
        private Dictionary<UnitType, UnitParametersData> entityDataDictionary;
        private Dictionary<UnitType, GameObject> entityPrefabDictionary;
        
        public void Initialize()
        {
            InitializeDictionaries();
        }

        private void InitializeDictionaries()
        {
            entityDataDictionary = new Dictionary<UnitType, UnitParametersData>();
            entityPrefabDictionary = new Dictionary<UnitType, GameObject>();

            foreach (var data in entityDataConfigs)
            {
                entityDataDictionary[data.unitType] = data;
            }

            foreach (var prefab in unitsPrefabs)
            {
                entityPrefabDictionary[prefab.name.ToEnum<UnitType>()] = prefab;
            }
        }

        public UnitParametersData GetUnitData(UnitType unitType)
        {
            entityDataDictionary.TryGetValue(unitType, out UnitParametersData data);
            return data;
        }

        public float GetUnitYeeHawPointsDrop(UnitType unitType)
        {
            UnitParametersData data = GetUnitData(unitType);
            return data.YeeHawPointsDrop;
        }
        
        public GameObject GetUnitPrefab(UnitType unitType)
        {
            entityPrefabDictionary.TryGetValue(unitType, out var prefab);
            return prefab;
        }
        
        public float GetSummonCooldownTiming(UnitType unitType)
        {
            UnitSummonData elem = Array.Find(unitsSummonData.unitsSummonData,x => x.unitType == unitType);
            return elem.summonTime;
        }
        
        public float GetSummonPrice(UnitType unitType)
        {
            UnitSummonData elem = Array.Find(unitsSummonData.unitsSummonData,x => x.unitType == unitType);
            return elem.price;
        }
        
        public float GetUnitCapacity(UnitType unitType)
        {
            UnitSummonData elem = Array.Find(unitsSummonData.unitsSummonData,x => x.unitType == unitType);
            return elem.capacity;
        }
        
        public float GetSummonCooldownTiming(BuildingType buildingType)
        {
            BuildingSummonData elem = Array.Find(buildingsSummonData.buildingsSummonData,x => x.buildingType == buildingType);
            return elem.summonTime;
        }
        
        public float GetSummonPrice(BuildingType buildingType)
        {
            BuildingSummonData elem = Array.Find(buildingsSummonData.buildingsSummonData,x => x.buildingType == buildingType);
            return elem.price;
        }
        
        public PlayerResources GetPlayerStartResources()
        {
            return playerStartResources;
        }

        public float GetBuildingCountLimit(BuildingType buildingType)
        {
            BuildingSummonData elem = Array.Find(buildingsSummonData.buildingsSummonData,x => x.buildingType == buildingType);
            return elem.countLimit;
        }

        public MoneyIncomeParameters GetMoneyIncomeParameters()
        {
            return buildingsEffects.moneyIncomeParameters;
        }
        
        public ArmyCapacityUpgradeParameters GetArmyCapacityUpgradeParameters()
        {
            return buildingsEffects.armyCapacityUpgradeParameters;
        }

        public TooltipContent GetTooltipContent(UnitType unitType)
        {
            return Array.Find(tooltipsContent.unitsTooltips, x => x.title == unitType.EnumToName());
        }
        
        public TooltipContent GetTooltipContent(BuildingType buildingType)
        {
            return Array.Find(tooltipsContent.buildingsTooltips, x => x.title == buildingType.EnumToName());
        }
        
        public TooltipContent GetTooltipContent(YeeHawActionType yeeHawActionType)
        {
            return Array.Find(tooltipsContent.yeeHawActionsTooltips, x => x.title == yeeHawActionType.EnumToName());
        }
    }
}