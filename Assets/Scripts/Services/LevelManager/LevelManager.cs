using Game.Code.Common.CoroutineRunner;
using UnityEngine;
using GameParameters;
using UnityEngine.SceneManagement;

namespace Services.LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelsParameters levelsParameters;
        [SerializeField] private LevelParametersContainer currentLevelParameters;
        private CoroutineRunner coroutineRunner;

        public void Initialize(CoroutineRunner coroutineRunner)
        {
            UpdateLevelsAvailability();
            this.coroutineRunner = coroutineRunner;
        }
        
        public void LoadLevel(int levelIndex)
        {
            if (levelsParameters.levelsParameters[levelIndex].isLevelAvailable == false)
            {
                return;
            }
            
            if (levelIndex < 0 || levelIndex >= levelsParameters.levelsParameters.Length)
            {
                Debug.LogError("Invalid level index!");
                return;
            }

            currentLevelParameters.levelParameters = levelsParameters.levelsParameters[levelIndex];
            coroutineRunner.Dispose();
            SceneManager.LoadScene(1);
        }
        
        public void LoadLevel(LevelParametersContainer level)
        {
            int levelIndex = level.levelParameters.levelId;
            
            if (levelsParameters.levelsParameters[levelIndex].isLevelAvailable == false)
            {
                return;
            }
            
            if (levelIndex < 0 || levelIndex >= levelsParameters.levelsParameters.Length)
            {
                Debug.LogError("Invalid level index!");
                return;
            }

            currentLevelParameters.levelParameters = levelsParameters.levelsParameters[levelIndex];

            SceneManager.LoadScene(0);
        }

        public void UpdateLevelsAvailability()
        {
            for (int i = 1; i < levelsParameters.levelsParameters.Length; i++)
            {
                if (levelsParameters.levelsParameters[i - 1].fastestLevelCompleteTime == float.MaxValue)
                {
                    levelsParameters.levelsParameters[i].UpdateLevelAvailability(false);
                }
                else
                {
                    levelsParameters.levelsParameters[i].UpdateLevelAvailability(true);
                }
            }
        }
    }
}