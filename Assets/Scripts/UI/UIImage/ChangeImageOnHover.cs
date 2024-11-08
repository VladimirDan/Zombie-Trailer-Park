using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI
{
    public class ChangeImageOnHover: MonoBehaviour, IPointerEnterHandler
    {
        [SerializeField] private Image image; 
        [SerializeField] private Sprite hoverSprite; 

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeImageSprite(image, hoverSprite);
        }

        public void ChangeImageSprite(Image image, Sprite newSprite)
        {
            image.sprite = newSprite;
        }
    }
}