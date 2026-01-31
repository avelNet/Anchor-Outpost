using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance {  get; private set; }

    [SerializeField] private int maxHP = 100;
    private int _currentHP;
    private bool _isDied;

    private Animator _animator;

    private void Awake()
    {
        Instance = this;

        _animator = GetComponentInChildren<Animator>();
        _currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (_isDied) return;

        _currentHP -= damage;
        Debug.Log("Player HP " + _currentHP);

        if ( _currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _isDied = true;
        _animator.SetTrigger("Dead");
    }

    public bool IsDied()
    {
        return _isDied;
    }
}
