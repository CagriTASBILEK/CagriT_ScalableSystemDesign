using System;

namespace Event
{
    public static class GameEvents
    {
        public static event Action OnPlayerJump;
        public static event Action<float> OnPlayerMove;
        public static event Action<int> OnPlayerTakeDamage;
        public static event Action<int> OnPlayerHealthChanged;

        public static void InvokePlayerJump()
        {
            OnPlayerJump?.Invoke();
        }
        public static void InvokePlayerMove(float direction)
        {
            OnPlayerMove?.Invoke(direction);
        }
        public static void InvokePlayerTakeDamage(int remainingHealth)
        {
            OnPlayerTakeDamage?.Invoke(remainingHealth);
        }
        public static void InvokePlayerHealthChanged(int currentHealth)
        {
            OnPlayerHealthChanged?.Invoke(currentHealth);
        }
    
    }
}