using Services;

public class SplashDamageUnit : Unit
{
    public int maxSplashAttackTargets;

    public override void setUpEntity(DataProvider dataProvider, PlayerBankModel playerBankModel)
    {
        base.setUpEntity(dataProvider, playerBankModel);

        ((SplashDamageUnitModel)unitModel).maxSplashAttackTargets = maxSplashAttackTargets;
    }
}

