using UnityEngine;
using System.Collections;
using Game.Code.Common.CoroutineRunner;

namespace Services
{
    public class SummonCooldownController
    {
        private CoroutineRunner coroutineRunner;
        
        public float cooldownTime;
        private float lastClickTime = -100;
        private float remainingCooldown = 0;
        
        public void OnCallOfActionWithCooldown(IEnumerator action)
        {
            coroutineRunner.RunCoroutine(HandleCallOfActionWithCooldown(action));
        }

        private IEnumerator HandleCallOfActionWithCooldown(IEnumerator action)
        {
            remainingCooldown += cooldownTime - (Time.time - lastClickTime);
            remainingCooldown = Mathf.Clamp(remainingCooldown, 0, 1000);
            
            lastClickTime = Time.time;
            
            yield return new WaitForSeconds(remainingCooldown);

            coroutineRunner.RunCoroutine(action);
        }

        public SummonCooldownController(CoroutineRunner coroutineRunner, float cooldownTime)
        {
            this.coroutineRunner = coroutineRunner;
            this.cooldownTime = cooldownTime;
        }
    }
}