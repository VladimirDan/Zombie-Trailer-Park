using System.ComponentModel;
using Enums;

namespace Assets.Scripts.Extensions
{
    public static class EnumExtensions
    {
        public static string EnumToName(this UnitType unitType)
        {
            var type = unitType.GetType();
            var memInfo = type.GetMember(unitType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        
            return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : unitType.ToString();
        }
        
        public static string EnumToName(this BuildingType buildingType)
        {
            var type = buildingType.GetType();
            var memInfo = type.GetMember(buildingType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        
            return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : buildingType.ToString();
        }
        
        public static string EnumToName(this YeeHawActionType yeeHawActionType)
        {
            var type = yeeHawActionType.GetType();
            var memInfo = type.GetMember(yeeHawActionType.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        
            return attributes.Length > 0 ? ((DescriptionAttribute)attributes[0]).Description : yeeHawActionType.ToString();
        }
    }
}