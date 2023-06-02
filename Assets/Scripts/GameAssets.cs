using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets i;

    private void Awake()
    {
        i = this;
    }

    public Sprite snakeHeadSprite;
    public Sprite SnakeBodySprite;
    public Sprite foodSprite;
}
