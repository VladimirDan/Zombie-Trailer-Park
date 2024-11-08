using Enums;
using GameParameters;

namespace Services.LevelStatisticsManager
{
    public class LevelStatisticsManager
    {
        public LevelStatistics levelStatistics;
        
        public LevelStatisticsManager()
        {
            levelStatistics = new LevelStatistics();
        }

        public void SetLevelEndTime(float seconds)
        {
            levelStatistics.levelCompleteTime = seconds;
        }

        public float GetLevelEndTime()
        {
            return levelStatistics.levelCompleteTime;
        }

        public void UpdateUnitsKilledStatistic(UnitType unitType)
        {
            switch (unitType)
            {
                case UnitType.Digger:
                    levelStatistics.diggersKilled++;
                    break;
                case UnitType.Shooter:
                    levelStatistics.shootersKilled++;
                    break;
                case UnitType.Boozer:
                    levelStatistics.boozersKilled++;
                    break;
                case UnitType.SurvivalistCar:
                    levelStatistics.survivalistCarsKilled++;
                    break;
                case UnitType.Cleric:
                    levelStatistics.clericsKilled++;
                    break;
                case UnitType.Zombie:
                    levelStatistics.regularZombiesKilled++;
                    break;
                case UnitType.ZombieJumper:
                    levelStatistics.zombieJumpersKilled++;
                    break;
                case UnitType.Banshee:
                    levelStatistics.bansheesKilled++;
                    break;
                case UnitType.Giant:
                    levelStatistics.giantsKilled++;
                    break;
                default:
                    break;
            }
        }

        public int GetKilledVillagersCount()
        {
            int killedVillagersCount =
                levelStatistics.diggersKilled + levelStatistics.shootersKilled + levelStatistics.boozersKilled 
                + levelStatistics.survivalistCarsKilled + levelStatistics.clericsKilled;
            
            return killedVillagersCount;
        }
        
        public int GetKilledZombiesCount()
        {
            int killedZombiesCount =
                levelStatistics.regularZombiesKilled + levelStatistics.zombieJumpersKilled
                + levelStatistics.bansheesKilled + levelStatistics.giantsKilled ;
            
            return killedZombiesCount;
        }
    }
}