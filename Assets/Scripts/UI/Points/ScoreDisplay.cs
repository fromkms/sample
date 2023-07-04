using Gameplay.GameEvents;
using TMPro;
using UnityEngine;

namespace UI.Points
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI value;


        private void Awake()
        {
            EventBus.Hub.Subscribe<PlayerDataUpdateMessage>(PlayerDataUpdateEvent);
        }
        
        
        private void PlayerDataUpdateEvent(PlayerDataUpdateMessage message)
        {
            var currentPoints = message.Content.Score;
            value.text = currentPoints.ToString();
        }
    }
}
