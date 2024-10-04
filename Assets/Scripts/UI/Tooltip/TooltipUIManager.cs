using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Gameplay.GameParameters;
using System;

namespace UI.Buttons
{
    public class TooltipUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private GameObject tooltipPanel;
        private TooltipContent tooltipContent;
        private GameObject warningMessagePanel;
        private GameObject warningMessageObject;
        
        public RectTransform borderRectTransform;
        
        public RectTransform buttonPanelRectTransform;
        private RectTransform tooltipPanelRectTransform;

        private string titleTextObjectPath = "TitlePanel/Title";
        private string titleImageObjectPath = "TitlePanel/UnitCapacityImage";
        private string descriptionTextObjectPath = "Description";
        private string warningTextObjectPath = "WarningText";

        public bool isMoneyEnough = false;
        public bool isYeeHawPointsEnough = false;
        public bool isArmyCapacityEnough = false;
        public bool isSummonRequirementAccomplished = false;

        public Action OnWarningTextUpdate;

        public void Initialize(TooltipContent tooltipContent, GameObject tooltipPanelPrefab, RectTransform borderRectTransform,
            RectTransform buttonPanelRectTransform)
        {
            this.borderRectTransform = borderRectTransform;
            this.buttonPanelRectTransform = buttonPanelRectTransform;
            this.tooltipContent = tooltipContent;
            tooltipPanel = Instantiate(tooltipPanelPrefab);
            tooltipPanelRectTransform = tooltipPanel.GetComponent<RectTransform>();
            tooltipPanelRectTransform.SetParent(borderRectTransform);
            
            warningMessageObject = tooltipPanelRectTransform.transform.Find(warningTextObjectPath).gameObject;
            
            SetTooltipContent(tooltipContent);
            //tooltipPanel.SetActive(false);
        }

        public void SetTooltipContent(TooltipContent tooltipContent)
        {
            GameObject titleTextObject = tooltipPanelRectTransform.transform.Find(titleTextObjectPath).gameObject;
            GameObject titleImageObject = tooltipPanelRectTransform.transform.Find(titleImageObjectPath).gameObject;
            GameObject descriptionTextObject = tooltipPanelRectTransform.transform.Find(descriptionTextObjectPath).gameObject;
            
            titleTextObject.GetComponent<TextMeshProUGUI>().text = tooltipContent?.title;
            
            if (tooltipContent.titlePic != null)
            {
                titleImageObject.GetComponent<Image>().sprite = tooltipContent?.titlePic;
            }
            else
            {
                titleImageObject.GetComponent<Image>().sprite = null;
            }
            
            descriptionTextObject.GetComponent<TextMeshProUGUI>().text = tooltipContent?.tooltipDescriptionText;
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnWarningTextUpdate?.Invoke();
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltipPanelRectTransform);
            SetTooltipPosition();
            tooltipPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltipPanel.SetActive(false);
        }

        void SetTooltipPosition()
        {
            float tooltipPanelLength = tooltipPanelRectTransform.rect.width / borderRectTransform.rect.width;
            float tooltipPanelHeight = tooltipPanelRectTransform.rect.height / borderRectTransform.rect.height;
            
            float maxX = buttonPanelRectTransform.anchorMin.x + tooltipPanelLength > 1 ?
                buttonPanelRectTransform.anchorMax.x : buttonPanelRectTransform.anchorMin.x + tooltipPanelLength;
            
            float minX = maxX - tooltipPanelLength;
            
            float maxY = buttonPanelRectTransform.anchorMax.y;
            float minY = (maxY - tooltipPanelHeight);
            
            tooltipPanelRectTransform.anchorMin = new Vector2(minX, minY);
            tooltipPanelRectTransform.anchorMax = new Vector2(maxX, maxY);
            
            tooltipPanelRectTransform.offsetMin = Vector2.zero;
            tooltipPanelRectTransform.offsetMax = Vector2.zero;
            
            tooltipPanelRectTransform.localScale = Vector3.one;
        }

        public void SetWarningMessage(string massage)
        {
            TextMeshProUGUI textMeshPro = warningMessageObject.GetComponent<TextMeshProUGUI>();

            textMeshPro.text = massage;
        }
        
        public string ConstructWarningTextForUnitButtonTooltip()
        {
            int problemsCount = 0;
            string result = tooltipContent.warningTextVariants.warningBeginning;
            
            if (!isSummonRequirementAccomplished)
            {
                problemsCount++;
                result+=tooltipContent.warningTextVariants.noNeededBuilding;
            }

            if (!isMoneyEnough)
            {
                problemsCount++;
                if(!isSummonRequirementAccomplished)
                    result+=tooltipContent.warningTextVariants.warningConnectionPart;
                
                result+=tooltipContent.warningTextVariants.noEnoughMoney;
            }
            
            if (!isArmyCapacityEnough)
            {
                problemsCount++;
                if(!isMoneyEnough || !isSummonRequirementAccomplished)
                    result+=tooltipContent.warningTextVariants.warningConnectionPart;
                
                result+=tooltipContent.warningTextVariants.noEnoughArmyCapacityAvailable;
            }
            
            result+=tooltipContent.warningTextVariants.warningEnd;
            result+=tooltipContent.title;

            return problemsCount == 0 ? "" : result;
        }
        
        public string ConstructWarningTextForBuildingButtonTooltip()
        {
            int problemsCount = 0;
            string result = tooltipContent.warningTextVariants.warningBeginning;
            
            if (!isSummonRequirementAccomplished)
            {
                problemsCount++;
                result+=tooltipContent.warningTextVariants.noNeededBuilding;
            }

            if (!isMoneyEnough)
            {
                problemsCount++;
                if(!isSummonRequirementAccomplished)
                    result+=tooltipContent.warningTextVariants.warningConnectionPart;
                
                result+=tooltipContent.warningTextVariants.noEnoughMoney;
            }
            
            result+=tooltipContent.warningTextVariants.warningEnd;
            result+=tooltipContent.title;
            
            
            return problemsCount == 0 ? "" : result;
        }
        
        public string ConstructWarningTextForYeeHawPowerButtonTooltip()
        {
            int problemsCount = 0;
            string result = tooltipContent.warningTextVariants.warningBeginning;

            if (!isYeeHawPointsEnough)
            {
                problemsCount++;
                
                result+=tooltipContent.warningTextVariants.noEnoughYeeHawPoints;
            }
            
            result+=tooltipContent.warningTextVariants.warningEnd;
            result+=tooltipContent.title;
            
            return problemsCount == 0 ? "" : result;
        }
        
        public void UpdateWarningTextForUnitTooltip()
        {
            string text = ConstructWarningTextForUnitButtonTooltip();
            SetWarningMessage(text);
        }
        
        public void UpdateWarningTextForBuildingTooltip()
        {
            string text = ConstructWarningTextForBuildingButtonTooltip();
            SetWarningMessage(text);
        }
        
        public void UpdateWarningTextForYeeHawPowerTooltip()
        {
            string text = ConstructWarningTextForYeeHawPowerButtonTooltip();
            SetWarningMessage(text);
        }
    }
}