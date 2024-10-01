using Services;
using Enums;

namespace Gameplay.Building
{
    public class TrailerBuilding : Building
    {
        private PlayerBankModel playerBankModel;
        
        public TrailerBuilding(DataProvider dataProvider, PlayerBankModel playerBankModel) : base(dataProvider)
        {
            this.playerBankModel = playerBankModel;
        }

        public override void Spawn()
        {
            base.Spawn();
            playerBankModel.UpgradeArmyCapacity();
        }
    }
}