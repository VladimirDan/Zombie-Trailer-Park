using System.ComponentModel;

namespace Enums
{
    public enum UnitType
    {
        [Description("Digger")] Digger,
        [Description("Angry Farmer")] Shooter,
        [Description("Boozer")] Boozer,
        [Description("Survivalist")] SurvivalistCar,
        [Description("Cleric")] Cleric,
        [Description("Zombie")] Zombie,
        [Description("Zombie Jumper")] ZombieJumper,
        [Description("Banshee")] Banshee,
        [Description("Giant")] Giant,
        [Description("Harvester")] Harvester,
        [Description("AirStrikePlane")] AirStrikePlane,
    }
}

