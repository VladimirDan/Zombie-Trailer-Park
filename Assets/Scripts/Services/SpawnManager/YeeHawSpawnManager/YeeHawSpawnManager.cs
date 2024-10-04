using Enums;

namespace Services.SpawnManager
{
    public class YeeHawSpawnManager
    {
        private UnitsSpawnManager unitsSpawnManager;

        public void SummonYeeHawPower(YeeHawActionType yeeHawActionType)
        {
            switch (yeeHawActionType)
            {
                case YeeHawActionType.Harvester:
                    SpawnHarvester();
                    break;
                default:
                    break;
            }
        }
        
        public void SpawnHarvester()
        {
            unitsSpawnManager.SpawnUnitOnRandomRow(UnitType.Harvester, unitsSpawnManager.baseSpawnPosition); 
        }

        public YeeHawSpawnManager(UnitsSpawnManager unitsSpawnManager)
        {
            this.unitsSpawnManager = unitsSpawnManager;
        }
    }
}