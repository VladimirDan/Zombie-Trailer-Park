using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Game.Code.Common.CoroutineRunner;
using Assets.Scripts.Gameplay.Entity.StateMachine;

namespace Assets.Scripts.StateMachine.States
{
    internal class DeathState : EntityBehaviourState
    {

        public override void Enter()
        {
            
        }

        public override void Exit()
        {

        }

        public DeathState(ICoroutineRunner _coroutineRunner) : base(_coroutineRunner) { }

    }
}
