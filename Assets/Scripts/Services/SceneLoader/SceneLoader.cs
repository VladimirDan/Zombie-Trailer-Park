using Game.Code.Common.CoroutineRunner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.SceneLoader
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(0);
            FindObjectOfType<CoroutineRunner>().Dispose();
        }
    }
}