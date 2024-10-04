using UI;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game.Code.Common.CoroutineRunner;
using Gameplay;
using System;
using Enums;

namespace Services
{
    public class GameController : MonoBehaviour
    {
        private CoroutineRunner coroutineRunner;
        private DataProvider dataProvider;
        private UIManager uiManager;
        private PlayerBankModel playerBankModel;
        
        private GameSpeedController gameSpeedController;
        private SummonManager summonManager;
        
        [SerializeField] private Button speedControllerButton;
        [SerializeField] private ButtonWithCooldown diggerSummonButton;
        [SerializeField] private ButtonWithCooldown shooterSummonButton;
        [SerializeField] private ButtonWithCooldown boozerSummonButton;
        [SerializeField] private ButtonWithCooldown survivalistCarSummonButton;
        [SerializeField] private ButtonWithCooldown clericSummonButton;

        [SerializeField] private ButtonWithCooldown salvageYardSummonButton;
        [SerializeField] private ButtonWithCooldown trailerSummonButton;
        [SerializeField] private ButtonWithCooldown farmHouseSummonButton;
        [SerializeField] private ButtonWithCooldown stillSummonButton;
        [SerializeField] private ButtonWithCooldown garageSummonButton;
        [SerializeField] private ButtonWithCooldown chapelSummonButton;
        
        [SerializeField] private ButtonWithCooldown harvesterSummonButton;
        [SerializeField] private ButtonWithCooldown bombardmentSummonButton;
        [SerializeField] private ButtonWithCooldown crowdSummonSummonButton;

        public void Initialize(CoroutineRunner coroutineRunner, DataProvider dataProvider, SummonManager summonManager,
            UIManager uiManager, PlayerBankModel playerBankModel)
        {
            this.coroutineRunner = coroutineRunner;
            this.dataProvider = dataProvider;
            this.summonManager = summonManager;
            this.uiManager = uiManager;
            this.summonManager = summonManager;
            this.playerBankModel = playerBankModel;

            gameSpeedController = new GameSpeedController();

            speedControllerButton.onClick.AddListener(OnSpeedButtonClick);

            AddSummonListener(diggerSummonButton, UnitType.Digger);
            AddSummonListener(shooterSummonButton, UnitType.Shooter);
            AddSummonListener(boozerSummonButton, UnitType.Boozer);
            AddSummonListener(survivalistCarSummonButton, UnitType.SurvivalistCar);
            AddSummonListener(clericSummonButton, UnitType.Cleric);

            AddSummonListener(salvageYardSummonButton, BuildingType.SalvageYard);
            AddSummonListener(trailerSummonButton, BuildingType.Trailer);
            AddSummonListener(farmHouseSummonButton, BuildingType.FarmHouse);
            AddSummonListener(stillSummonButton, BuildingType.Still);
            AddSummonListener(garageSummonButton, BuildingType.Garage);
            AddSummonListener(chapelSummonButton, BuildingType.Chapel);

            AddSummonListener(harvesterSummonButton, YeeHawActionType.Harvester);
        }

        public void AddSummonListener(ButtonWithCooldown button, UnitType unitType)
        {
            uiManager.UpdateButtonPressMoneyPrice(button, dataProvider.GetSummonPrice(unitType));
            TooltipUIManager tooltipUIManager = uiManager.CreateTooltipPanel(button, dataProvider.GetTooltipContent(unitType));
            tooltipUIManager.OnWarningTextUpdate += tooltipUIManager.UpdateWarningTextForUnitTooltip;
            
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(unitType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(unitType, tooltipUIManager);
            Action checkOutButtonState = () => CheckoutSummonButtonActivity(button, checkFunction);

            if (button != null)
            {
                button.onClick.AddListener(() => summonManager.PayForSummon(unitType));
                button.onClick.AddListener(() => OnSummonButtonClick(unitType, button, cooldownController));
                uiManager.OnButtonsActivityChange += checkOutButtonState;
            }
        }

        public void AddSummonListener(ButtonWithCooldown button, BuildingType buildingType)
        {
            uiManager.UpdateButtonPressMoneyPrice(button, dataProvider.GetSummonPrice(buildingType));
            TooltipUIManager tooltipUIManager = uiManager.CreateTooltipPanel(button, dataProvider.GetTooltipContent(buildingType));
            tooltipUIManager.OnWarningTextUpdate += tooltipUIManager.UpdateWarningTextForBuildingTooltip;
            
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(buildingType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(buildingType, tooltipUIManager);
            Action checkOutButtonState = () => CheckoutSummonButtonActivity(button, checkFunction);

            if (button != null)
            {
                button.onClick.AddListener(() => summonManager.PayForSummon(buildingType));
                button.onClick.AddListener(() => OnSummonButtonClick(buildingType, button, cooldownController));
                uiManager.OnButtonsActivityChange += checkOutButtonState;
            }
        }
        
        public void AddSummonListener(ButtonWithCooldown button, YeeHawActionType yeeHawActionType)
        {
            uiManager.UpdateButtonPressMoneyPrice(button, dataProvider.GetSummonPrice(yeeHawActionType));
            TooltipUIManager tooltipUIManager = uiManager.CreateTooltipPanel(button, dataProvider.GetTooltipContent(yeeHawActionType));
            tooltipUIManager.OnWarningTextUpdate += tooltipUIManager.UpdateWarningTextForYeeHawPowerTooltip;
            
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(yeeHawActionType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(yeeHawActionType, tooltipUIManager);
            Action checkOutButtonState = () => CheckoutSummonButtonActivity(button, checkFunction);

            if (button != null)
            {
                button.onClick.AddListener(() => summonManager.PayForSummon(yeeHawActionType));
                button.onClick.AddListener(() => OnSummonButtonClick(yeeHawActionType, button, cooldownController));
                uiManager.OnButtonsActivityChange += checkOutButtonState;
            }
        }

        public void OnSummonButtonClick(UnitType unitType, ButtonWithCooldown button,
            SummonCooldownController cooldownController)
        {
            float cooldown = dataProvider.GetSummonCooldownTiming(unitType);

            cooldownController.OnCallOfActionWithCooldown(summonManager.SummonEntity(unitType, button, cooldown));
        }

        public void OnSummonButtonClick(BuildingType buildingType, ButtonWithCooldown button,
            SummonCooldownController cooldownController)
        {
            float cooldown = dataProvider.GetSummonCooldownTiming(buildingType);

            cooldownController.OnCallOfActionWithCooldown(summonManager.SummonEntity(buildingType, button, cooldown));
        }
        
        public void OnSummonButtonClick(YeeHawActionType yeeHawActionType, ButtonWithCooldown button,
            SummonCooldownController cooldownController)
        {
            float cooldown = dataProvider.GetSummonCooldownTiming(yeeHawActionType);
            
            cooldownController.OnCallOfActionWithCooldown(summonManager.SummonEntity(yeeHawActionType, button, cooldown));
        }

        private void CheckoutSummonButtonActivity(ButtonWithCooldown button, Func<bool> conditionCheckForActivity)
        {
            if (conditionCheckForActivity.Invoke())
            {
                uiManager.ActivateButton(button);
            }
            else
            {
                uiManager.DeactivateButton(button);
            }
        }

        public void OnSpeedButtonClick()
        {
            gameSpeedController.ChangeSpeed();
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