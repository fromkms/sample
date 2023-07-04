using System.Collections.Generic;
using Gameplay.GameEvents;
using UnityEngine;

namespace Gameplay.Timers
{
    public class RoundTimer : MonoBehaviour
    {
        [SerializeField] private List<TimerVisualBase> timers;


        private void Awake()
        {
            EventBus.Hub.Subscribe<GameConfigLoaded>(ConfigLoaded);
            EventBus.Hub.Subscribe<CardHasSpawnedMessage>(CardHasReady);
            EventBus.Hub.Subscribe<ProcessNextItem>(ProcessNextItem);
            EventBus.Hub.Subscribe<EndGameMessage>(EndGameEvent);
            
            timers[0].OnTimerEnd += TimerEndEvent;
        }


        private void TimerEndEvent()
        {
            var message = new TimerEndMessage(this);
            EventBus.Hub.Publish(message);
        }


        private void EndGameEvent(EndGameMessage obj)
        {
            StopAll();
        }


        private void ProcessNextItem(ProcessNextItem obj)
        {
            StopAll();
        }


        private void CardHasReady(CardHasSpawnedMessage message)
        {
            StopAll();
            
            foreach (var timer in timers)
            {
                timer.Begin();
            }
        }


        private void ConfigLoaded(GameConfigLoaded message)
        {
            var duration = message.Content.SelectionLimit;
            
            foreach (var timer in timers)
            {
                timer.Initilize(duration);
            }
        }


        private void StopAll()
        {
            foreach (var timer in timers)
            {
                timer.Stop();
            }
        }
    }
}
