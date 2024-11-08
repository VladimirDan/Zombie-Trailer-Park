using UnityEngine;
using Services;

namespace Gameplay.GameEntity.Entity
{
    public class EntityModel<T> : MonoBehaviour, IEntityModel<T>
    {
        public T EntityType { get; set; }
        public AudioManager AudioManager{ get; set; }
    }
}