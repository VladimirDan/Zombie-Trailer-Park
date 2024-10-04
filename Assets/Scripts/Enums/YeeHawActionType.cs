using System.ComponentModel;

namespace Enums
{
    public enum YeeHawActionType
    {
        [Description("Angry Mob")] CrowdSummon,
        [Description("Air Strike")] Bombardment,
        [Description("Harvester")] Harvester,
    }
}