namespace UI
{
    public class ArmyCapacityPanelUIManager : UITextManager
    {
        public void UpdateArmyCapacityUIInfo(float amount, float capacity)
        {
            UpdateText($"{amount}/{capacity}");
        }
    }
}