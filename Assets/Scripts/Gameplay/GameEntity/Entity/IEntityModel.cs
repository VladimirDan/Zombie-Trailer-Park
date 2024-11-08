namespace Gameplay.GameEntity.Entity
{
    public interface IEntityModel<T>
    {
        public T EntityType { get; set; }
    }
}