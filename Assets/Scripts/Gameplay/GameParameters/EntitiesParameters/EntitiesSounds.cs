using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.GameParameters.EntitiesParameters
{
    [CreateAssetMenu(fileName = "EntitiesSounds", menuName = "Entities/EntitiesSounds")]
    public class EntitiesSounds : ScriptableObject
    {
        [SerializeField, Header("Attack Sounds"), Space]
        public AudioClip[] diggerAttackSounds;
        public AudioClip[] shooterAttackSounds;
        public AudioClip[] boozerAttackSounds;
        public AudioClip[] survivalistCarAttackSounds;
        public AudioClip[] clericAttackSounds;
        public AudioClip[] zombieAttackSounds;
        public AudioClip[] zombieJumperAttackSounds;
        public AudioClip[] bansheeAttackSounds;
        public AudioClip[] giantAttackSounds;
        
        [SerializeField, Header("Spawn Sounds"), Space]
        public AudioClip[] clericSpawnSounds;
        public AudioClip[] bansheeSpawnSounds;
        public AudioClip[] giantSpawnSounds;
        public AudioClip[] yeeHawSpawnSounds;
        public AudioClip[] crowdSpawnSounds;
        public AudioClip[] airstrikeSpawnSounds;
        public AudioClip[] zombieSpawnSounds;
        
        [SerializeField, Header("Death Sounds"), Space]
        public AudioClip[] diggerDeathSounds;
        public AudioClip[] shooterDeathSounds;
        public AudioClip[] boozerDeathSounds;
        public AudioClip[] survivalistCarDeathSounds;
        public AudioClip[] clericDeathSounds;
    }
}