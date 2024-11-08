using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class GameSpeedControllerUIManager : MonoBehaviour
    {
        [SerializeField] private Image[] speedLevelIdentifiers;

        public void UpdateSpeedLevelIdentifiers(int speedLevel)
        {
            for(int i = 0; i < speedLevelIdentifiers.Length; i++)
            {
                if (speedLevel < i)
                {
                    speedLevelIdentifiers[i].enabled = false;
                }
                else
                {
                    speedLevelIdentifiers[i].enabled = true;
                }
            }
        }
    }
}