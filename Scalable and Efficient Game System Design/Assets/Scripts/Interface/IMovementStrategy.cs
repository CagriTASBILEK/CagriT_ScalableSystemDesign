using UnityEngine;
using System.Collections;

namespace Interface
{
    public interface IMovementStrategy
    {
        void Move(Transform transform, Rigidbody rb, float direction);
        bool CanMove(Transform transform);
    }
}