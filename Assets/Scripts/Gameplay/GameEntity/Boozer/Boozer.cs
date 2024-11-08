using Services.LevelStatisticsManager;
using UnityEngine;
using Services;

namespace Gameplay.GameEntity.Boozer
{
    public class Boozer : Unit
    {
        public GameObject bombPrefab;
        public float bombThrowHeight;
        
        public override void setUpEntity(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, PlayerBankModel playerBankModel, AudioManager audioManager)
        {
            base.setUpEntity(dataProvider, levelStatisticsManager, playerBankModel, audioManager);

            ((BoozerModel)unitModel).bombPrefab = bombPrefab;
            ((BoozerModel)unitModel).bombThrowHeight = bombThrowHeight;
        }
    }
}