using Services;
using UI.Buttons;
using UnityEngine;
using System;
using Enums;
using Gameplay.GameParameters;
using Services.LevelStatisticsManager;
using TMPro;
using UI.HealthBar;
using UnityEngine.UI;
using GameParameters;

namespace UI
{
    public class LevelUIManager : MonoBehaviour
    {
        [SerializeField] private MoneyPanelUIManager moneyUIPanelManager;
        [SerializeField] private UITextManager yeeHawUIPanelManager;
        [SerializeField] private ArmyCapacityPanelUIManager armyCapacityUIPanelManager;

        [SerializeField] private RectTransform tooltipsPanel;
        [SerializeField] private GameObject tooltipPanelPrefab;

        [SerializeField] public HealthBarUI playerBaseHealthBar;
        [SerializeField] public HealthBarUI zombieBaseHealthBar;
        
        [SerializeField] private GameSpeedControllerUIManager gameSpeedControllerUIManager;
        [SerializeField] private SoundButtonUIManager soundButtonUIManager;

        [SerializeField] private GameObject[] levelUICanvasObjects;
        [SerializeField] private Canvas gameEndCanvas;
        [SerializeField] private Image gameEndBackgroundImage;
        [SerializeField] private Sprite winEndBackgroundSprite;
        [SerializeField] private Sprite loseEndBackgroundSprite;
        [SerializeField] private GameObject timeTakenPanel;
        [SerializeField] private UITextManager levelEndMessagePanel;
        [SerializeField] private TimerUIManager levelEndTimePanel;
        [SerializeField] private UITextManager killedVillagersCountPanel;
        [SerializeField] private UITextManager killedZombiesCountPanel;

        [SerializeField] private Image UIOverlayImage;
        [SerializeField] private Canvas menuCanvas;

        private DataProvider dataProvider;

        [SerializeField] private Image levelBackGroundImage;

        private LevelStatisticsManager levelStatisticsManager;

        [SerializeField] private LevelEndMessage levelEndMessage;

        public event Action OnButtonsActivityChange;

        public void Initialize(DataProvider dataProvider, LevelStatisticsManager levelStatisticsManager)
        {
            this.dataProvider = dataProvider;
            this.levelStatisticsManager = levelStatisticsManager;

            SetLevelBackgroundImage(dataProvider.GetCurrentLevelParameters().levelBackground);
        }

        protected void Update()
        {
            OnButtonsActivityChange?.Invoke();
        }

        public void InitializeHealthBarUI(HealthBarUI healthBarUI, HealthModel health)
        {
            healthBarUI.Initialize(health);
        }
        
        public void UpdateMoneyInfo(PlayerBankModel playerBankModel)
        {
            moneyUIPanelManager.UpdateMoneyUIInfo(playerBankModel.money.GetCreditsAmount());
        }

        public void UpdateYeeHawPointsInfo(PlayerBankModel playerBankModel)
        {
            yeeHawUIPanelManager.UpdateText(playerBankModel.yeeHawPoints.GetCreditsAmount());
        }

        public void UpdateArmyCapacityInfo(PlayerBankModel playerBankModel)
        {
            armyCapacityUIPanelManager.UpdateArmyCapacityUIInfo(
                playerBankModel.armyCapacity.GetCreditsAmount(),
                playerBankModel.armyCapacity.GetCreditsCapacity()
            );
        }

        public void UpdateMoneyAndArmyInfo(PlayerBankModel playerBankModel)
        {
            UpdateMoneyInfo(playerBankModel);
            UpdateArmyCapacityInfo(playerBankModel);
        }

        public void UpdateAllResourcesInfo(PlayerBankModel playerBankModel)
        {
            UpdateMoneyInfo(playerBankModel);
            UpdateArmyCapacityInfo(playerBankModel);
            UpdateYeeHawPointsInfo(playerBankModel);
        }

        public void PlayButtonSummonEffect(ButtonWithCooldown button, float summonCooldown)
        {
            button.FillOverlay(summonCooldown);
        }

        public void ActivateButton(ButtonWithCooldown button)
        {
            if (!button.interactable)
            {
                button.SetActiveState();
            }
        }

        public void DeactivateButton(ButtonWithCooldown button)
        {
            if (button.interactable)
            {
                button.SetInactiveState();
            }
        }

        public void UpdateButtonPressMoneyPrice(ButtonWithCooldown button, float value)
        {
            TextMeshProUGUI priceObject = button.transform.parent.Find("Price").GetComponent<TextMeshProUGUI>();
            priceObject.text = $"${value}";
        }

        public void UpdateButtonPressYeeHawPointsPrice(ButtonWithCooldown button, float value)
        {
            TextMeshProUGUI priceObject = button.transform.parent.Find("Price").GetComponent<TextMeshProUGUI>();
            priceObject.text = $"{value}";
        }

        public TooltipUIManager CreateTooltipPanel(ButtonWithCooldown button, TooltipContent tooltipContent)
        {
            RectTransform buttonPanel = (RectTransform)button.transform.parent;

            TooltipUIManager tooltipUIManager = button.gameObject.AddComponent<TooltipUIManager>();
            tooltipUIManager.Initialize(tooltipContent, tooltipPanelPrefab, tooltipsPanel, buttonPanel);
            
            return tooltipUIManager;
        }
        
        public UnitTooltipUIManager CreateTooltipPanel(ButtonWithCooldown button, TooltipContent tooltipContent, UnitType unitType)
        {
            RectTransform buttonPanel = (RectTransform)button.transform.parent;

            UnitTooltipUIManager tooltipUIManager = button.gameObject.AddComponent<UnitTooltipUIManager>();
            int unitCapacity = (int)dataProvider.GetUnitCapacity(unitType);
            tooltipUIManager.Initialize(tooltipContent, tooltipPanelPrefab, tooltipsPanel, buttonPanel, unitCapacity);
            
            return tooltipUIManager;
        }

        public void SetLevelBackgroundImage(Sprite newSprite)
        {
            levelBackGroundImage.sprite = newSprite;
        }

        public void SwitchToLevelWinEndUI()
        {
            gameEndBackgroundImage.sprite = winEndBackgroundSprite;
            levelEndMessagePanel.UpdateText(levelEndMessage.victoryMessage);
            levelEndTimePanel.UpdateTimerUI(levelStatisticsManager.GetLevelEndTime());
            killedVillagersCountPanel.UpdateText(levelStatisticsManager.GetKilledVillagersCount());
            killedZombiesCountPanel.UpdateText(levelStatisticsManager.GetKilledZombiesCount());

            SwitchToGameEndUI();
        }
        
        public void SwitchToLevelLoseEndUI()
        {
            gameEndBackgroundImage.sprite = loseEndBackgroundSprite;
            levelEndMessagePanel.UpdateText(levelEndMessage.loseMessage);
            timeTakenPanel.SetActive(false);
            killedVillagersCountPanel.UpdateText(levelStatisticsManager.GetKilledVillagersCount());
            killedZombiesCountPanel.UpdateText(levelStatisticsManager.GetKilledZombiesCount());

            SwitchToGameEndUI();
        }
        
        public void SwitchToGameEndUI()
        {
            foreach (var gameObject in levelUICanvasObjects)
            {
                gameObject.SetActive(false);
            }

            gameEndCanvas.enabled = true;
            EnableUIOverlayImage(true);
        }
        
        public void ToggleMenuUI()
        {
            menuCanvas.enabled = menuCanvas.enabled == true ? false : true;
        }
        
        public void UpdateSpeedLevelIdentifiers(int speedLevel)
        {
            gameSpeedControllerUIManager.UpdateSpeedLevelIdentifiers(speedLevel);
        }

        public void EnableUIOverlayImage(bool isEnabled)
        {
            UIOverlayImage.enabled = isEnabled;
        }

        public void ToggleUIOverlay(Image overlay)
        {
            overlay.enabled = overlay.enabled == true ? false : true;
        }
    }
}