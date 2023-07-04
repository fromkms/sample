using System.Collections.Generic;
using Gameplay.GameEvents;
using UnityEngine;

namespace UI.Health
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField] private HealthPanelElement elementPrefab;
        [SerializeField] private Transform elementsRoot;


        private List<HealthPanelElement> instances;
        

        private void Awake()
        {
            EventBus.Hub.Subscribe<GameConfigLoaded>(ConfigLoadedEvent);
            EventBus.Hub.Subscribe<PlayerDataUpdateMessage>(PlayerDataUpdateEvent);
        }


        private void PlayerDataUpdateEvent(PlayerDataUpdateMessage message)
        {
            var currentLives = message.Content.Lives;
            for (int i = 0; i < instances.Count; i++)
            {
                if (i < currentLives)
                {
                    instances[i].ResetToDefault();
                }
                else
                {
                    instances[i].MarkAsUsed();
                }
            }
        }


        private void ConfigLoadedEvent(GameConfigLoaded message)
        {
            var livesCount = message.Content.LivesCount;
            instances = new List<HealthPanelElement>(livesCount);
            
            for (int i = 0; i < livesCount; i++)
            {
                var t = Instantiate(elementPrefab, elementsRoot);
                instances.Add(t);
            }
        }
    }
}
