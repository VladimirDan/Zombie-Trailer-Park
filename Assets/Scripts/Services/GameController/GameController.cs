using UI;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game.Code.Common.CoroutineRunner;
using Gameplay;
using System;

namespace Services
{
    public class GameController : MonoBehaviour
    {
        private CoroutineRunner coroutineRunner;
        private DataProvider dataProvider;
        
        private PlayerBankModel playerBankModel;
        
        private MoneyPanelUIManager moneyUIPanelManager;
        private UITextManager yeeHawUIPanelManager;
        private ArmyCapacityPanelUIManager armyCapacityUIPanelManager;
        
        private GameSpeedController gameSpeedController;
        private SummonController summonController;

        private SummonCooldownController diggerSummonCooldownController;
        private SummonCooldownController shooterSummonCooldownController;
        private SummonCooldownController boozerSummonCooldownController;
        private SummonCooldownController survivalistCarSummonCooldownController;
        private SummonCooldownController clericSummonCooldownController;
        
        [SerializeField] private Button speedControllerButton;
        [SerializeField] private ButtonWithCooldown diggerSummonButton;
        [SerializeField] private ButtonWithCooldown shooterSummonButton;
        [SerializeField] private ButtonWithCooldown boozerSummonButton;
        [SerializeField] private ButtonWithCooldown survivalistCarSummonButton;
        [SerializeField] private ButtonWithCooldown clericSummonButton;
        
        public void Initialize(CoroutineRunner coroutineRunner, DataProvider dataProvider, SummonController summonController)
        {
            this.coroutineRunner = coroutineRunner;
            this.dataProvider = dataProvider;
            this.summonController = summonController;

            moneyUIPanelManager = dataProvider.GetMoneyPanelManager();
            yeeHawUIPanelManager = dataProvider.GetYeeHawUIPanelManager();
            armyCapacityUIPanelManager = dataProvider.GetArmyCapacityUIPanelManager();
            
            CreditModel playerStartMoney = new CreditModel(dataProvider.GetPlayerStartMoney().GetCreditsAmount(), 0);
            moneyUIPanelManager.UpdateMoneyUIInfo(playerStartMoney.GetCreditsAmount());
            
            CreditModel playerStartYeeHawPoints = new CreditModel(dataProvider.GetPlayerStartYeeHawPoints().GetCreditsAmount(), 0);
            yeeHawUIPanelManager.UpdateText(playerStartYeeHawPoints.GetCreditsAmount());
            
            CreditModel playerStartArmyCapacity = new CreditModel(dataProvider.GetPlayerStartArmyCapacity().GetCreditsAmount(), 
                dataProvider.GetPlayerStartArmyCapacity().GetCreditsCapacity());
            armyCapacityUIPanelManager.UpdateArmyCapacityUIInfo(playerStartArmyCapacity.GetCreditsAmount(), playerStartArmyCapacity.GetCreditsCapacity());

            playerBankModel = new PlayerBankModel(playerStartMoney, playerStartYeeHawPoints, playerStartArmyCapacity);
            
            gameSpeedController = new GameSpeedController();

            if (speedControllerButton != null)
            {
                speedControllerButton.onClick.AddListener(OnSpeedButtonClick);
            }

            AddSummonListener(diggerSummonButton, UnitType.Digger);
            AddSummonListener(shooterSummonButton, UnitType.Shooter);
            AddSummonListener(boozerSummonButton, UnitType.Boozer);
            AddSummonListener(survivalistCarSummonButton, UnitType.SurvivalistCar);
            AddSummonListener(clericSummonButton, UnitType.Cleric);
        }

        public void AddSummonListener(ButtonWithCooldown button, UnitType unitType)
        {
            SummonCooldownController cooldownController = new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(unitType));
            if (button != null)
            {
                button.onClick.AddListener(() => OnUnitSummonButtonClick(unitType, button, cooldownController));
                button.OnActivityChange += () => CheckoutSummonButtonActivity(button, () => CheckUnitSummonAvailability(unitType));
            }
        }
        
        public void OnSpeedButtonClick()
        {
            gameSpeedController.ChangeSpeed();
        }
        
        public void OnUnitSummonButtonClick(UnitType unitType, ButtonWithCooldown button,  SummonCooldownController cooldownController)
        {
            float cooldown = dataProvider.GetSummonCooldownTiming(unitType);
            float unitPrice = dataProvider.GetUnitPrice(unitType);
            float unitCapacity = dataProvider.GetUnitCapacity(unitType);
            
            playerBankModel.money.ReduceCredits(unitPrice);
            playerBankModel.armyCapacity.AddCredits(unitCapacity);
            
            moneyUIPanelManager.UpdateMoneyUIInfo(playerBankModel.money.GetCreditsAmount());
            armyCapacityUIPanelManager.UpdateArmyCapacityUIInfo(playerBankModel.armyCapacity.GetCreditsAmount(), playerBankModel.armyCapacity.GetCreditsCapacity());
            
            IEnumerator action(UnitType unitType)
            {
                button.FillOverlay(cooldown);
                yield return new WaitForSeconds(cooldown);
            
                summonController.SummonUnit(unitType);
            }
            
            cooldownController.OnCallOfActionWithCooldown(action(unitType));
        }

        private bool CheckUnitSummonAvailability(UnitType unitType)
        {
            float unitPrice = dataProvider.GetUnitPrice(unitType);
            float unitCapacity = dataProvider.GetUnitCapacity(unitType);

            float playerMoney = playerBankModel.money.GetCreditsAmount();
            float playerArmyCapacity = playerBankModel.armyCapacity.GetCreditsCapacity() - playerBankModel.armyCapacity.GetCreditsAmount();

            bool hasEnoughMoney = playerMoney - unitPrice >= 0;
            bool hasEnoughArmyCapacity = playerArmyCapacity - unitCapacity >= 0;

            return hasEnoughMoney && hasEnoughArmyCapacity;
        }
        
        private void CheckoutSummonButtonActivity(ButtonWithCooldown button, Func<bool> conditionCheckForActivity)
        {
            if (conditionCheckForActivity.Invoke())
            {
                if (!button.interactable)
                {
                    button.SetActiveState();
                }
            }
            else
            {
                if (button.interactable)
                {
                    button.SetInactiveState();
                }
            }
        }
        
        private void OnDestroy()
        {
            if (speedControllerButton != null)
            {
                speedControllerButton.onClick.RemoveListener(gameSpeedController.ChangeSpeed);
            }
        }
    }
}