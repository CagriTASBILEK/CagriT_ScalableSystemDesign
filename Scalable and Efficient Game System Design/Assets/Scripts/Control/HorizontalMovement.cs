using Interface;
using UnityEngine;

public class HorizontalMovement : IMovementStrategy
{
    private float moveDistance;

    public HorizontalMovement(float moveDistance)
    {
        this.moveDistance = moveDistance;
    }

    public void Move(Transform transform, Rigidbody rb, float direction)
    {
        Vector3 newPosition = transform.position + Vector3.right * direction * moveDistance;
        transform.position = newPosition;
    }

    public bool CanMove(Transform transform)
    {
        return true;
    }
}