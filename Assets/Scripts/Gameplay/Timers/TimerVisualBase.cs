using System;
using UnityEngine;

namespace Gameplay.Timers
{
    public abstract class TimerVisualBase : MonoBehaviour
    {
        public event Action OnTimerEnd = delegate {  };
        
        
        protected float Duration { get; private set; }
        
        public void Initilize(float duration)
        {
            Duration = duration;
        }


        public abstract void Begin();
        public abstract void Stop();


        protected void TriggerEnd()
        {
            OnTimerEnd.Invoke();
        }
    }
}
