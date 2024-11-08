using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.Buttons
{
    public class SoundButtonUIManager : MonoBehaviour
    {
        [SerializeField] private Image soundImage;
        [SerializeField] private Sprite soundOffSprite;
        [SerializeField] private Sprite soundOnSprite;

        public void SetSoundOnSprite()
        {
            soundImage.sprite = soundOnSprite;
        }
        
        public void SetSoundOffSprite()
        {
            soundImage.sprite = soundOffSprite;
        }
    }
}