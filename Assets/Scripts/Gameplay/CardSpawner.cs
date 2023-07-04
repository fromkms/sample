using System.Collections.Generic;
using Gameplay.Factory;
using Gameplay.GameEvents;
using UI.Cards;
using UnityEngine;

namespace Gameplay
{
    public class CardSpawner : MonoBehaviour
    {
        [SerializeField] private CardItem prefab;
        [SerializeField] private Transform cardsRoot;

        
        private ItemFactory cardsFactory;
        private List<CardItem> currentCards = new List<CardItem>(2);
        

        private void Awake()
        {
            cardsFactory = new CardsFactory(prefab, cardsRoot);
            EventBus.Hub.Subscribe<CardSpawnMessage>(CardSpawnEvent);
            EventBus.Hub.Subscribe<EndGameMessage>(EndGameEvent);
            EventBus.Hub.Subscribe<CardUnloadMessage>(CardUnloadMessage);
        }


        private void CardUnloadMessage(CardUnloadMessage message)
        {
            var data = message.Content;
            currentCards.Remove(data);
        }


        private void EndGameEvent(EndGameMessage message)
        {
            var messages = new List<CardUnloadMessage>(currentCards.Count);
            
            foreach (var cardItem in currentCards)
            {
                var t = new CardUnloadMessage(this, cardItem);
                messages.Add(t);
            }

            foreach (var unloadMessage in messages)
            {
                EventBus.Hub.Publish(unloadMessage);
            }
        }


        private void CardSpawnEvent(CardSpawnMessage message)
        {
            var card = cardsFactory.CreateItem(message.Content);
            currentCards.Add(card);
        }
    }
}
