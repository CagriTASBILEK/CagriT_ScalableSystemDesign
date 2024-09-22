using UnityEngine;

namespace Interface
{
    public interface IObstacle
    {
        void Initialize(Vector3 localPosition);
        void Activate();
        void Deactivate();
    }
}