using UnityEngine;

namespace LevelsParameters
{
    [CreateAssetMenu(fileName = "BuildingEffects", menuName = "Building Effects/Building Effects")]
    public class BuildingsEffects : ScriptableObject
    {
        public MoneyIncomeParameters moneyIncomeParameters;
        public ArmyCapacityUpgradeParameters armyCapacityUpgradeParameters;
    }
}