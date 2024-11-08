using Assets.Scripts.StateMachine.States;
using GameParameters;
using UnityEngine;
using Services;
using Services.LevelStatisticsManager;
using Services.Timer;
using UI;

namespace Gameplay.GameEntity.Base
{
    public class PlayerBase : BaseEntity
    {
        public override void SetUpEntity(DataProvider dataProvider, Timer timer, LevelStatisticsManager levelStatisticsManager, LevelUIManager levelUIManager, AudioManager audioManager)
        {
            base.SetUpEntity(dataProvider, timer, levelStatisticsManager, levelUIManager, audioManager);
            
            levelUIManager.InitializeHealthBarUI(levelUIManager.playerBaseHealthBar, healthModel);
        }
        
        protected override void HandleMainBuildingDestruction()
        {
            LevelParameters currentLevelParameters = dataProvider.GetCurrentLevelParameters();

            levelStatisticsManager.SetLevelEndTime(timer.GetElapsedTime());
            levelUIManager.SwitchToLevelLoseEndUI();
            
            coroutineRunner.Dispose();
            
            Debug.Log("Level end");
        }
    }
}