using Event;
using UnityEngine;

namespace Control
{
    public class BasicObstacle : ObstacleControl
    {
        [SerializeField] private int damage = 10;

        protected override void OnCollision()
        {
            GameEvents.InvokePlayerTakeDamage(damage);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && isActive)
            {
                OnCollision();
            }
        }
    }
}