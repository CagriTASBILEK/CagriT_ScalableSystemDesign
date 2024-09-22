using Interface;
using UnityEngine;

public class JumpMovement : IMovementStrategy
{
    private float jumpHeight;
    private float groundCheckDistance = 1f;
    private LayerMask groundLayer;

    public JumpMovement(float jumpHeight, LayerMask groundLayer)
    {
        this.jumpHeight = jumpHeight;
        this.groundLayer = groundLayer;
    }

    public void Move(Transform transform, Rigidbody rb, float direction)
    {
        Vector3 jumpPosition = transform.position + Vector3.up * jumpHeight;
        transform.position = jumpPosition;
        rb.isKinematic = false;
        rb.velocity = Vector3.zero;
    }

    public bool CanMove(Transform transform)
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }
}