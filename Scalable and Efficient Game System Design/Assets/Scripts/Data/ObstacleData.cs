using UnityEngine;

[CreateAssetMenu(fileName = "New Obstacle", menuName = "Obstacle Data")]
public class ObstacleData : ScriptableObject
{
    public string obstacleName;
    public GameObject obstaclePrefab;
}