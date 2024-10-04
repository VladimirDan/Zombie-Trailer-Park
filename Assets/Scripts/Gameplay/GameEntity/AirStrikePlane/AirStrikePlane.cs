using Game.Code.Common.CoroutineRunner;
using System.Collections;
using UnityEngine;
using CreaturesData;

namespace Gameplay.GameEntity.AirStrikePlane
{
    public class AirStrikePlane : MonoBehaviour
    {
        [SerializeField] private AirStrikePlaneParameters airStrikePlaneParameters;
        public CoroutineRunner coroutineRunner;

        public void Initialize()
        {
            coroutineRunner.RunCoroutine(MoveCycle());
            coroutineRunner.RunCoroutine(BombingDropCycle());
        }
        
        public IEnumerator MoveCycle()
        {
            while (true)
            {
                Move();

                yield return null;
            }
        }
        
        public void Move()
        {
            Vector3 movement = new Vector3(airStrikePlaneParameters.moveSpeed * Time.deltaTime, 0, 0);
            transform.position += movement;
        }

        public IEnumerator BombingDropCycle()
        {
            while(true)
            {
                DropBomb();
                
                yield return new WaitForSeconds(airStrikePlaneParameters.bombDropCooldown);
            }
        }
        
        public void DropBomb()
        {
            Vector3 planePosition = transform.position;
            Vector3 planeSize = transform.localScale;
            Vector3 spawnPosition = new Vector3(
                planePosition.x,
                planePosition.y - planeSize.y / 2,
                planePosition.z
            );
            
            GameObject bomb = Instantiate(airStrikePlaneParameters.bombPrefab, spawnPosition, Quaternion.identity);
        }
    }
}