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
        [SerializeField] private EntityBasicData [] entityDataConfigs;

        private Dictionary<string, EntityBasicData> entityDataDictionary;
        private Dictionary<string, GameObject> entityPrefabDictionary;

        private void Awake()
        {
            InitializeDictionaries();
        }

        private void InitializeDictionaries()
        {
            entityDataDictionary = new Dictionary<string, EntityBasicData>();
            entityPrefabDictionary = new Dictionary<string, GameObject>();

            foreach (var data in entityDataConfigs)
            {
                entityDataDictionary[data.unitType] = data;
            }

            foreach (var prefab in prefabs)
            {
                entityPrefabDictionary[prefab.name] = prefab;
            }
        }

        public EntityBasicData GetUnitData(string unitType)
        {
            entityDataDictionary.TryGetValue(unitType, out var data);
            return data;
        }

        public GameObject GetUnitPrefab(string unitType)
        {
            entityPrefabDictionary.TryGetValue(unitType, out var prefab);
            return prefab;
        }
    }
}
