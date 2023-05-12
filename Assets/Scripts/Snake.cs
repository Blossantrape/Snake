using System;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2Int gridPosition;

    private void Awake()
    {
        gridPosition = new Vector2Int(10, 10);
    }

    private void Update()
    {
        transform.position = new Vector3(gridPosition.x, gridPosition.y);
    }
}
