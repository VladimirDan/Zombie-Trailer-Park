using Assets.Scripts.StateMachine.States;
using Enums;
using Game.Code.Common.CoroutineRunner;
using Gameplay.GameEntity.Entity;

namespace Gameplay.GameEntity.StateMachine.States
{
    public class UnitDeathState : DeathState<UnitType>
    {
        public override void Enter()
        {
            entityModel.AudioManager.AddUnitDeathSoundArtist(entityModel.EntityType);
        }

        public override void Exit()
        {
            //entityModel.AudioManager.RemoveUnitDeathSoundArtist(entityModel.EntityType);
        }

        public UnitDeathState(ICoroutineRunner _coroutineRunner, EntityModel<UnitType> entityModel) : base(_coroutineRunner, entityModel)
        {
        }
    }
}