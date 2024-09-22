using UnityEngine;
using System.Collections.Generic;
using Control;
using Factory;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private int initialDifficulty = 1;
    [SerializeField] private float levelSpeed = 5f;
    [SerializeField] private int maxActiveLevels = 3;
    [SerializeField] private float distanceBetweenLevels = 2f; 
    [SerializeField] private int maxDifficulty = 20;
    
    private LevelFactory levelFactory;
    private ObstacleFactory obstacleFactory;
    private List<LevelControl> activeLevels = new List<LevelControl>();
    private int currentDifficulty;
    private float totalDistance = 0f;

    private void Awake()
    {
        obstacleFactory = new ObstacleFactory(objectPool);
        levelFactory = new LevelFactory(objectPool, obstacleFactory);
        LoadLevelPrefabsFromResources();
    }

    private void LoadLevelPrefabsFromResources()
    {
        GameObject[] levelPrefabs = Resources.LoadAll<GameObject>("LevelPrefabs");
        foreach (GameObject prefab in levelPrefabs)
        {
            levelFactory.AddLevelPrefab(prefab.name, prefab);
        }
    }

    private void Start()
    {
        currentDifficulty = initialDifficulty;
        LoadInitialLevels();
    }

    private void LoadInitialLevels()
    {
        for (int i = 0; i < maxActiveLevels; i++)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        LevelControl newLevel = levelFactory.CreateLevel(currentDifficulty) as LevelControl;
        if (newLevel != null)
        {
            float zPosition;
            if (activeLevels.Count > 0)
            {
                LevelControl lastLevel = activeLevels[activeLevels.Count - 1];
                float newLevelLength = (newLevel.transform.localScale.z*5) + activeLevels[activeLevels.Count - 1].transform.localScale.z*5;
                zPosition = lastLevel.transform.position.z + newLevelLength;
            }
            else
            {
                zPosition = 0f;
            }

            newLevel.transform.position = new Vector3(0, 0, zPosition);
            activeLevels.Add(newLevel);
            
            if (activeLevels.Count > maxActiveLevels)
            {
                LevelControl oldLevel = activeLevels[0];
                activeLevels.RemoveAt(0);
                oldLevel.Unload();
                levelFactory.ReturnLevelToPool(oldLevel);
            }
            IncreaseDifficulty();
        }
        else
        {
            Debug.LogError("Failed to create new level");
        }
    }

    private void Update()
    {
        MoveActiveLevels();
        CheckForNewLevel();
    }

    private void MoveActiveLevels()
    {
        float distanceToMove = levelSpeed * Time.deltaTime;
        totalDistance += distanceToMove;
        foreach (var level in activeLevels)
        {
            level.Move(distanceToMove);
        }
    }

    private void CheckForNewLevel()
    {
        if (activeLevels.Count == 0 || totalDistance >= activeLevels[0].Length)
        {
            LoadNextLevel();
            totalDistance = 0f;
        }
    }

    private void IncreaseDifficulty()
    {
        if (currentDifficulty >= maxDifficulty) return;
        currentDifficulty++;
        levelSpeed += 0.5f;
    }
}