using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private int initialDifficulty = 1;
    [SerializeField] private float levelSpeed = 5f;
    [SerializeField] private int maxActiveLevels = 3;
    [SerializeField] private float distanceBetweenLevels = 2f; 
    [SerializeField] private int maxDifficulty = 20;
    
    private LevelFactory levelFactory;
    private List<ILevel> activeLevels = new List<ILevel>();
    private int currentDifficulty;
    private float totalDistance = 0f;

    private void Awake()
    {
        levelFactory = new LevelFactory(objectPool);
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
        ILevel newLevel = levelFactory.CreateLevel(currentDifficulty);
        if (newLevel != null)
        {
            float zPosition = activeLevels.Count > 0 
                ? activeLevels[activeLevels.Count - 1].GetEndPosition() + distanceBetweenLevels
                : 0f;
            (newLevel as MonoBehaviour).transform.position = new Vector3(0, 0, zPosition);
            activeLevels.Add(newLevel);

            if (activeLevels.Count > maxActiveLevels)
            {
                ILevel oldLevel = activeLevels[0];
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
        if (activeLevels.Count == 0 || totalDistance >= activeLevels[0].Length + distanceBetweenLevels)
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