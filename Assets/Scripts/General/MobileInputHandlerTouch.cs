using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.TouchPhase;

public class MobileInputHandlerTouch : MonoBehaviour
{
    public static event System.Action<SwipeDirection> OnSwipe;
    
    private Vector2 _touchStartPos;

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }
    
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _touchStartPos = touch.position;
                    break;
                case TouchPhase.Ended:
                    Vector2 swipeDelta = touch.position - _touchStartPos;

                    if (swipeDelta.magnitude > 100) // Минимальная длина свайпа.
                    {
                        float angel = Mathf.Atan2(swipeDelta.y, swipeDelta.x) * Mathf.Rad2Deg;

                        if (angel < 45 && angel > -45)
                        {
                            // Свайп вправо.
                            OnSwipe?.Invoke(SwipeDirection.Right);
                        }
                        else if (angel < -45 && angel > -135)
                        {
                            // Свайп вниз.
                            OnSwipe?.Invoke(SwipeDirection.Down);
                        }
                        else if (angel > 45 && angel < 135)
                        {
                            // Свайп вверх.
                            OnSwipe?.Invoke(SwipeDirection.Up);
                        }
                        else
                        {
                            // Свайп влево.
                            OnSwipe?.Invoke(SwipeDirection.Left);
                        }
                    }
                    break;
            }
        }
    }
}