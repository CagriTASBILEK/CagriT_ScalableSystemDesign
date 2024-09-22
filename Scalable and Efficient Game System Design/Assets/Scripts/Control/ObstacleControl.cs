using Interface;
using UnityEngine;

namespace Control
{
    public abstract class ObstacleControl : MonoBehaviour, IObstacle
    {
        protected bool isActive = false;

        public virtual void Initialize(Vector3 localPosition)
        {
            transform.localPosition = localPosition;
        }

        public virtual void Activate()
        {
            isActive = true;
            gameObject.SetActive(true);
        }

        public virtual void Deactivate()
        {
            isActive = false;
            gameObject.SetActive(false);
        }
        protected abstract void OnCollision();
    }
}