using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private Rigidbody2D _rb;
    private Vector2 _moveInput;

    private bool _isRunning;
    private float _moveSpeed = 5f;
    private float _minMovingSpeed = 0.1f;

    private void Awake()
    {
        Instance = this;

        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (PlayerCombat.Instance.IsDashing())
            return;
        Move();
    }

    private void Move()
    {
        _moveInput = GameInput.Instance.GetMoveDirection();
        _rb.MovePosition(_rb.position + _moveInput * (_moveSpeed * Time.fixedDeltaTime));
        if(Mathf.Abs(_moveInput.x) > _minMovingSpeed || Mathf.Abs(_moveInput.y) > _minMovingSpeed)
        {
            _isRunning = true;
        }
        else
        {
            _isRunning = false;
        }
    }

    public bool IsRunning()
    {
        return _isRunning;
    }
}
