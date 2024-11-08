using UI;
using UI.Buttons;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Game.Code.Common.CoroutineRunner;
using System;
using Enums;

namespace Services
{
    public class GameController : MonoBehaviour
    {
        private CoroutineRunner coroutineRunner;
        private DataProvider dataProvider;
        private LevelUIManager levelUIManager;
        
        [SerializeField] private GameSpeedController gameSpeedController;
        private AudioManager audioManager;
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
            LevelUIManager levelUIManager, AudioManager audioManager)
        {
            this.coroutineRunner = coroutineRunner;
            this.dataProvider = dataProvider;
            this.summonManager = summonManager;
            this.levelUIManager = levelUIManager;
            this.summonManager = summonManager;
            this.audioManager = audioManager;

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
            AddSummonListener(bombardmentSummonButton, YeeHawActionType.Bombardment);
            AddSummonListener(crowdSummonSummonButton, YeeHawActionType.CrowdSummon);
        }

        public void AddSummonListener(ButtonWithCooldown button, UnitType unitType)
        {
            levelUIManager.UpdateButtonPressMoneyPrice(button, dataProvider.GetSummonPrice(unitType));
            UnitTooltipUIManager tooltipUIManager = levelUIManager.CreateTooltipPanel(button, dataProvider.GetTooltipContent(unitType), unitType);
            tooltipUIManager.OnWarningTextUpdate += tooltipUIManager.UpdateWarningTextForUnitTooltip;
            
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(unitType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(unitType, tooltipUIManager);
            Action checkOutButtonState = () => CheckoutSummonButtonActivity(button, checkFunction);

            if (button != null)
            {
                button.onClick.AddListener(() => summonManager.PayForSummon(unitType));
                button.onClick.AddListener(() => OnSummonButtonClick(unitType, button, cooldownController));
                levelUIManager.OnButtonsActivityChange += checkOutButtonState;
            }
        }

        public void AddSummonListener(ButtonWithCooldown button, BuildingType buildingType)
        {
            levelUIManager.UpdateButtonPressMoneyPrice(button, dataProvider.GetSummonPrice(buildingType));
            TooltipUIManager tooltipUIManager = levelUIManager.CreateTooltipPanel(button, dataProvider.GetTooltipContent(buildingType));
            tooltipUIManager.OnWarningTextUpdate += tooltipUIManager.UpdateWarningTextForBuildingTooltip;
            
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(buildingType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(buildingType, tooltipUIManager);
            Action checkOutButtonState = () => CheckoutSummonButtonActivity(button, checkFunction);

            if (button != null)
            {
                button.onClick.AddListener(() => summonManager.PayForSummon(buildingType));
                button.onClick.AddListener(() => OnSummonButtonClick(buildingType, button, cooldownController));
                levelUIManager.OnButtonsActivityChange += checkOutButtonState;
            }
        }
        
        public void AddSummonListener(ButtonWithCooldown button, YeeHawActionType yeeHawActionType)
        {
            levelUIManager.UpdateButtonPressYeeHawPointsPrice(button, dataProvider.GetSummonPrice(yeeHawActionType));
            TooltipUIManager tooltipUIManager = levelUIManager.CreateTooltipPanel(button, dataProvider.GetTooltipContent(yeeHawActionType));
            tooltipUIManager.OnWarningTextUpdate += tooltipUIManager.UpdateWarningTextForYeeHawPowerTooltip;
            
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(yeeHawActionType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(yeeHawActionType, tooltipUIManager);
            Action checkOutButtonState = () => CheckoutSummonButtonActivity(button, checkFunction);

            if (button != null)
            {
                button.onClick.AddListener(() => summonManager.PayForSummon(yeeHawActionType));
                button.onClick.AddListener(() => OnSummonButtonClick(yeeHawActionType, button, cooldownController));
                button.onClick.AddListener(() => audioManager.PlayYeeHawSound());
                levelUIManager.OnButtonsActivityChange += checkOutButtonState;
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
                levelUIManager.ActivateButton(button);
            }
            else
            {
                levelUIManager.DeactivateButton(button);
            }
        }

        public void OnSpeedButtonClick()
        {
            gameSpeedController.SetNextSpeed();
        }
        
        private void OnDestroy()
        {
            if (speedControllerButton != null)
            {
                speedControllerButton.onClick.RemoveListener(gameSpeedController.SetNextSpeed);
            }
        }
    }
}