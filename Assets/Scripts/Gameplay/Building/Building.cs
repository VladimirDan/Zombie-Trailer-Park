using Services;
using Enums;
using UnityEngine;

namespace Gameplay.Building
{
    public class Building
    {
        protected DataProvider dataProvider;
        protected bool isBuilt;
        protected int count;

        public Building(DataProvider dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        public virtual void Spawn()
        {
            isBuilt = true;
        }
        
        public virtual void OccupySlot()
        {
            count++;
        }

        public bool HasFreeSlot(BuildingType buildingType)
        {
            return count < dataProvider.GetBuildingCountLimit(buildingType);
        }
        
        public bool IsBuilt()
        {
            return isBuilt;
        }
    }
}