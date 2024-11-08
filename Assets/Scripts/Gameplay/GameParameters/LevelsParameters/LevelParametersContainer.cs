using UnityEngine;
using UnityEngine.Serialization;

namespace GameParameters
{
    [CreateAssetMenu(fileName = "LevelParametersContainer", menuName = "Level/LevelParametersContainer")]
    public class LevelParametersContainer : ScriptableObject
    {
        public LevelParameters levelParameters;
    }
}