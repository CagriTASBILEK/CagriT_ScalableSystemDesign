using Control;
using Event;
using Interface;
using Manager;
using UnityEngine;

[RequireComponent(typeof(PlayerMovementControl))]
public class PlayerController : MonoBehaviour
{
    private IPlayerMovement playerMovement;
    private IInputHandler inputHandler;
    
    [SerializeField] private int maxHealth = 100;
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
        GameEvents.OnPlayerTakeDamage += HandlePlayerTakeDamage;
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerMove -= HandlePlayerMove;
        GameEvents.OnPlayerJump -= HandlePlayerJump;
        GameEvents.OnPlayerTakeDamage -= HandlePlayerTakeDamage;
    }

    private void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Handles player input and triggers relevant events.
    /// </summary>
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

    /// <summary>
    /// Moves the player horizontally.
    /// </summary>
    private void HandlePlayerMove(float direction)
    {
        playerMovement.MoveHorizontal(direction);
    }

    /// <summary>
    /// Makes the player jump.
    /// </summary>
    private void HandlePlayerJump()
    {
        playerMovement.Jump();
    }
    
    /// <summary>
    /// Handles player taking damage.
    /// </summary>
    private void HandlePlayerTakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        GameEvents.InvokePlayerHealthChanged(currentHealth);
        if (currentHealth <= 0)
        {
            GameManager.Instance.GameRestart();
        }
    }
    
}