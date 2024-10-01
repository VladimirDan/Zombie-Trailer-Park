using UnityEngine;

namespace LevelsParameters
{
    [CreateAssetMenu(fileName = "PlayerAndZombieBasesParameters", menuName = "Level/PlayerAndZombieBasesParameters")]
    public class PlayerAndZombieBasesParameters : ScriptableObject
    {
        public BaseParameters playerBase;
        public BaseParameters zombieBase;
    }
}