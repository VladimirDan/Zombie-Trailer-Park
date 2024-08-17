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
        protected IEntityModel entityModel;

        public UnitBehaviourState(IEntityModel _entityModel, ICoroutineRunner _coroutineRunner) : base(_coroutineRunner)
        {
            entityModel = _entityModel;
        }
    }
}
