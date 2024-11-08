using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Buttons
{
    public class MenuLevelButton : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Sprite onButtonSelectedBgSprite;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image onSelectedLeftImage;

        public void OnPointerEnter(PointerEventData eventData)
        {
            backgroundImage.sprite = onButtonSelectedBgSprite;
            onSelectedLeftImage.gameObject.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            onSelectedLeftImage.gameObject.SetActive(false);
        }
    }
}
