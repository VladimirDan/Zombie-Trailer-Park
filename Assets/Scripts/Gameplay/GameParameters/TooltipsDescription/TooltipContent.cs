using JetBrains.Annotations;
using UnityEngine;

namespace Gameplay.GameParameters
{
    [CreateAssetMenu(fileName = "TooltipContent", menuName = "TooltipsContent/TooltipContent")]
    public class TooltipContent : ScriptableObject
    {
        public string title;
        [CanBeNull] public Sprite titlePic;
        public string tooltipDescriptionText;
        public WarningTextVariants warningTextVariants;
    }
}