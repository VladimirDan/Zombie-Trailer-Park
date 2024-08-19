using Game.Code.Common.CoroutineRunner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Entity.StateMachine.States
{
    public class JumpState : UnitBehaviourState
    {
        public override void Enter()
        {
            ((ZombieJumperModel)unitModel).Jump();
            ((ZombieJumperModel)unitModel).lastJumpTime = Time.time;
        }
        public override void Exit()
        {
            unitModel.Walk(exitSpeed, unitModel.CreatureHorizontalMovementDirection);
        }

        public JumpState(ZombieJumperModel _unitModel, ICoroutineRunner _coroutineRunner) : base(_unitModel, _coroutineRunner) { }
    }
}
