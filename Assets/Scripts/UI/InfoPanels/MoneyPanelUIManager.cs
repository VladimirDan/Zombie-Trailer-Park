namespace UI
{
    public class MoneyPanelUIManager : UITextManager
    {
        public void UpdateMoneyUIInfo(float amount)
        {
            UpdateText($"$ {amount}");
        }
    }
}