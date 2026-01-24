using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private State _currentState;
    private Animator _animator;
    private Transform _player;
    private NavMeshAgent _agent;
    private Vector3 _walkPoint;
    private bool _isWalkToPoint;
    [SerializeField] private EnemySO _enemyData;

    [Header("States")]
    private float _maxHP;
    private float _speedEnemy;
    private int _agroRange;
    private int _attackRange;
    private int _walkingRange;

    public enum State
    {
        Idle,
        Walking,
        Chasing,
        Attacking,
        Death
    }

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player").transform;
        _animator = GetComponentInChildren<Animator>();
        _currentState = State.Idle;
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _maxHP = _enemyData.maxHPEnemy;
        _speedEnemy = _enemyData.movingEnemySpeed;
        _agroRange = _enemyData.agroRange;
        _attackRange = _enemyData.attackRange;
        _walkingRange = _enemyData.walkingRange;
    }

    private void Update()
    {
        SwitchState();
    }

    private void SwitchState()
    {
        switch (_currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Walking:
                Walking();
                break;
            case State.Chasing:
                Chasing();
                break;
            case State.Attacking:
                Attack();
                break;
            case State.Death:
                Death();
                break;
        }
    }

    private void Idle()
    {
        _animator.SetBool("isWalking", false);
        _agent.ResetPath();
        
        if(CalcDistance() <= _walkingRange)
        {
            _currentState = State.Walking;
        }
    }

    private void Walking()
    {
        
    }


    private void Chasing()
    {
        
    }

    private void Attack()
    {

    }

    private void Death()
    {

    }

    private float CalcDistance()
    {
        float distance = Vector2.Distance(transform.position, _player.position);
        return distance;
    }
}
