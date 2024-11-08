using Services;
using UI.Buttons;
using UnityEngine;
using System;
using Gameplay.GameParameters;
using TMPro;
using GameParameters;
using UnityEngine.UI;

namespace UI
{
    public class MainMenuUIManager : MonoBehaviour
    {
        [SerializeField] private TimerUIManager[] levelCompleteTimerUIManagers;
        [SerializeField] private Image[] levelsLocks;
        
        [SerializeField] private LevelsParameters levelsParameters;

        public void Initialize()
        {
            for (int i = 0; i < levelCompleteTimerUIManagers.Length; i++)
            {
                float fastestLevelCompleteTime = levelsParameters.levelsParameters[i].fastestLevelCompleteTime;
                if (fastestLevelCompleteTime < float.MaxValue)
                {
                    TimerUIManager timerUIManager = levelCompleteTimerUIManagers[i];
                    timerUIManager.UpdateTimerUI(fastestLevelCompleteTime);
                }
            }
            
            for (int i = 0; i < levelCompleteTimerUIManagers.Length; i++)
            {
                if (levelsParameters.levelsParameters[i].isLevelAvailable != true)
                {
                    levelsLocks[i].enabled = true;
                }
                else
                {
                    levelsLocks[i].enabled = false;
                }
            }
        }
    }
}