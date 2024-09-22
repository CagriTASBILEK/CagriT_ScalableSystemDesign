using System.Collections.Generic;
using UnityEngine;

namespace Factory
{
    /// <summary>
    /// Factory class used to create and manage levels.
    /// </summary>
    public class LevelFactory
    {
        private ObjectPool objectPool;
        private ObstacleFactory obstacleFactory;
        private List<string> levelTags;
        private List<GameObject> levelPrefabs;
        private int difficultyThresholdForRandom = 10;
    
        /// <summary>
        /// Constructor for the LevelFactory class.
        /// </summary>
        public LevelFactory(ObjectPool objectPool, ObstacleFactory obstacleFactory)
        {
            this.objectPool = objectPool;
            this.obstacleFactory = obstacleFactory;
            this.levelTags = new List<string>();
            this.levelPrefabs = new List<GameObject>();
        }
        
        public void AddLevelPrefab(string type, GameObject prefab)
        {
            if (!levelTags.Contains(type))
            {
                levelTags.Add(type);
                levelPrefabs.Add(prefab);
                objectPool.AddPrefab(type, prefab);
            }
        }

        /// <summary>
        /// Creates a new level based on the specified difficulty.
        /// </summary>
        public ILevel CreateLevel(int difficulty)
        {
            string levelTag = GetLevelTagForDifficulty(difficulty);
        
            int index = levelTags.IndexOf(levelTag);
            if (index == -1)
            {
                return null;
            }

            GameObject levelObject = objectPool.SpawnFromPool(levelTag, Vector3.zero, Quaternion.identity);
        
            if (levelObject == null)
            {
                return null;
            }

            ILevel level = levelObject.GetComponent<ILevel>();
            if (level == null)
            {
                return null;
            }

            level.Initialize(difficulty, obstacleFactory);
            return level;
        }
        /// <summary>
        /// Returns the level to the object pool.
        /// </summary>
        public void ReturnLevelToPool(ILevel level)
        {
            GameObject levelObject = (level as MonoBehaviour).gameObject;
            string levelTag = levelObject.tag;
            objectPool.ReturnToPool(levelTag, levelObject);
        }

        /// <summary>
        /// Returns the appropriate level tag for the specified difficulty.
        /// </summary>
        private string GetLevelTagForDifficulty(int difficulty)
        {
            if (difficulty < difficultyThresholdForRandom)
            {
                if (difficulty <= 3) return "EasyLevel";
                if (difficulty <= 7) return "MediumLevel";
                return "HardLevel";
            }
        
            return levelTags[Random.Range(0, levelTags.Count)];
        }
    }
}