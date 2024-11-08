using Assets.Scripts.StateMachine.States;
using Enums;
using Game.Code.Common.CoroutineRunner;
using Gameplay.GameEntity.Entity;
using UnityEngine;

namespace Gameplay.GameEntity.StateMachine.States
{
    public class UnitIdleState : IdleState<UnitType>
    {
        public override void Enter()
        {
            entityModel.AudioManager.AddUnitSpawnSoundArtist(entityModel.EntityType);
        }

        public override void Exit()
        {
            //entityModel.AudioManager.RemoveUnitSpawnSoundArtist(entityModel.EntityType);
        }

        public UnitIdleState(EntityModel<UnitType> entityModel, ICoroutineRunner _coroutineRunner) : base(entityModel, _coroutineRunner)
        {
        }
    }
}