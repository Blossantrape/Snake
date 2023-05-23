using UnityEngine;

public class LevelGrid
{
    private Vector2Int foodGridPosition; // Позиция объекта (яблока).
    private GameObject foodGameObject;
    private int wight; // Высота сетки.
    private int height; // Ширина сетки.
    private Snake snake; // Ссылка змеи, пометка.

    public LevelGrid(int wight, int height)
    {
        this.wight = wight;
        this.height = height;
    }

    public void Setup(Snake snake) // Ссылка змеи, пометка.
    {
        this.snake = snake; // Ссылка змеи, пометка.
        SpawnFood();
    }
    
    /// <summary>
    /// Спавнер объетов (яблока).
    /// </summary>
    private void SpawnFood()
    {
        do { // Сначала идёт генерация, а потом проверка.
            // Генерация местоположения для игрового объекта (яблока).
            foodGridPosition = new Vector2Int(Random.Range(0,wight), Random.Range(0, height));
        } while (snake.GetGridPosition() == foodGridPosition);
        
        // Создание объекта с именем и компонентом рендера спрайта.
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite; // Подкл. компонента.
        // Изменение позиции объекта (яблока).
        foodGameObject.transform.position = new Vector2Int(foodGridPosition.x, foodGridPosition.y); 
    }
    
    /// <summary>
    /// Метод проверки позиций змеи и еды между собой.
    /// </summary>
    /// <param name="snakeGridPosition"></param>
    public void SnakeMoved(Vector2Int snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition) // Если позиция змеи и еды одинаковая
        {
            Object.Destroy(foodGameObject); // Удаление объекта еды.
            SpawnFood(); // Спавнв новой.
            Debug.Log("Snake ate Food"); // Отладка.
        }
    }

}
