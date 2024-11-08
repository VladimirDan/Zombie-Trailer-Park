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
        protected GameObject tooltipPanel;
        protected TooltipContent tooltipContent;
        protected GameObject warningMessagePanel;
        protected GameObject warningMessageObject;
        protected GameObject titleImageObject;
        
        public RectTransform borderRectTransform;
        
        public RectTransform buttonPanelRectTransform;
        protected RectTransform tooltipPanelRectTransform;

        protected string titleTextObjectPath = "TitlePanel/Title";
        protected string titleImageObjectPath = "TitlePanel/UnitCapacityImage";
        protected string descriptionTextObjectPath = "Description";
        protected string warningTextObjectPath = "WarningText";

        public bool isMoneyEnough = false;
        public bool isYeeHawPointsEnough = false;
        public bool isArmyCapacityEnough = false;
        public bool isSummonRequirementAccomplished = false;

        public Action OnWarningTextUpdate;

        public virtual void Initialize(TooltipContent tooltipContent, GameObject tooltipPanelPrefab, RectTransform borderRectTransform,
            RectTransform buttonPanelRectTransform)
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
            
            SetTooltipContent(tooltipContent);
        }

        public virtual void SetTooltipContent(TooltipContent tooltipContent)
        {
            GameObject titleTextObject = tooltipPanelRectTransform.transform.Find(titleTextObjectPath).gameObject;
            GameObject descriptionTextObject = tooltipPanelRectTransform.transform.Find(descriptionTextObjectPath).gameObject;
            
            titleTextObject.GetComponent<TextMeshProUGUI>().text = tooltipContent?.title;

            SetTitlePic(titleImageObject);
            
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

        public virtual void SetTitlePic(GameObject titlePic)
        {
            if (titlePic == null)
            {
                return;
            }
            Transform titleImageObjectPanel = tooltipPanelRectTransform.transform.Find(titleImageObjectPath).gameObject.transform;
            
            Transform imageTransform = Instantiate(titlePic).transform;
            imageTransform.transform.SetParent(titleImageObjectPanel);
            
        }
    }
}