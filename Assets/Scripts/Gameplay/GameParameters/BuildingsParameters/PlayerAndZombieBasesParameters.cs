using UnityEngine;

namespace GameParameters
{
    [CreateAssetMenu(fileName = "PlayerAndZombieBasesParameters", menuName = "Level/PlayerAndZombieBasesParameters")]
    public class PlayerAndZombieBasesParameters : ScriptableObject
    {
        public BaseParameters playerBase;
        public BaseParameters zombieBase;
    }
}