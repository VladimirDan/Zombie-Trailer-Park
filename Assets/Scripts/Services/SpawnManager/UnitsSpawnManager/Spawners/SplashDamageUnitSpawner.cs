using Services.LevelStatisticsManager;
using CreaturesData;
using Services;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    public class SplashDamageUnitSpawner : StandartUnitSpawner
    {
        public override void SetParameters(Unit unitObject, UnitParametersData unitData)
        {
            base.SetParameters(unitObject, unitData);
            ((SplashDamageUnit)unitObject).maxSplashAttackTargets = ((SplashDamageUnitParametersData)unitData).maxSplashAttackTargets;
        }

        public SplashDamageUnitSpawner(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, PlayerBankModel playerBankModel) 
            : base(dataProvider, levelStatisticsManager, playerBankModel) { }
    }
}
