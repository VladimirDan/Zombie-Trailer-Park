using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Game.Code.Common.CoroutineRunner;

namespace Assets.Scripts.StateMachine.States
{
    internal class AfkState : EntityBehaviourState
    {
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
        public AfkState(ICoroutineRunner _coroutineRunner) : base(_coroutineRunner) { }
    }
}
