using UnityEngine;
using UnityEngine.Serialization;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "BuildingsSummonData", menuName = "Entities/Units Summon Data")]
    public class UnitsSummonData : ScriptableObject
    {
        public UnitSummonData []unitsSummonData;
    }
    
    
    [System.Serializable]
    public struct UnitSummonData
    {
        public UnitType unitType;
        public float price;
        public float capacity;
        public float summonTime;
    }
}