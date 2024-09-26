using UnityEngine;

namespace Services
{
    public class GameSpeedController
    {
        private float[] speedLevels = { 0.75f, 1f, 1.5f, 2f };
        private int currentLevelIndex = 1;

        public void ChangeSpeed()
        {
            currentLevelIndex = (currentLevelIndex + 1) % speedLevels.Length;
            Time.timeScale = speedLevels[currentLevelIndex];
        }
    }
}