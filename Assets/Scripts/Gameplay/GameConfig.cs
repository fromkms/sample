using System;
using System.IO;
using UnityEngine;

namespace Gameplay
{
    public class GameConfig
    {
        private static readonly string DefaultConfigPath = Path.Combine(Application.streamingAssetsPath, "config.json");
        private static GameConfig DefaultConfig => new GameConfig() {LivesCount = 3, SelectionLimit = 5};
        
        public int LivesCount;
        public int SelectionLimit;


        public static GameConfig LoadConfig()
        {
            try
            {
                var data = File.ReadAllText(DefaultConfigPath);
                var gameConfig = JsonUtility.FromJson<GameConfig>(data);
                return gameConfig;
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError(e.StackTrace);
                
                return DefaultConfig;
            }
        }
    }
}
