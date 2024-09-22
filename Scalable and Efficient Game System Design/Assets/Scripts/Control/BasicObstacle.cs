using UnityEngine;

namespace Control
{
    public class BasicObstacle : ObstacleControl
    {
        protected override void OnCollision()
        {
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