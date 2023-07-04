using Data;
using UI.Cards;
using UnityEngine;

namespace Gameplay.Factory
{
    public abstract class ItemFactory
    {
        public abstract CardItem CreateItem(CardItemData data);
    }


    public class CardsFactory : ItemFactory
    {
        private readonly CardItem prefab;
        private readonly Transform root;
        
        
        public CardsFactory(CardItem prefab, Transform root)
        {
            this.prefab = prefab;
            this.root = root;
        }
        
        
        public override CardItem CreateItem(CardItemData data)
        {
            var instance = Object.Instantiate(prefab, root);
            instance.Initilize(data);
            return instance;
        }
    }
}
