using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine.States
{
    internal class AFK : EntitieBehaviourState
    {
        public override void Enter()
        {
        }

        public override void Exit()
        {
        }
        public AFK(Entitie _entitieModel) : base(_entitieModel) { }
    }
}
