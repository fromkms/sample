using Gameplay.GameEvents;
using UnityEngine;

namespace Gameplay
{
    public class Core : MonoBehaviour
    {
        [SerializeField] private string eatablePath = "Eatable";
        [SerializeField] private string uneatablePath = "Uneatable";
        
        private CardsLoader cardsLoader;
        private PlayerData playerData;
        private GameConfig config;


        private void Awake()
        {
            EventBus.Hub.Subscribe<ProcessNextItem>(ProcessNextItem);
            EventBus.Hub.Subscribe<CardUnloadMessage>(CardUnloadMessageEvent);
            EventBus.Hub.Subscribe<RestartGameMessage>(RestartGameMessage);
        }

        
        private async void Start()
        {
            cardsLoader = new CardsLoader(eatablePath, uneatablePath);
            await cardsLoader.Initilize();

            LoadConfig();
        }


        private void LoadConfig()
        {
            config = GameConfig.LoadConfig();
            playerData = new PlayerData(config.LivesCount);
            var message = new GameConfigLoaded(this, config);
            EventBus.Hub.Publish(message);
        }


        private async void SpawnRandomCard()
        {
            var card = cardsLoader.GetRandomCard();
            await card;
            var message = new CardSpawnMessage(this, card.Result);
            EventBus.Hub.Publish(message);
        }
        
        
        private void ProcessNextItem(ProcessNextItem message)
        {
            SpawnRandomCard();
        }
        
        
        private void CardUnloadMessageEvent(CardUnloadMessage message)
        {
            cardsLoader.UnloadCard(message.Content.CurrentData);
            Destroy(message.Content.gameObject);
        }
        
        
        private void RestartGameMessage(RestartGameMessage message)
        {
            playerData.Reset(config.LivesCount);
        }
    }
}
