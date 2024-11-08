using UnityEngine;

namespace Services.GamePauseManager
{
    public class GamePauseManager : MonoBehaviour
    {
        private bool isPaused = false;

        public bool IsPaused => isPaused;

        public void TogglePause()
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        public void ResumeGame()
        {
            isPaused = false;
            Time.timeScale = 1;
        }

        public void PauseGame()
        {
            isPaused = true;
            Time.timeScale = 0;
        }
    }
}