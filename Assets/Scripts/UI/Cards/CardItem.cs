using System;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Cards
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CardItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<Vector2> OnDragBegin = delegate {  }; 
        public event Action<Vector2> OnDragEvent = delegate {  }; 
        public event Action<Vector2> OnDragEnd = delegate {  }; 
        

        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI itemName;
        [SerializeField] private Animator animator;
        [SerializeField] private float swipeMaxDistance = 300f;


        public float SwipeMaxDistance => swipeMaxDistance;
        public CardItemData CurrentData { get; private set; }


        private void OnValidate()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        }


        private void Awake()
        {
            animator.Update(0f);
        }


        public void EnableInteraction()
        {
            canvasGroup.interactable = true;
        }


        public void DisableInteraction()
        {
            canvasGroup.interactable = false;
        }
        

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragBegin.Invoke(eventData.position);
        }


        public void OnDrag(PointerEventData eventData)
        {
            OnDragEvent.Invoke(eventData.delta);
        }


        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEnd.Invoke(eventData.delta);
        }


        public void Initilize(CardItemData data)
        {
            CurrentData = data;
            itemImage.sprite = CurrentData.Sprite;
            itemName.text = CurrentData.Name;
        }
    }
}
