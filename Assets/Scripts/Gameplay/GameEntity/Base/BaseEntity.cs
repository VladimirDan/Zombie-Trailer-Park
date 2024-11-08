using Assets.Scripts.StateMachine.States;
using GameParameters;
using UnityEngine;
using Services;
using Services.LevelStatisticsManager;
using Services.Timer;
using UI;

namespace Gameplay.GameEntity.Base
{
    public class BaseEntity : Entity<string>
    {
        protected Timer timer;
        protected LevelUIManager levelUIManager;
        public override void CheckoutCurrentState()
        {
            if (!healthModel.IsAlive())
            {
                stateMachine.ChangeCurrentState(new DeathState<string>(coroutineRunner, entityModel));
                Die();
            }
        }

        protected virtual void HandleMainBuildingDestruction()
        {
            levelStatisticsManager.SetLevelEndTime(timer.GetElapsedTime());
            coroutineRunner.Dispose();
            
            Debug.Log("Level end");
        }

        public virtual void SetUpEntity(DataProvider dataProvider, Timer timer, LevelStatisticsManager levelStatisticsManager, LevelUIManager levelUIManager, AudioManager audioManager)
        {
            base.SetUpEntity(dataProvider, levelStatisticsManager, audioManager);
            this.timer = timer;
            this.levelStatisticsManager = levelStatisticsManager;
            this.levelUIManager = levelUIManager;
            
            onDestroy += HandleMainBuildingDestruction;
        }

        public void Initialize(DataProvider dataProvider, Timer timer, LevelStatisticsManager levelStatisticsManager, LevelUIManager levelUIManager, AudioManager audioManager)
        {
            SetUpEntity(dataProvider, timer, levelStatisticsManager, levelUIManager, audioManager);
            this.enabled = true;
        }
    }
}
