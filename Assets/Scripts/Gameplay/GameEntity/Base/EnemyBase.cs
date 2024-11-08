using GameParameters;
using UnityEngine;
using Services;
using Services.LevelStatisticsManager;
using Services.Timer;
using UI;

namespace Gameplay.GameEntity.Base
{
    public class EnemyBase : BaseEntity
    {
        public override void SetUpEntity(DataProvider dataProvider, Timer timer, LevelStatisticsManager levelStatisticsManager, LevelUIManager levelUIManager, AudioManager audioManager)
        {
            base.SetUpEntity(dataProvider, timer, levelStatisticsManager, levelUIManager, audioManager);

            levelUIManager.InitializeHealthBarUI(levelUIManager.zombieBaseHealthBar, healthModel);
        }
        
        protected override void HandleMainBuildingDestruction()
        {
            LevelParameters currentLevelParameters = dataProvider.GetCurrentLevelParameters();

            levelStatisticsManager.SetLevelEndTime(timer.GetElapsedTime());
            levelUIManager.SwitchToLevelWinEndUI();
            currentLevelParameters.UpdateFastestLevelCompleteTime(timer.GetElapsedTime());
            
            coroutineRunner.Dispose();
            
            Debug.Log("Level end - Win");
        }
    }
}