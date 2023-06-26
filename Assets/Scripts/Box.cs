using System;
using UnityEngine;

public class Box : MonoBehaviour
{
    private static Box _instance;

    private Vector2Int _boxGridPosition;
    private GameObject _boxGameObject;
    
    private LevelGrid _levelGrid;
    private GameHandler _gameHandler;

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        SpawnBox(_levelGrid._width, _levelGrid._height);
    }

    private void SpawnBox(int widthBox, int heightBox)
    {
        _boxGridPosition = new Vector2Int(widthBox/2, heightBox/2);
        Debug.Log("End creating position of The box");
        _boxGameObject = new GameObject("Box", typeof(SpriteRenderer));
        _boxGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.I.boxSprite;
        Debug.Log("End creating The box");
        
        _boxGameObject.transform.position = new Vector3Int(_boxGridPosition.x, _boxGridPosition.y);
        Debug.Log("End teleporting The box");
    }
}
