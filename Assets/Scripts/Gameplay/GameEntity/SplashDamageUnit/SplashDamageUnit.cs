using Services.LevelStatisticsManager;
using Services;

public class SplashDamageUnit : Unit
{
    public int maxSplashAttackTargets;

    public override void setUpEntity(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager, PlayerBankModel playerBankModel, AudioManager audioManager)
    {
        base.setUpEntity(dataProvider, levelStatisticsManager, playerBankModel, audioManager);

        ((SplashDamageUnitModel)unitModel).maxSplashAttackTargets = maxSplashAttackTargets;
    }
}

