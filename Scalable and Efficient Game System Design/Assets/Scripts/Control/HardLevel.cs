using Interface;
using UnityEngine;

namespace Control
{
    public class HardLevel : LevelControl
    {
        protected override void PlaceObstacles(int difficulty)
        {
            int obstacleCount = GetObstacleCountForDifficulty(difficulty);
            float currentZPos = 0;

            for (int i = 0; i < obstacleCount; i++)
            {
                currentZPos -= minDistanceBetweenObstacles;

                if (currentZPos < -levelLength)
                {
                    break;
                }

                Vector3 position = GetPositionInLaneWithMinDistance(currentZPos);
                Vector3 worldPosition = transform.TransformPoint(position);
                IObstacle obstacle = obstacleFactory.CreateObstacle("BasicObstacle", worldPosition, transform);
                if (obstacle != null)
                {
                    obstacles.Add(obstacle);
                }
            }
        }
    }
}