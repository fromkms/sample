using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "GameData/CardItems/New Card Item", fileName = "CardItem")]
    public class CardItemData : ScriptableObject
    {
        [SerializeField] private string shortName;
        [SerializeField] private bool eatable;
        [SerializeField] private Sprite sprite;


        public string Name => shortName;


        public bool Eatable => eatable;


        public Sprite Sprite => sprite;
    }
}
