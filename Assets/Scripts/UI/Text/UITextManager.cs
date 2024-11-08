using UnityEngine;
using TMPro;

namespace UI
{
    public class UITextManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textObject;

        public void UpdateText(string text)
        {
            textObject.text = text;
        }
        
        public void UpdateText(float num)
        {
            textObject.text = num.ToString();
        }
    }
}