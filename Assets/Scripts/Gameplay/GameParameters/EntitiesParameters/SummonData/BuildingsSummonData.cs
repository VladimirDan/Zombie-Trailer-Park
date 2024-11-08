using Enums;
using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "BuildingsSummonData", menuName = "Entities/Buildings Summon Data")]
    public class BuildingsSummonData : ScriptableObject
    {
        public BuildingSummonData []buildingsSummonData;
    }
    
    [System.Serializable]
    public struct BuildingSummonData
    {
        public BuildingType buildingType;
        public float price;
        public float summonTime;
        public float countLimit;
    }
}