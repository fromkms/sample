using UnityEngine;
using UnityEngine.UI;

namespace UI.Health
{
    [RequireComponent(typeof(Image))]
    public class HealthPanelElement : MonoBehaviour
    {
        [SerializeField] private Image targetImage;


        private void OnValidate()
        {
            if (targetImage == null) targetImage = GetComponent<Image>();
        }


        public void MarkAsUsed()
        {
            SetTargetAlpha(0.4f);
        }


        public void ResetToDefault()
        {
            SetTargetAlpha(1f);
        }


        private void SetTargetAlpha(float value)
        {
            var color = targetImage.color;
            color.a = value;
            targetImage.color = color;
        }
    }
}
