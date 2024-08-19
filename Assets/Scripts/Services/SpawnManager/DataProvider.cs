using Assets.Scripts.Extensions;
using CreaturesData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Services.SpawnManager
{
    public class DataProvider : MonoBehaviour
    {
        [SerializeField] private GameObject [] prefabs;
        [SerializeField] private UnitParametersData[] entityDataConfigs;

        private Dictionary<UnitType, UnitParametersData> entityDataDictionary;
        private Dictionary<UnitType, GameObject> entityPrefabDictionary;

        private void Awake()
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

            foreach (var prefab in prefabs)
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
    }
}
