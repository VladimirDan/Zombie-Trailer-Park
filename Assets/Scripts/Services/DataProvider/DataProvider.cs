using Assets.Scripts.Extensions;
using LevelsParameters;
using CreaturesData;
using System.Collections.Generic;
using UnityEngine;
using System;
using Gameplay;
using UI;
using UnityEngine.Serialization;

namespace Services
{
    public class DataProvider: MonoBehaviour
    {
        [SerializeField] private GameObject [] unitsPrefabs;
        [SerializeField] private UnitParametersData[] entityDataConfigs;
        
        [SerializeField] private UnitsSummonData unitsSummonData;

        [SerializeField] private PlayerResources playerStartResources;
        
        private Dictionary<UnitType, UnitParametersData> entityDataDictionary;
        private Dictionary<UnitType, GameObject> entityPrefabDictionary;

        private MoneyPanelUIManager moneyPanelUIManager;
        private UITextManager yeeHawUIPanelManager;
        private ArmyCapacityPanelUIManager armyCapacityUIPanelManager;
        
        public void Initialize()
        {
            InitializeDictionaries();

            moneyPanelUIManager = GameObject.FindObjectOfType<MoneyPanelUIManager>();
            yeeHawUIPanelManager = GameObject.Find("Yee-Haw Panel").GetComponent<UITextManager>();
            armyCapacityUIPanelManager = GameObject.FindObjectOfType<ArmyCapacityPanelUIManager>();
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

        public EntityBasicData GetUnitData(UnitType unitType)
        {
            entityDataDictionary.TryGetValue(unitType, out var data);
            return data;
        }

        public GameObject GetUnitPrefab(UnitType unitType)
        {
            entityPrefabDictionary.TryGetValue(unitType, out var prefab);
            return prefab;
        }
        
        public float GetSummonCooldownTiming(UnitType unitType)
        {
            UnitSummonData elem = Array.Find(unitsSummonData.unitsData,x => x.unitType == unitType);
            return elem.summonTime;
        }
        
        public float GetUnitPrice(UnitType unitType)
        {
            UnitSummonData elem = Array.Find(unitsSummonData.unitsData,x => x.unitType == unitType);
            return elem.price;
        }
        
        public float GetUnitCapacity(UnitType unitType)
        {
            UnitSummonData elem = Array.Find(unitsSummonData.unitsData,x => x.unitType == unitType);
            return elem.capacity;
        }

        public CreditModel GetPlayerStartMoney()
        {
            return playerStartResources.money;
        }
        
        public CreditModel GetPlayerStartYeeHawPoints()
        {
            return playerStartResources.yeeHawPoints;
        }
        
        public CreditModel GetPlayerStartArmyCapacity()
        {
            return playerStartResources.armyCapacity;
        }

        public MoneyPanelUIManager GetMoneyPanelManager()
        {
            return moneyPanelUIManager;
        }
        
        public UITextManager GetYeeHawUIPanelManager()
        {
            return yeeHawUIPanelManager;
        }
        
        public ArmyCapacityPanelUIManager GetArmyCapacityUIPanelManager()
        {
            return armyCapacityUIPanelManager;
        }
    }
}