using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Assets.Scripts.Gameplay.Entity.StateMachine;
using Gameplay.GameEntity.Entity;

namespace Assets.Scripts.StateMachine.States
{
    public class DeathState<T> : EntityBehaviourState
    {
        protected EntityModel<T> entityModel;
        
        public override void Enter()
        {
        }

        public override void Exit()
        {

        }

        public DeathState(ICoroutineRunner _coroutineRunner, EntityModel<T> entityModel) : base(_coroutineRunner)
        {
            this.entityModel = entityModel;
        }
    }
}
