using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    public static EnemyVisual Instance { get; private set; }

    private SpriteRenderer _sprite;
    private float _moveDir;

    private void Awake()
    {
        Instance = this;

        _sprite = GetComponent<SpriteRenderer>();
    }

    public void flipXEnemy(Vector2 direction)
    {
        if(direction.x < 0)
        {
            _sprite.flipX = true;
        }
        else if (direction.x > 0)
        {
            _sprite.flipX = false;
        }
        else
        {
            Debug.LogWarning("Error");
        }
    }
}
