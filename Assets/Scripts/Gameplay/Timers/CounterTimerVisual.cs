using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay.Timers
{
    public class CounterTimerVisual : TimerVisualBase
    {
        private static readonly int CountHash = Animator.StringToHash("Tick");
        private static readonly int ResetHash = Animator.StringToHash("Reset");
        
        
        [SerializeField] private TextMeshProUGUI timerValue;
        [SerializeField] private Animator animator;


        private Coroutine coroutine;
        
        
        public override void Begin()
        {
            coroutine = StartCoroutine(TimerCoroutine());
        }


        public override void Stop()
        {
            if(coroutine != null) StopCoroutine(coroutine);
            animator.ResetTrigger(CountHash);
            animator.SetTrigger(ResetHash);
        }


        private IEnumerator TimerCoroutine()
        {
            var maxValue = (int)Duration;
            var currentValue = (int)Duration;
            animator.ResetTrigger(ResetHash);

            while (true)
            {
                timerValue.text = currentValue.ToString();
                animator.SetTrigger(CountHash);
                
                yield return new WaitForSeconds(1f);
                
                currentValue = Mathf.Clamp(currentValue - 1, 0, maxValue);
                animator.SetTrigger(ResetHash);
                
                if(currentValue == 0) break;
            }
            
            animator.SetTrigger(ResetHash);
            TriggerEnd();
        }
    }
}
