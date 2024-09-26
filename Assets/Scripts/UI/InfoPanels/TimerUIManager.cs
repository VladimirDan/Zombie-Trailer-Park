namespace UI
{
    public class GameTimerUIManager : UITextManager
    {
        public void UpdateTimerUI(int minutes, int seconds)
        {
            UpdateText($"{minutes:D2}:{seconds:D2}");
        }
    }
}