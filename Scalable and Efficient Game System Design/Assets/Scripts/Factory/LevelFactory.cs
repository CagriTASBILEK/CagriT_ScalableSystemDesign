using UnityEngine;
using System.Collections.Generic;
using Control;
using Factory;

public class LevelFactory
{
    private ObjectPool objectPool;
    private ObstacleFactory obstacleFactory;
    private List<string> levelTags;
    private List<GameObject> levelPrefabs;
    private int difficultyThresholdForRandom = 10;
    
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

    public void ReturnLevelToPool(ILevel level)
    {
        GameObject levelObject = (level as MonoBehaviour).gameObject;
        string levelTag = levelObject.tag;
        objectPool.ReturnToPool(levelTag, levelObject);
    }

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