using Gameplay.GameEvents;
using UnityEngine;

namespace UI.Cards.CardStates
{
    public class CardBehaviourOutro : CardBehaviourBase
    {
        protected override void OnStateEnterInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Item.DisableInteraction();
        }


        protected override void OnStateExitInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var message = new CardUnloadMessage(this, Item);
            EventBus.Hub.Publish(message);
        }
    }
}
