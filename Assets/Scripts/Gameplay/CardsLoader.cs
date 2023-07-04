using System.Collections.Generic;
using System.Threading.Tasks;
using Data;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Gameplay
{
    public class CardsLoader
    {
        private readonly string eatablePath;
        private readonly string uneatablePath;
        private List<IResourceLocation> items;
        private AsyncOperationHandle<IList<IResourceLocation>> eatableLocations;
        private AsyncOperationHandle<IList<IResourceLocation>> uneatableLocations;
        private List<AsyncOperationHandle<CardItemData>> loadedCards = new List<AsyncOperationHandle<CardItemData>>();


        public CardsLoader(string eatablePath, string uneatablePath)
        {
            this.eatablePath = eatablePath;
            this.uneatablePath = uneatablePath;
        }


        public async Task Initilize()
        {
            eatableLocations = Addressables.LoadResourceLocationsAsync(eatablePath);
            uneatableLocations = Addressables.LoadResourceLocationsAsync(uneatablePath);
            
            await Task.WhenAll(eatableLocations.Task, uneatableLocations.Task);

            var totalCount = eatableLocations.Result.Count + uneatableLocations.Result.Count;
            items = new List<IResourceLocation>(totalCount);
            
            items.AddRange(eatableLocations.Result);
            items.AddRange(uneatableLocations.Result);
        }


        public async Task<CardItemData> GetRandomCard()
        {
            while (true)
            {
                var randomIndex = Random.Range(0, items.Count);
                var adress = items[randomIndex];
                var asset = Addressables.LoadAssetAsync<CardItemData>(adress);
                await asset.Task;

                var result = asset.Result;
                loadedCards.Add(asset);
            
                return result;
            }
        }


        public void UnloadCard(CardItemData data)
        {
            foreach (var handle in loadedCards)
            {
                if(handle.Result != data) continue;
                Addressables.Release(handle);
                loadedCards.Remove(handle);
                return;
            }
            
            Debug.LogError($"Can't release item {data.Name} cause it's not loaded");
        }
    }
}
