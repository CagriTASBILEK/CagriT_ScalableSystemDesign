using System.Collections.Generic;
using Interface;
using UnityEngine;

namespace Factory
{
    /// <summary>
    /// Factory class used to create and manage obstacles.
    /// </summary>
    public class ObstacleFactory
    {
        private ObjectPool objectPool;
        private List<ObstacleData> obstacleDataList;

        /// <summary>
        /// Constructor for the ObstacleFactory class.
        /// </summary>
        public ObstacleFactory(ObjectPool objectPool)
        {
            this.objectPool = objectPool;
            this.obstacleDataList = new List<ObstacleData>();
            LoadObstacleDataFromResources();
        }
        
        /// <summary>
        /// Loads obstacle data from the Resources folder.
        /// </summary>
        private void LoadObstacleDataFromResources()
        {
            ObstacleData[] obstacleDatas = Resources.LoadAll<ObstacleData>("ObstacleDatas");
            for (int i = 0; i < obstacleDatas.Length; i++)
            {
                obstacleDataList.Add(obstacleDatas[i]);
                objectPool.AddPrefab(obstacleDatas[i].obstacleName, obstacleDatas[i].obstaclePrefab);
            }
        }

        /// <summary>
        /// Creates a new obstacle of the specified type at the given position.
        /// </summary>
        public IObstacle CreateObstacle(string type, Vector3 position, Transform parent)
        {
            ObstacleData data = null;
            for (int i = 0; i < obstacleDataList.Count; i++)
            {
                if (obstacleDataList[i].obstacleName == type)
                {
                    data = obstacleDataList[i];
                    break;
                }
            }

            if (data == null)
            {
                return null;
            }
            
            GameObject obstacleObject = objectPool.SpawnFromPool(data.obstacleName, position, Quaternion.identity, parent);
    
            if (obstacleObject == null)
            {
                return null;
            }

            IObstacle obstacle = obstacleObject.GetComponent<IObstacle>();
            if (obstacle == null)
            {
                return null;
            }

            obstacle.Initialize(obstacleObject.transform.localPosition);
            obstacle.Activate();
            return obstacle;
        }

        /// <summary>
        /// Returns the obstacle to the object pool.
        /// </summary>
        public void ReturnObstacleToPool(GameObject obstacleObject)
        {
            for (int i = 0; i < obstacleDataList.Count; i++)
            {
                if (obstacleDataList[i].obstaclePrefab.name == obstacleObject.name)
                {
                    objectPool.ReturnToPool(obstacleDataList[i].obstacleName, obstacleObject);
                    return;
                }
            }
        }
    }
}