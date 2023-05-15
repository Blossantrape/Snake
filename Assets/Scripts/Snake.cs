using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridMoveDirection; // Направление змейки.
    private Vector3Int gridPosition; // Позиция змейки.
    private float gridMoveTimer; // Время для автоматического премещения змейки.
    private float gridMoveTimerMax; // Её максимальное значение.
    
    private void Awake()
    {
        gridPosition = new Vector3Int(10, 10, 1);
        gridMoveTimerMax = 1f;
        gridMoveTimer = gridMoveTimerMax;
        gridMoveDirection = new Vector2Int(1, 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gridPosition.y += 1;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gridPosition.y -= 1;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gridPosition.x -= 1;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gridPosition.x += 1;
        }

        gridMoveTimer = Time.deltaTime;
        if (gridMoveTimer >= gridMoveTimerMax)
        {
            
        }
        
        transform.position = new Vector3(gridPosition.x, gridPosition.y, gridPosition.z);
    }
}
