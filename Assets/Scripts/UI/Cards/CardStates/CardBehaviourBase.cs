using UnityEngine;

namespace UI.Cards.CardStates
{
    public abstract class CardBehaviourBase : StateMachineBehaviour
    {
        protected CardItem Item { get; private set; }
        protected Animator Animator { get; private set; }


        public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Animator = animator;
            
            var go = animator.gameObject;
            if (go.TryGetComponent<CardItem>(out var result))
            {
                Item = result;
            }
            else
            {
                Debug.LogError($"Component {nameof(CardItem)} not found on {go.name}", go);
            }
            
            OnStateEnterInternal(animator, stateInfo, layerIndex);
        }
        
        
        public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            OnStateExitInternal(animator, stateInfo, layerIndex);
        }


        protected virtual void OnStateEnterInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}
        protected virtual void OnStateExitInternal(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){}
    }
}
