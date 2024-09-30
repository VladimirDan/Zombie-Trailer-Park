using CreaturesData;
using Services;

namespace Assets.Scripts.Services.SpawnManager.Factories
{
    
    public class StandartUnitSpawner : UnitSpawner        //Every unit that only attack single target or walk is standart unit
    {
        public override void SetParameters(Unit unitObject, UnitParametersData unitData)
        {
            unitObject.unitType = unitData.unitType;
            unitObject.OpponentLayer = unitData.OpponentLayer;
            unitObject.CreatureSpeed = unitData.CreatureSpeed;
            unitObject.CreatureHorizontalMovementDirection = unitData.CreatureHorizontalMovementDirection;
            unitObject.AttackDamage = unitData.AttackDamage;
            unitObject.AttackRange = unitData.AttackRange;
            unitObject.AttackSpeed = unitData.AttackSpeed;

            unitObject.YeeHawPointsDrop = unitData.YeeHawPointsDrop;
            unitObject.playerBank = playerBankModel;
        }

        public StandartUnitSpawner(DataProvider dataProvider, PlayerBankModel playerBankModel) : base(dataProvider, playerBankModel) { }
    }
}