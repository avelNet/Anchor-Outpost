using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    public static EnemyVisual Instance { get; private set; }

    private SpriteRenderer _sprite;
    private Animator _animator;
    private float _moveDir;

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();
        _sprite = GetComponent<SpriteRenderer>();
    }

    public void flipXEnemy(Vector2 direction)
    {
        Vector3 scale = transform.localScale;
        if (direction.x > 0)
        {
            scale.x = 1;
        }
        else if (direction.x < 0)
        {
            scale.x = -1;
        }
        else
        {
            Debug.LogWarning("Error");
        }

        transform.localScale = scale;
    }

    public void TakeHit()
    {
        _animator.SetTrigger("TakeHit");
    }
}
