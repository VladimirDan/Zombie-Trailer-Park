using Enums;
using UnityEngine;

namespace CreaturesData
{
    [CreateAssetMenu(fileName = "YeeHawPowersSummonData", menuName = "Entities/YeeHawPowersSummonData")]
    public class YeeHawPowersSummonData : ScriptableObject
    {
        public YeeHawPowerSummonData[] yeeHawPowerSummonData;
    }

    [System.Serializable]
    public struct YeeHawPowerSummonData
    {
        public YeeHawActionType yeeHawActionType;
        public float price;
        public float summonTime;
    }
}