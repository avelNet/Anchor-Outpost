using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static EnemyHealth Instance { get; private set; }

    [SerializeField] private int maxHP = 100;
    private int _currentHP;
    private bool _isDied;

    private Animator _animator;
    private Collider2D _collider2D;
    private Rigidbody2D _rigidbody2D;

    private EnemyAI _enemyAI;
    private GetCollider _getCollider;

    private void Awake()
    {
        Instance = this;

        _getCollider = GetComponentInChildren<GetCollider>();
        _enemyAI = GetComponent<EnemyAI>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _animator = GetComponentInChildren<Animator>();
        _currentHP = maxHP;
        _getCollider._collider.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        if (_isDied) return;

        _currentHP -= damage;
        Debug.Log("Enemy HP " + _currentHP);

        if (_currentHP <= 0)
        {
            Die();
        }
        else
        {
            _enemyAI.CancelAttack();
            _animator.SetTrigger("TakeHit");
        }
    }

    private void Die()
    {
        _isDied = true;
        _animator.SetTrigger("Dead");
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        _enemyAI.enabled = false;
        _collider2D.enabled = false;
        _getCollider._collider.enabled = true;
    }
}
