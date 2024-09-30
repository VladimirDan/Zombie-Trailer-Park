using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


namespace UI.Buttons
{
    public class TooltipUIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject tooltipPanelPrefab;
        private GameObject tooltipPanel;
       
        private GameObject warningMessagePanel;
        private GameObject warningMessageObject;
        private RectTransform warningMessageRectTransform;
        
        public RectTransform borderRectTransform;
        
        public RectTransform buttonPanelRectTransform;
        private RectTransform tooltipPanelRectTransform;

        void Start()
        {
            tooltipPanel = Instantiate(tooltipPanelPrefab);
      
            tooltipPanelRectTransform = tooltipPanel.GetComponent<RectTransform>();
            tooltipPanelRectTransform.SetParent(borderRectTransform);
            
            SetTooltipPosition();
           
            
            warningMessageObject = tooltipPanelRectTransform.transform.Find("WarningText").gameObject;
            warningMessageRectTransform = warningMessageObject.GetComponent<RectTransform>();
            
            tooltipPanel.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltipPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltipPanel.SetActive(false);
        }

        void SetTooltipPosition()
        {
            float tooltipPanelLength = tooltipPanelRectTransform.anchorMax.x - tooltipPanelRectTransform.anchorMin.x;
            float tooltipPanelHeight = tooltipPanelRectTransform.anchorMax.y - tooltipPanelRectTransform.anchorMin.y;

            float dd = tooltipPanelRectTransform.rect.height / borderRectTransform.rect.height;
            
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
        
        public void AddWarningMessage(string massage)
        {
            TextMeshProUGUI textMeshPro = warningMessageObject.GetComponent<TextMeshProUGUI>();

            textMeshPro.text = textMeshPro.text + massage;
        }
    }
}