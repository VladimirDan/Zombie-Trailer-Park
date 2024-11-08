namespace UI
{
    public class TimerUIManager : UITextManager
    {
        public void UpdateTimerUI(int minutes, int seconds)
        {
            UpdateText($"{minutes:D2}:{seconds:D2}");
        }
        
        public void UpdateTimerUI(float timeInSeconds)
        {
            int minutes = (int)(timeInSeconds / 60);
            int seconds = (int)(timeInSeconds % 60);
            float centiseconds = timeInSeconds % 1 * 100;
            
            UpdateText($"{minutes:D2}:{seconds:D2}.{centiseconds:F0}");
        }
        
        public void UpdateTimerUI(int minutes, float seconds)
        {
            UpdateText($"{minutes:D2}:{seconds:D2}");
        }
    }
}