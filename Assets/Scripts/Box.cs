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
        /*int widthBox = _levelGrid._width;
        int heightBox = _levelGrid._height;
        /*CreateBorder(widthBox, heightBox);#1#*/
    }
    
    public void CreateBorder(int widthBox, int heightBox)
    {
        GameObject borderContainer = new GameObject("borderContainer");
        borderContainer.transform.parent = transform;
        // Создание вертикальной стены.
        for (int y = -1; y <= heightBox; y++)
        {
            SpawnBox(-1, y, borderContainer);
            SpawnBox(widthBox/* - 1*/, y, borderContainer);
        }
        
        // Создание горизонтальной стены.
        for (int x = -1; x <= widthBox; x++)
        {
            SpawnBox(x, -1, borderContainer);
            SpawnBox(x, heightBox/* - 1*/, borderContainer);
        }
    }

    private void SpawnBox(int widthBox, int heightBox, GameObject parent)
    {
        _boxGridPosition = new Vector2Int(widthBox, heightBox);
        _boxGameObject = new GameObject("Box", typeof(SpriteRenderer));
        _boxGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.I.boxSprite;
        _boxGameObject.transform.position = new Vector3Int(_boxGridPosition.x, _boxGridPosition.y);
        _boxGameObject.transform.parent = parent.transform;
    }

    /*public void SetParentForBoxes()
    {
        GameObject[] boxObjects = GameObject.FindGameObjectsWithTag("Box");
        foreach (GameObject boxObject in boxObjects)
        {
            // boxObject.tag = "Box";
            boxObject.transform.parent = transform.parent;
        }
    }*/
}
