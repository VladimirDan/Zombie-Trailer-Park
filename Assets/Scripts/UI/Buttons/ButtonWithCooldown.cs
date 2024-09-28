using Game.Code.Common.CoroutineRunner;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace UI.Buttons
{
    public class ButtonWithCooldown : Button, IPointerEnterHandler, IPointerExitHandler
    {
        private CoroutineRunner coroutineRunner;
        
        private Outline outline;
        [SerializeField] private Color hoverOutlineColor = Color.black;
        [SerializeField] private Color outlineColor = Color.yellow;
        [SerializeField] private Vector2 outlineDistanceHover = new Vector2(2, 2);
        [SerializeField] private Vector2 outlineDistance = new Vector2(1, 1);
        
        private Image mainImage;
        private Image overlayImage;
        public Sprite overlaySprite;
        
        [SerializeField] private Color activeButtonColor = Color.white;
        [SerializeField] private Color inactiveButtonColor = new Color(0.99f, 0.99f, 0.99f, 1f);
        
        [SerializeField] private Color cooldownProgressColor = new Color(1f, 1f, 1f, 0.3f);
        
        public event Action OnActivityChange;
        
        protected override void Start()
        {
            base.Start();

            coroutineRunner = FindObjectOfType<CoroutineRunner>();
            mainImage = GetComponent<Image>();
            overlayImage = FindOverlayImage() == null ? CreateOverlayImage() : FindOverlayImage();

            outline = GetComponent<Outline>();
            if(outline == null)
                outline = gameObject.AddComponent<Outline>();
            
            ChangeOutlineColor(hoverOutlineColor); 
            outline.effectDistance = outlineDistance; 
        }

        protected void Update()
        {
            OnActivityChange?.Invoke(); 
        }
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            ChangeOutlineColor(outlineColor);
            ChangeOutlineEffectDistance(outlineDistanceHover);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            ChangeOutlineColor(hoverOutlineColor);
            ChangeOutlineEffectDistance(outlineDistance);
        }

        private void ChangeOutlineColor(Color newColor)
        {
            outline.effectColor = newColor;
        }
        
        private void ChangeOutlineEffectDistance(Vector2 newDistance)
        {
            outline.effectDistance = newDistance;
        }
        
        private Image CreateOverlayImage()
        {
            GameObject overlayGameObject = new GameObject("OverlayImage");
            overlayImage = overlayGameObject.AddComponent<Image>();
            overlayGameObject.transform.SetParent(transform, false);

            RectTransform mainRect = mainImage.GetComponent<RectTransform>();
            RectTransform overlayRect = overlayImage.GetComponent<RectTransform>();
            overlayRect.sizeDelta = mainRect.sizeDelta;
            overlayRect.anchoredPosition = Vector2.zero;

            overlayImage.sprite = overlaySprite;
            overlayImage.type = Image.Type.Filled;
            overlayImage.fillMethod = Image.FillMethod.Radial360;
            HideOverlay();

            ChangeOverlayColor(cooldownProgressColor);
            overlayImage.raycastTarget = false;

            return overlayImage;
        }

        private Image FindOverlayImage()
        {
            Transform overlayTransform = transform.Find("OverlayImage");
            if (overlayTransform != null)
            {
                return overlayTransform.GetComponent<Image>();
            }

            return null;
        }

        public void FillOverlay(float seconds)
        {
            StartCoroutine(UpdateOverlayFill(seconds));
        }

        private IEnumerator UpdateOverlayFill(float seconds)
        {
            float elapsedTime = 0f;

            HideOverlay(); 

            overlayImage.color = cooldownProgressColor;
            
            while (elapsedTime < seconds)
            {
                elapsedTime += Time.deltaTime;
                overlayImage.fillAmount = elapsedTime / seconds; 
                yield return null; 
            }

            HideOverlay();
        }

        private void ChangeOverlayColor(Color newColor)
        {
            if (overlayImage != null)
            {
                overlayImage.color = newColor;
            }
        }

        private void ChangeButtonColor(Color newColor)
        {
            if (mainImage != null)
            {
                mainImage.color = newColor;
            }
        }
        
        public void SetInactiveState()
        {
            ChangeButtonColor(inactiveButtonColor);
            interactable = false;
        }
        
        public void SetActiveState()
        {
            ChangeButtonColor(activeButtonColor);
            interactable = true;
        }

        public void HideOverlay()
        {
            overlayImage.fillAmount = 0;
        }
    }
}