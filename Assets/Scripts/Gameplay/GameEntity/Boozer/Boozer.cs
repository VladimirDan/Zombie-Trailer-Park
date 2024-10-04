using UnityEngine;
using Services;

namespace Gameplay.GameEntity.Boozer
{
    public class Boozer : Unit
    {
        public GameObject bombPrefab;
        public float bombThrowHeight;
        
        public override void setUpEntity(DataProvider dataProvider, PlayerBankModel playerBankModel)
        {
            base.setUpEntity(dataProvider, playerBankModel);

            ((BoozerModel)unitModel).bombPrefab = bombPrefab;
            ((BoozerModel)unitModel).bombThrowHeight = bombThrowHeight;
        }
    }
}