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
        }

        public void AddSummonListener(ButtonWithCooldown button, UnitType unitType)
        {
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(unitType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(unitType);
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
            SummonCooldownController cooldownController =
                new SummonCooldownController(coroutineRunner, dataProvider.GetSummonCooldownTiming(buildingType));

            Func<bool> checkFunction = () => summonManager.AreResourcesEnoughForSummoning(buildingType);
            Action checkOutButtonState = () => CheckoutSummonButtonActivity(button, checkFunction);

            if (button != null)
            {
                button.onClick.AddListener(() => summonManager.PayForSummon(buildingType));
                button.onClick.AddListener(() => OnSummonButtonClick(buildingType, button, cooldownController));
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