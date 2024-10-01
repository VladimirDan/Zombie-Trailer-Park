using UnityEngine;

namespace Gameplay.GameParameters
{
    [CreateAssetMenu(fileName = "TooltipsContent", menuName = "TooltipsContent/TooltipsContent")]
    public class TooltipsContent : ScriptableObject
    {
        public TooltipContent[] unitsTooltips;
        public TooltipContent[] buildingsTooltips;
        public TooltipContent[] yeeHawActionsTooltips;
    }
}