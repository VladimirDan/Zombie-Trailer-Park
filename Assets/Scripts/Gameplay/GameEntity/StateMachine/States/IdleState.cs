using Game.Code.Common.CoroutineRunner;
using Gameplay.GameEntity.Entity;

namespace Assets.Scripts.StateMachine.States
{
    public class IdleState<T> : EntityBehaviourState
    {
        protected EntityModel<T> entityModel;
        
        public override void Enter()
        {
            //entityModel.AudioManager.AddEntitySpawnSoundArtist<T>(entityModel.EntityType);
        }

        public override void Exit()
        {
        }

        public IdleState(EntityModel<T> entityModel, ICoroutineRunner _coroutineRunner) : base(_coroutineRunner)
        {
            this.entityModel = entityModel;
        }
    }
}
