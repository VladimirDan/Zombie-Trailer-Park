using System.ComponentModel;

namespace Enums
{
    public enum BuildingType
    {
        [Description("Salvage Yard")] SalvageYard,
        [Description("Trailer")] Trailer,
        [Description("Farm House")] FarmHouse,
        [Description("Garage")] Garage,
        [Description("Still")] Still,
        [Description("Chapel")] Chapel,
    }
}