using System;
using UI;
using UnityEngine;
using UI.Buttons;

namespace Services
{
    public class GameSpeedController : MonoBehaviour
    {
        private float[] speedLevels = { 0.75f, 1f, 1.5f, 2f };
        public int regularSpeedLevel = 1;
        private int currentLevelIndex = 1;

        [SerializeField] private LevelUIManager levelUIManager;

        public void Start()
        {
            SetRegularSpeedLevel();
        }
        
        public void SetRegularSpeedLevel()
        {
            SetSpeedLevel(regularSpeedLevel);
        }
        
        public void SetNextSpeed()
        {
            SetSpeedLevel((currentLevelIndex + 1) % speedLevels.Length);
        }
        
        public void SetSpeedLevel(int level)
        {
            if(level >= 0 && level <= speedLevels.Length)
            {
                currentLevelIndex = level;
                Time.timeScale = speedLevels[level];
                levelUIManager.UpdateSpeedLevelIdentifiers(level);
            }
        }
        
        public void ResumeGame()
        {
            SetSpeedLevel(currentLevelIndex);
        }

        public void PauseGame()
        {
            Time.timeScale = 0;
        }

        public void TogglePause()
        {
            if (Time.timeScale == 0)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
}