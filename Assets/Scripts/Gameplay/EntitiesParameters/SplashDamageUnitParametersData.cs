using UnityEngine;

namespace CreaturesData
{

    [CreateAssetMenu(fileName = "SplashDamageUnitParameters", menuName = "Entities/Splash Damage Unit Parameters")]

    public class SplashDamageUnitParametersData : UnitParametersData
    {
        public int maxSplashAttackTargets;
    }
}
