using System;
using UnityEngine;
using UI.Buttons;

namespace Services.SoundManager
{
    public class SoundSwitcher : MonoBehaviour
    {
        [SerializeField] private SoundButtonUIManager soundButtonUIManager;
        private AudioSource audioSource;

        public void Start()
        {
            audioSource = GameObject.Find("Music").GetComponent<AudioSource>();
            if(audioSource != null && !audioSource.isPlaying)
            {
                StopAllSounds();
            }
            
        }

        public void SwitchSound()
        {
            if (audioSource.isPlaying)
            {
                StopAllSounds();
            }
            else
            {
                PlayAllSounds();
            }
        }
            
        public void StopAllSounds()
        {
            audioSource.Stop();
            AudioListener.volume = 0f;
            soundButtonUIManager.SetSoundOffSprite();
        }

        public void PlayAllSounds()
        {
            audioSource.Play();
            AudioListener.volume = 1f;
            soundButtonUIManager.SetSoundOnSprite();
        }
    }
}