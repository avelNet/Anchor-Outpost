using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance {  get; private set; }

    private float _moveSpeed = 5f;
    private float _minMovingSpeed = 0.1f;
    private bool _isRunning = false;

    private Rigidbody2D _rb;
    private Vector2 _inputMove;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        Instance = this;

        _playerHealth = GetComponent<PlayerHealth>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _inputMove = GameInput.Instance.GetMoveDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _inputMove * (_moveSpeed * Time.fixedDeltaTime));
        if(Mathf.Abs(_inputMove.x) > _minMovingSpeed || Mathf.Abs(_inputMove.y) > _minMovingSpeed)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyAttack"))
        {
            AttackHitbox _attackHitbox = other.GetComponent<AttackHitbox>();

            if( _attackHitbox != null )
            {
                int damage = _attackHitbox.Damage;
                _playerHealth.TakeDamage(damage);
            }
        }
    }
}
