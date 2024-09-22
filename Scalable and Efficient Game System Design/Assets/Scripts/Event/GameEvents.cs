using System;

namespace Event
{
    public static class GameEvents
    {
        public static event Action<float> OnPlayerMove;
        public static event Action OnPlayerJump;

        public static void InvokePlayerMove(float direction)
        {
            OnPlayerMove?.Invoke(direction);
        }

        public static void InvokePlayerJump()
        {
            OnPlayerJump?.Invoke();
        }
    }
}