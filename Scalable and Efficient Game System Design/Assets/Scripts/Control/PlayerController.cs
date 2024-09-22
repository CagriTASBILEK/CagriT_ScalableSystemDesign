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