using Services;
using UI.Buttons;
using UnityEngine;
using System;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private MoneyPanelUIManager moneyUIPanelManager;
        [SerializeField] private UITextManager yeeHawUIPanelManager;
        [SerializeField] private ArmyCapacityPanelUIManager armyCapacityUIPanelManager;
        
        public event Action OnButtonsActivityChange;
        
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
    }
}