using Gameplay.GameEvents;
using UnityEngine;

namespace Gameplay
{
    public class PlayerData
    {
        private int currentLives;
        private int currentScore;
        
        
        public PlayerData(int startLives)
        {
            Reset(startLives);
            
            EventBus.Hub.Subscribe<CardDistributedMessage>(CardDistributedEvent);
        }


        public void Reset(int lives)
        {
            ResetInternal(lives);
            SendUpdate();
        }


        private void ResetInternal(int lives)
        {
            currentLives = Mathf.Clamp(lives, 1, int.MaxValue);
            currentScore = 0;
        }
        
        
        private void CardDistributedEvent(CardDistributedMessage message)
        {
            var data = message.Content;
            var rightDistribution = data.CardData.Eatable && data.DistributedType == DistributedData.Type.Eatable || data.CardData.Eatable == false && data.DistributedType == DistributedData.Type.Uneatable;
            currentLives += rightDistribution == false ? -1 : 0;
            currentScore += rightDistribution ? 1 : 0;

            SendUpdate();
        }


        private void SendUpdate()
        {
            var t = new PlayerDataDelta()
            {
                Score = currentScore,
                Lives = currentLives 
            };
            var notification = new PlayerDataUpdateMessage(this, t);
            EventBus.Hub.Publish(notification);

            if (currentLives == 0)
            {
                var endGame = new EndGameMessage(this);
                EventBus.Hub.Publish(endGame);
            }
            else
            {
                var nextItem = new ProcessNextItem(this);
                EventBus.Hub.Publish(nextItem);
            }
        }
    }
}
