using System;
using UnityEngine;

namespace Services.Timer
{
    public class Timer
    {
        private float elapsedTime = 0;
        public event Action OnTimerTick;
        
        public void Start()
        {
            elapsedTime = 0; 
        }
        
        public void Update()
        {
            elapsedTime += Time.deltaTime;
            OnTimerTick?.Invoke();
        }

        public int GetMinutes()
        {
            return (int)(elapsedTime / 60);
        }

        public int GetSeconds()
        {
            return (int)(elapsedTime % 60);
        }

        public float GetElapsedTime()
        {
            return elapsedTime;
        }
    }
}