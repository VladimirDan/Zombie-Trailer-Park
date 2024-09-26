using UnityEngine;
using Gameplay;
using UnityEngine.Serialization;

namespace LevelsParameters
{
    [CreateAssetMenu(fileName = "LevelStartResources", menuName = "Level/Level Start Resources")]
    public class PlayerResources : ScriptableObject
    {
        public CreditModel money;
        public CreditModel yeeHawPoints;
        public CreditModel armyCapacity;
    }
}