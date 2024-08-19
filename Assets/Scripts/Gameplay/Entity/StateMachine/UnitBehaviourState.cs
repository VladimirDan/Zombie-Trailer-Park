using Game.Code.Common.CoroutineRunner;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Gameplay.Entity.StateMachine
{
    public abstract class UnitBehaviourState : EntityBehaviourState
    {
        protected IUnitModel unitModel;
        public float exitSpeed = 0f;

        public UnitBehaviourState(IUnitModel _entityModel, ICoroutineRunner _coroutineRunner) : base(_coroutineRunner)
        {
            unitModel = _entityModel;
        }
    }
}
