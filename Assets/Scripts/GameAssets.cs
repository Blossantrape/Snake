using UnityEngine;
using UnityEngine.Serialization;

public class GameAssets : MonoBehaviour
{
    public static GameAssets I;

    private void Awake()
    {
        I = this;
    }

    public Sprite snakeHeadSprite;
    public Sprite snakeBodySprite;
    public Sprite foodSprite;
}
