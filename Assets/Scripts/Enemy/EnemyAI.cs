using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _moveEnemySpeed = 3f;

    private State _currentState;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _player;
    private SpriteRenderer _sprite;

    private float _walkingRange = 10f;
    private float _agroRange = 3f;
    private float _lostAgroRange = 4f;
    private float _attackRange = 1f;
    private float _walkRadius = 5f;

    private Vector2 _currentPosition;
    private Vector2 _walkTarget;
    private Vector2 _direction;

    private bool _isAttacking;

    private const string ATTACK = "Attack";

    public enum State
    {
        Idle,
        Chasing,
        Attack
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _player = GameObject.FindWithTag("Player").transform;
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _currentState = State.Idle;
    }

    private void FixedUpdate()
    {
        SwitchState();
    }

    private void SwitchState()
    {
        switch(_currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        _animator.SetBool("isChasing", false);
        _rb.linearVelocity = Vector2.zero;

        float dist = Vector2.Distance(_player.position, transform.position);

        if (dist <= 5 && dist > 1)
        {
            _currentState = State.Chasing;
        }
    }

    private void Chasing()
    {
        _animator.SetBool("isChasing", true);

        float dist = Vector2.Distance(_player.position, transform.position);
        _direction = (_player.position - (Vector3)_rb.position).normalized;
        _rb.linearVelocity = _direction * _moveEnemySpeed;

        if(dist <= 1)
        {
            _currentState = State.Attack; 
        }
        if(dist >= 3)
        {
            _currentState = State.Idle; 
        }

        EnemyVisual.Instance.flipXEnemy(_direction);
    }

    private void Attack()
    {
        _rb.linearVelocity = Vector2.zero;
        _animator.SetBool("isChasing", false);
        if(!_isAttacking)
        {
            _isAttacking = true;
            _animator.SetTrigger(ATTACK);
        }
    }

    public void OnEndAttack()
    {
        _isAttacking = false; 
        float dist = Vector2.Distance(_player.position, transform.position);
        if(dist <= _attackRange)
        {
            _currentState = State.Attack; 
        }
        else
        {
            _currentState = State.Chasing;
        }
    }
}
