using System.Collections;
using UnityEngine;

namespace Gameplay.Timers
{
    public class FillTimerVisual : TimerVisualBase
    {
        [SerializeField] private SlicedFilledImage targetImage;


        private Coroutine coroutine;
        

        public override void Begin()
        {
            coroutine = StartCoroutine(TimerCoroutine());
        }


        public override void Stop()
        {
            if(coroutine!=null) StopCoroutine(coroutine);
            targetImage.fillAmount = 1f;
        }


        IEnumerator TimerCoroutine()
        {
            var currentValue = Duration;
            while (true)
            {
                currentValue = Mathf.Clamp(currentValue - Time.deltaTime, -Mathf.Epsilon, Duration);
                var t = currentValue / Duration;
                if(t <= 0) break;
                targetImage.fillAmount = t;

                yield return null;
            }
            
            TriggerEnd();
        }
    }
}
