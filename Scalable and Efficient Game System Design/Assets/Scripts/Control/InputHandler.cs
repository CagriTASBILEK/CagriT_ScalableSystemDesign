using Interface;
using UnityEngine;

namespace Control
{
    public class InputHandler : IInputHandler
    {
        private Vector2 startMousePosition;
        private Vector2 endMousePosition;
        private bool isDragging = false;
        private float swipeThreshold = 50f;

        public InputData GetInputData()
        {
            InputData inputData = new InputData();

            if (Input.GetMouseButtonDown(0))
            {
                startMousePosition = Input.mousePosition;
                isDragging = true;
            }
            else if (Input.GetMouseButtonUp(0) && isDragging)
            {
                endMousePosition = Input.mousePosition;
                Vector2 swipeDelta = endMousePosition - startMousePosition;

                if (swipeDelta.magnitude > swipeThreshold)
                {
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        inputData.HorizontalInput = Mathf.Sign(swipeDelta.x);
                    }
                    else if (swipeDelta.y > 0)
                    {
                        inputData.JumpRequested = true;
                    }
                }

                isDragging = false;
            }

            return inputData;
        
        }
    }
}