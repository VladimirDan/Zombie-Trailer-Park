using UnityEngine;
using UnityEngine.Serialization;

namespace GameParameters
{
    [CreateAssetMenu(fileName = "LevelsParameters", menuName = "Level/LevelsParameters")]
    public class LevelsParameters : ScriptableObject
    {
        public LevelParameters[] levelsParameters;
    }
}