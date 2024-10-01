using Services;
using UI.Buttons;
using UnityEngine;
using System;
using Gameplay.GameParameters;
using TMPro;
using UI.HealthBar;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MoneyPanelUIManager moneyUIPanelManager;
        [SerializeField] private UITextManager yeeHawUIPanelManager;
        [SerializeField] private ArmyCapacityPanelUIManager armyCapacityUIPanelManager;
        
        [SerializeField] private RectTransform tooltipsPanel;
        [SerializeField] private GameObject tooltipPanelPrefab;

        [SerializeField] private HealthBarUI playerBaseHealthBar;
        [SerializeField] private HealthBarUI zombieBaseHealthBar;

        private HealthModel playerBaseHealth;
        private HealthModel zombieBaseHealth;
        
        public event Action OnButtonsActivityChange;

        public void Initialize(HealthModel playerBaseHealth, HealthModel zombieBaseHealth)
        {
            this.playerBaseHealth = playerBaseHealth;
            this.zombieBaseHealth = zombieBaseHealth;
            
            playerBaseHealthBar.Initialize(playerBaseHealth);
            zombieBaseHealthBar.Initialize(zombieBaseHealth);
        }
        
        protected void Update()
        {
            OnButtonsActivityChange?.Invoke(); 
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

        public TooltipUIManager CreateTooltipPanel(ButtonWithCooldown button, TooltipContent tooltipContent)
        {
            RectTransform buttonPanel = (RectTransform)button.transform.parent;
            
            TooltipUIManager tooltipUIManager = button.gameObject.AddComponent<TooltipUIManager>();
            tooltipUIManager.Initialize(tooltipContent, tooltipPanelPrefab, tooltipsPanel, buttonPanel);
            return tooltipUIManager;
        }
    }
}