using System;
using UnityEngine;
using UnityEngine.UI;

namespace General
{
    public class MobileInputHandleGamePad : MonoBehaviour
    {
        private MobileInputHandleGamePad _instance;
        [SerializeField] private Snake _snake;
        
        private void Awake()
        {
            _instance = this;
            _snake = GetComponent<Snake>();
        }

        public void OnUpButtonDown()
        {
            if (_snake._gridMoveDirection != Snake.Direction.Down) // Не позволяет поворачиваться на 180 градусов.
            {
                _snake._gridMoveDirection = Snake.Direction.Up;
            }
        }
        
        public void OnDownButtonDown()
        {
            if (_snake._gridMoveDirection != Snake.Direction.Up) // Не позволяет поворачиваться на 180 градусов.
            {
                _snake._gridMoveDirection = Snake.Direction.Down;
            }
        }
        
        public void OnLeftButtonDown()
        {
            if (_snake._gridMoveDirection != Snake.Direction.Right) // Не позволяет поворачиваться на 180 градусов.
            {
                _snake._gridMoveDirection = Snake.Direction.Left;
            }
        }
        
        public void OnRightButtonDown()
        {
            if (_snake._gridMoveDirection != Snake.Direction.Left) // Не позволяет поворачиваться на 180 градусов.
            {
                _snake._gridMoveDirection = Snake.Direction.Right;
            }
        }
    }
}