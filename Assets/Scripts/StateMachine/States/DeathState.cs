using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine.States
{
    internal class DeathState : EntitieBehaviourState
    {
        public Coroutine attackCoroutine;

        public override void Enter()
        {
            
        }

        public override void Exit()
        {

        }

        public DeathState(Entitie _entitieModel) : base(_entitieModel) { }
    
    }
}
