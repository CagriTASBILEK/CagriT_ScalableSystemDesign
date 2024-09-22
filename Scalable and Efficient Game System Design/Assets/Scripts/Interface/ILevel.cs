using Factory;
using UnityEngine;

public interface ILevel
{
    float Length { get; }
    void Initialize(int difficulty, ObstacleFactory obstacleFactory);
    void Unload();
    void Move(float distance);
    float GetEndPosition();
}