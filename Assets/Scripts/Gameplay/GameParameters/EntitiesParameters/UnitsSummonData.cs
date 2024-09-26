using UnityEngine;
using UnityEngine.Serialization;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "UnitsData", menuName = "Entities/Units Data")]
    public class UnitsSummonData : ScriptableObject
    {
        public UnitSummonData []unitsData;
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