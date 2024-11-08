using UI;
using UnityEngine;

namespace Services.Timer
{
    public class TimerManager : MonoBehaviour
    {
        [SerializeField] private TimerUIManager timerUITextManager;
        private Timer timer;

        public void Initialize()
        {
            timer = new Timer();
            timer.Start();
        }

        public void Update()
        {
            timer.Update();

            int minutes = timer.GetMinutes();
            int seconds = timer.GetSeconds();
            
            timerUITextManager.UpdateTimerUI(minutes, seconds);
        }

        public Timer GetTimer()
        {
            return timer;
        }
    }
}