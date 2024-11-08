using Services.LevelStatisticsManager;
using Gameplay.GameEntity.Boozer;
using CreaturesData;
using Services;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class BombThrowerUnitSpawner : StandartUnitSpawner
    {
        public override void SetParameters(Unit unitObject, UnitParametersData unitData)
        {
            base.SetParameters(unitObject, unitData);
            ((Boozer)unitObject).bombPrefab = ((BombThrowerUnitParameters)unitData).bombPrefab;
            ((Boozer)unitObject).bombThrowHeight = ((BombThrowerUnitParameters)unitData).bombThrowHeight;
        }

        public BombThrowerUnitSpawner(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, PlayerBankModel playerBankModel) 
            : base(dataProvider, levelStatisticsManager, playerBankModel) { }
    }
}