using System;
using Gameplay.GameEvents;
using UnityEngine;

namespace UI
{
    public class DistributeGroups : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;


        private void Awake()
        {
            EventBus.Hub.Subscribe<CardHasSpawnedMessage>(CardHasReady);
            EventBus.Hub.Subscribe<ProcessNextItem>(ProcessNextItem);
        }


        private void ProcessNextItem(ProcessNextItem obj)
        {
            canvasGroup.alpha = 0f;
        }


        private void CardHasReady(CardHasSpawnedMessage message)
        {
            canvasGroup.alpha = 1f;
        }
    }
}
