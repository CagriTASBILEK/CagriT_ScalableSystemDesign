using UnityEngine;
using System.Collections.Generic;
using Factory;
using Interface;

namespace Control
{
    public abstract class LevelControl : MonoBehaviour, ILevel
    {
        [SerializeField] protected float levelLength = 10f;
        [SerializeField] protected float laneWidth = 1f;
        [SerializeField] protected int laneCount = 3;
        [SerializeField] protected int baseObstacleCount = 3;
        [SerializeField] protected float obstacleCountIncreaseRate = 0.5f;
        [SerializeField] protected float minDistanceBetweenObstacles = 3f;
        protected List<IObstacle> obstacles = new List<IObstacle>();
        protected ObstacleFactory obstacleFactory;
        public float Length => levelLength;

        public virtual void Initialize(int difficulty, ObstacleFactory factory)
        {
            this.obstacleFactory = factory;
            PlaceObstacles(difficulty);
        }
        
        public virtual void Unload()
        {
            foreach (var obstacle in obstacles)
            {
                obstacle.Deactivate();
                obstacleFactory.ReturnObstacleToPool((obstacle as MonoBehaviour)?.gameObject);
            }
            obstacles.Clear();
        }

        public virtual void Move(float distance)
        {
            transform.Translate(Vector3.back * distance);
        }
        
        public float GetEndPosition()
        {
            return transform.position.z + levelLength;
        }

        protected abstract void PlaceObstacles(int difficulty);
        
        protected Vector3 GetPositionInLaneWithMinDistance(float zPos)
        {
            int lane = Random.Range(0, laneCount);
            float xPos = (lane - 1) * laneWidth;
            return new Vector3(xPos, 0.5f, zPos);
        }
        
        protected int GetObstacleCountForDifficulty(int difficulty)
        {
            return Mathf.RoundToInt(baseObstacleCount + (difficulty - 1) * obstacleCountIncreaseRate);
        }
    }
}