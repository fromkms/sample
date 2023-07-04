using Gameplay.GameEvents;
using UnityEngine;

namespace UI.Cards.CardStates
{
    public class CardBehaviourIntro : CardBehaviourBase
    {
        protected override void OnStateEnterInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Item.DisableInteraction();
        }


        protected override void OnStateExitInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            NotifyAboutReady();
        }
        
        
        private void NotifyAboutReady()
        {
            var message = new CardHasSpawnedMessage(this);
            EventBus.Hub.Publish(message);
        }
    }
}
