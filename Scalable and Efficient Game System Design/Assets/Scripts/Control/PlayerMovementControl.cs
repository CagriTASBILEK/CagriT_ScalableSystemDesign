using Event;
using Interface;
using UnityEngine;

namespace Control
{
    public class PlayerMovementControl : MonoBehaviour, IPlayerMovement
    {
        [SerializeField] private float moveDistance = 1f;
        [SerializeField] private float jumpHeight = 1f;
        [SerializeField] private LayerMask groundLayer;
        
        private IMovementStrategy horizontalMovementStrategy;
        private IMovementStrategy jumpMovementStrategy;
        private Rigidbody rb;
        private Vector3 initialPosition;
        private bool isGrounded = true;
        private int currentLane = 0;
        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            horizontalMovementStrategy = new HorizontalMovement(moveDistance);
            jumpMovementStrategy = new JumpMovement(jumpHeight, groundLayer);
        }
        private void Start()
        {
            initialPosition = transform.position;
        }
        
        private void FixedUpdate()
        {
            
            CheckGrounded();
            if (isGrounded)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
                rb.velocity = Vector3.zero;
                rb.isKinematic = true;
            }
        }
        
        public void MoveHorizontal(float direction)
        {
            int targetLane = currentLane + (int)Mathf.Sign(direction);
            if (targetLane >= -1 && targetLane <= 1 && horizontalMovementStrategy.CanMove(transform))
            {
                horizontalMovementStrategy.Move(transform, rb, direction);
                currentLane = targetLane;
            }
        }

        public void Jump()
        {
            if (jumpMovementStrategy.CanMove(transform))
            {
                jumpMovementStrategy.Move(transform, rb, 0);
                isGrounded = false;
            }
        }

        public bool IsGrounded => isGrounded;

        private void CheckGrounded()
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f, groundLayer);
        }
        
    }
}