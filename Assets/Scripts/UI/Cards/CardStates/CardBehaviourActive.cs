using Gameplay.GameEvents;
using TinyMessenger;
using UnityEngine;

namespace UI.Cards.CardStates
{
    public class CardBehaviourActive : CardBehaviourBase
    {
        private static readonly int BlendFloatParamHash = Animator.StringToHash("SwipeBlend");
        private static readonly int OutroUneatableTriggerParamHash = Animator.StringToHash("OutroUneatable");
        private static readonly int OutroEatableTriggerParamHash = Animator.StringToHash("OutroEatable");
        private static readonly int OutroTimeoutTriggerParamHash = Animator.StringToHash("OutroTimeout");


        private float passedDistance = 0f;
        private TinyMessageSubscriptionToken subscriptionToken;
        
        
        protected override void OnStateEnterInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Item.EnableInteraction();
            Item.OnDragBegin += DragBeginEvent;
            Item.OnDragEvent += DragEvent;
            Item.OnDragEnd += DragEndEvent;
            
            subscriptionToken = EventBus.Hub.Subscribe<TimerEndMessage>(TimerEndMessage);
        }
        

        protected override void OnStateExitInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Item.OnDragBegin -= DragBeginEvent;
            Item.OnDragEvent -= DragEvent;
            Item.OnDragEnd -= DragEndEvent;
            
            EventBus.Hub.Unsubscribe(subscriptionToken);
        }
        

        private void DragBeginEvent(Vector2 startPosition)
        {
            passedDistance = 0f;
        }
        
        
        private void DragEvent(Vector2 delta)
        {
            passedDistance += delta.x;
            var t = passedDistance / Item.SwipeMaxDistance;
            SetBlendValue(t);

            if (CheckBounds(t, out var toEatable))
            {
                DistributeCard(toEatable? DistributedData.Type.Eatable : DistributedData.Type.Uneatable);
            }
        }
        
        
        private void TimerEndMessage(TimerEndMessage obj)
        {
            DistributeCard(DistributedData.Type.TimeIsOut);
        }


        private void DistributeCard(DistributedData.Type type)
        {
            if (type == DistributedData.Type.Eatable)
            {
                Animator.SetTrigger(OutroEatableTriggerParamHash);
            }
            else if (type == DistributedData.Type.Uneatable)
            {
                Animator.SetTrigger(OutroUneatableTriggerParamHash);
            }
            else if (type == DistributedData.Type.TimeIsOut)
            {
                Animator.SetTrigger(OutroTimeoutTriggerParamHash);
            }
            
            var data = new DistributedData()
            {
                DistributedType =  type,
                CardData = Item.CurrentData
            };
            var message = new CardDistributedMessage(this, data);
            EventBus.Hub.Publish(message);
        }
        
        
        private void DragEndEvent(Vector2 endPosition)
        {
            passedDistance = 0f;
            SetBlendValue(0f);
        }


        private void SetBlendValue(float newValue)
        {
            newValue = Mathf.Clamp(newValue, -1, 1);
            Animator.SetFloat(BlendFloatParamHash, newValue);
        }


        private bool CheckBounds(float value, out bool toEatable)
        {
            if (value <= -1)
            {
                toEatable = false;
                return true;
            }

            if (value >= 1)
            {
                toEatable = true;
                return true;
            }

            toEatable = false;
            return false;
        }
    }
}
