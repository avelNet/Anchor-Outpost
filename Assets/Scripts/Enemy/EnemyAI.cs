using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _moveEnemySpeed = 3f;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _obstacleCheckDistance = 2f;

    private State _currentState;
    private Rigidbody2D _rb;
    private Animator _animator;
    private Transform _player;

    private float _walkingRange = 5f;
    private float _attackRange = 1f;
    private float _walkRadius = 5f;
    private float _patrolStopDistance = 0.2f;
    private float _stuckTimer;
    private float _stuckTimeLimit = 0.5f;

    private Vector2 _currentPosition;
    private Vector2 _direction;
    private Vector2 _patrolTarget;
    private Vector2 _lastPosition;

    private bool _isAttacking;

    private const string ATTACK = "Attack";

    public enum State
    {
        Idle,
        Chasing,
        Patrol,
        Attack
    }

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _currentState = State.Idle;
    }

    private void Update()
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
            case State.Patrol:
                Patrol();
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

        if (dist <= _walkingRange && dist > _attackRange)
        {
            _currentState = State.Chasing;
        }
        if (dist > _walkingRange)
        {
            _currentState = State.Patrol;
        }
    }

    private void Patrol()
    {
        _animator.SetBool("isWalking", true);

        Vector2 dir = (_patrolTarget - (_rb.position)).normalized;

        if(IsObstacleAHead(dir))
        {
            _patrolTarget = GetRandomDirectionPoint();
            return;
        }

        EnemyVisual.Instance.flipXEnemy(dir);

        _rb.linearVelocity = dir * _moveEnemySpeed;

        CheckIfStuck();

        if (Vector2.Distance(_rb.position, _patrolTarget) <= _patrolStopDistance)
        {
            _patrolTarget = GetRandomDirectionPoint();
        }

        float dist = Vector2.Distance(_player.position, transform.position);
        if (dist <= _walkingRange && dist > _attackRange)
        {
            _currentState = State.Chasing;
        }
    }

    private void CheckIfStuck()
    {
        if(Vector2.Distance(_rb.position, _lastPosition) < 0.01f)
        {
            _stuckTimer += Time.deltaTime;
            if(_stuckTimer >= _stuckTimeLimit)
            {
                _patrolTarget = GetRandomDirectionPoint();
                _stuckTimer = 0f;
            }
        }
        else
        {
            _stuckTimer = 0f;
            _lastPosition = _rb.position;
        }
    }

    private Vector2 GetRandomDirectionPoint()
    {
        Vector2 randomPoint = (Vector2)transform.position + Random.insideUnitCircle * _walkRadius;
        return randomPoint;
    }

    private bool IsObstacleAHead(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            dir,
            _obstacleCheckDistance,
            _obstacleMask
        );

        return hit.collider != null;
    }

    private void Chasing()
    {
        _animator.SetBool("isChasing", true);

        float dist = Vector2.Distance(_player.position, transform.position);
        _direction = (_player.position - (Vector3)_rb.position).normalized;
        _rb.linearVelocity = _direction * _moveEnemySpeed;

        if(dist <= _attackRange)
        {
            _currentState = State.Attack; 
        }
        if (dist > _walkingRange)
        {
            _currentState = State.Patrol;
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
