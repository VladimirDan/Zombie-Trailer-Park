using Services;
using Enums;

namespace Gameplay.Building
{
    public class SalvageYardBuilding : Building
    {
        private PlayerBankModel playerBankModel;
        
        public SalvageYardBuilding(DataProvider dataProvider, PlayerBankModel playerBankModel) : base(dataProvider)
        {
            this.playerBankModel = playerBankModel;
        }
        
        public override void Spawn()
        {
            base.Spawn();
            playerBankModel.StartMoneyIncomeCycle();
        }
    }
}