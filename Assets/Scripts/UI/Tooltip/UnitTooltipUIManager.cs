using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Gameplay.GameParameters;
using System;
using Enums;
using Services;

namespace UI.Buttons
{
    public class UnitTooltipUIManager : TooltipUIManager
    {
        public void Initialize(TooltipContent tooltipContent, GameObject tooltipPanelPrefab, RectTransform borderRectTransform,
            RectTransform buttonPanelRectTransform, int unitArmyCapacity)
        {
            this.borderRectTransform = borderRectTransform;
            this.buttonPanelRectTransform = buttonPanelRectTransform;
            this.tooltipContent = tooltipContent;
            titleImageObject = tooltipContent.armyCapacityUnitImageObject;
            this.titleImageObject = titleImageObject;
            tooltipPanel = Instantiate(tooltipPanelPrefab);
            tooltipPanelRectTransform = tooltipPanel.GetComponent<RectTransform>();
            tooltipPanelRectTransform.SetParent(borderRectTransform);
            
            warningMessageObject = tooltipPanelRectTransform.transform.Find(warningTextObjectPath).gameObject;
            
            SetTooltipContent(tooltipContent, unitArmyCapacity);
        }
        
        public void SetTooltipContent(TooltipContent tooltipContent, int unitArmyCapacity)
        {
            GameObject titleTextObject = tooltipPanelRectTransform.transform.Find(titleTextObjectPath).gameObject;
            GameObject descriptionTextObject = tooltipPanelRectTransform.transform.Find(descriptionTextObjectPath).gameObject;
            
            titleTextObject.GetComponent<TextMeshProUGUI>().text = tooltipContent?.title;
            
            SetUnitCapacityImage(unitArmyCapacity, titleImageObject);
            
            descriptionTextObject.GetComponent<TextMeshProUGUI>().text = tooltipContent?.tooltipDescriptionText;
        }
        
        public void SetUnitCapacityImage(int unitCapacity, GameObject armyCapacityUnit)
        {
            Transform titleImageObjectPanel = tooltipPanelRectTransform.transform.Find(titleImageObjectPath).gameObject.transform;
            
            for (int i = 0; i < unitCapacity; i++)
            {
                Transform armyCapacityUnitImageTransform = Instantiate(armyCapacityUnit).transform;
                armyCapacityUnitImageTransform.transform.SetParent(titleImageObjectPanel);
            }
        }
    }
}