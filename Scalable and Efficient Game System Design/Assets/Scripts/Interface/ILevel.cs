using UnityEngine;

public interface ILevel
{
    float Length { get; }
    void Initialize(int difficulty);
    void Unload();
    void Move(float distance);
    float GetEndPosition();
}