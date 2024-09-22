using UnityEngine;

namespace Interface
{
    public interface IPlayerMovement
    {
        void MoveHorizontal(float direction);
        void Jump();
        bool IsGrounded { get; }
    }
}