using UnityEngine;

namespace GameParameters
{
    [CreateAssetMenu(fileName = "LevelEndMessage", menuName = "Level/LevelEndMessage")]
    public class LevelEndMessage : ScriptableObject
    {
        public string victoryMessage;
        public string loseMessage;
    }
}