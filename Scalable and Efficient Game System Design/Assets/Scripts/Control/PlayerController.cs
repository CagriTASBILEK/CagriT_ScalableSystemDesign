using Control;
using Event;
using Interface;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementControl))]
public class PlayerController : MonoBehaviour
{
    private IPlayerMovement playerMovement;
    private IInputHandler inputHandler;

    [SerializeField] private int score = 0;
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovementControl>();
        inputHandler = new InputHandler();
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        GameEvents.OnPlayerMove += HandlePlayerMove;
        GameEvents.OnPlayerJump += HandlePlayerJump;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerMove -= HandlePlayerMove;
        GameEvents.OnPlayerJump -= HandlePlayerJump;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        InputData inputData = inputHandler.GetInputData();

        if (inputData.HorizontalInput != 0)
        {
            GameEvents.InvokePlayerMove(inputData.HorizontalInput);
        }

        if (inputData.JumpRequested)
        {
            GameEvents.InvokePlayerJump();
        }
    }

    private void HandlePlayerMove(float direction)
    {
        playerMovement.MoveHorizontal(direction);
    }

    private void HandlePlayerJump()
    {
        playerMovement.Jump();
    }
}