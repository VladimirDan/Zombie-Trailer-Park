using Game.Code.Common.CoroutineRunner;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace UI.Buttons
{
    public class ButtonWithCooldown : Button
    {
        private CoroutineRunner coroutineRunner;

        private Image mainImage;
        private Image overlayImage;
        public Sprite overlaySprite;
        
        private Color activeButtonColor = Color.white;
        private Color inactiveButtonColor = new Color(0.99f, 0.99f, 0.99f, 1f);
        
        private Color cooldownProgressColor = new Color(1f, 1f, 1f, 0.3f);
        
        public event Action OnActivityChange;
        
        protected override void Start()
        {
            base.Start();

            coroutineRunner = FindObjectOfType<CoroutineRunner>();
            mainImage = GetComponent<Image>();


            overlayImage = FindOverlayImage() == null ? CreateOverlayImage() : FindOverlayImage();
        }

        protected void Update()
        {
            OnActivityChange?.Invoke(); 
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

        public void FillOverlay()
        {
            overlayImage.fillAmount = 1;
        }

        private IEnumerator UpdateOverlayFill(float seconds)
        {
            float elapsedTime = 0f;

            HideOverlay(); 

            overlayImage.color = cooldownProgressColor;
            
            while (elapsedTime < seconds)
            {
                elapsedTime += Time.deltaTime;
                overlayImage.fillAmount = elapsedTime / seconds; // Заполняем пропорционально времени
                yield return null; // Ждем следующего кадра
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