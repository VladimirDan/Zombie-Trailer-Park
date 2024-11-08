using Enums;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Services.SpawnManager
{
    public class BuildingImageManager : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] salvageYards;
        [SerializeField] private SpriteRenderer[] trailers;
        [SerializeField] private SpriteRenderer[] farmHouses;
        [SerializeField] private SpriteRenderer[] garages;
        [SerializeField] private SpriteRenderer[] stills;
        [SerializeField] private SpriteRenderer[] chapels;

        public void ActivateNextBuildingImage(BuildingType buildingType)
        {
            SpriteRenderer[] spriteRenderers = buildingType switch
            {
                BuildingType.SalvageYard => salvageYards,
                BuildingType.Trailer => trailers,
                BuildingType.FarmHouse => farmHouses,
                BuildingType.Still => stills,
                BuildingType.Garage => garages,
                BuildingType.Chapel => chapels,
                _ => Array.Empty<SpriteRenderer>(),
            };

            SpriteRenderer image = FindNextUnEnabledBuildingImage(spriteRenderers);

            if (image != null)
            {
                image.enabled = true;
            }
        }

        public SpriteRenderer FindNextUnEnabledBuildingImage(SpriteRenderer[] spriteRenderers)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                if (spriteRenderers[i].enabled == false)
                {
                    return spriteRenderers[i];
                }
            }

            return null;
        }
    }
}