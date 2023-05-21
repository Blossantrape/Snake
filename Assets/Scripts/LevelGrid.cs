using UnityEngine;

public class LevelGrid
{
    private Vector3 foodGridPosition; // Позиция объекта (яблока).
    private GameObject foodGameObject;
    private float wight; // Высота сетки.
    private float height; // Ширина сетки.
    private Snake snake; // Ссылка змеи, пометка.

    public LevelGrid(float wight, float height)
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
            foodGridPosition = new Vector3(Random.Range(0,wight), Random.Range(0, height), 0.9f);
        } while (snake.GetGridPosition() == foodGridPosition);
        
        // Создание объекта с именем и компонентом рендера спрайта.
        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.i.foodSprite; // Подкл. компонента.
        // Изменение позиции объекта (яблока).
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.x, foodGridPosition.z); 
    }
    
    /// <summary>
    /// Метод проверки позиций змеи и еды между собой.
    /// </summary>
    /// <param name="snakeGridPosition"></param>
    public void SnakeMoved(Vector3 snakeGridPosition)
    {
        if (snakeGridPosition == foodGridPosition) // Если позиция змеи и еды одинаковая
        {
            Object.Destroy(foodGameObject); // Удаление объекта еды.
            SpawnFood(); // Спавнв новой.
            Debug.Log("Snake ate Food"); // Отладка.
        }
    }

}
