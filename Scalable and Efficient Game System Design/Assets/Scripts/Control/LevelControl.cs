using UnityEngine;
using System.Collections.Generic;

namespace Control
{
    public abstract class LevelControl : MonoBehaviour, ILevel
    {
        [SerializeField] protected float levelLength = 10f;
        [SerializeField] protected float laneWidth = 1f;
        [SerializeField] protected int laneCount = 3;
        public float Length => levelLength;

        public virtual void Initialize(int difficulty)
        {
        }
        public virtual void Unload()
        {
        }

        public virtual void Move(float distance)
        {
            transform.Translate(Vector3.back * distance);
        }
        
        public float GetEndPosition()
        {
            return transform.position.z + levelLength;
        }

        protected abstract void PlaceObstacles(int difficulty);

        protected Vector3 GetRandomPositionInLane()
        {
            int lane = Random.Range(0, laneCount);
            float xPos = (lane - 1) * laneWidth;
            return new Vector3(xPos, 0.5f, Random.Range(0, -levelLength));
        }
    }
}