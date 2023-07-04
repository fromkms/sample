using Gameplay.GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FinishPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI pointsValue;
        [SerializeField] private Button restartButton;


        private void Awake()
        {
            restartButton.onClick.AddListener(RestartButtonClickEvent);
            EventBus.Hub.Subscribe<PlayerDataUpdateMessage>(PlayerDataUpdateEvent);
            EventBus.Hub.Subscribe<EndGameMessage>(EndGameMessageEvent);
            
            Hide();
        }


        private void EndGameMessageEvent(EndGameMessage message)
        {
            Show();
            gameObject.SetActive(true);
        }


        private void PlayerDataUpdateEvent(PlayerDataUpdateMessage message)
        {
            var data = message.Content.Score;
            pointsValue.text = $"Счет: {data}";
        }


        private void RestartButtonClickEvent()
        {
            Hide();
            var message = new RestartGameMessage(this);
            EventBus.Hub.Publish(message);
        }


        private void Show()
        {
            canvasGroup.alpha = 1f;
            gameObject.SetActive(true);
        }


        private void Hide()
        {
            canvasGroup.alpha = 0f;
            gameObject.SetActive(false);
        }
    }
}
