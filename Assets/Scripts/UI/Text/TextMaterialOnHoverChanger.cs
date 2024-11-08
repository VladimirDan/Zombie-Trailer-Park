using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class TextMaterialOnHoverChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text[] textMeshPro; 
        [SerializeField] private Material normalMaterial; 
        [SerializeField] private Material hoverMaterial; 

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeTMPMaterial(textMeshPro, hoverMaterial);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeTMPMaterial(textMeshPro, normalMaterial);
        }

        public void ChangeTMPMaterial(TMP_Text[] textMeshProElements, Material newMaterial)
        {
            foreach (TMP_Text tmp in textMeshProElements)
            {
                tmp.fontSharedMaterial = newMaterial;
            }
        }
    }
}