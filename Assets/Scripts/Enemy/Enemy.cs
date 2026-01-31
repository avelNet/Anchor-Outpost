using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("PlayerAttack"))
        {
            AttackHitbox _attackHitbox = other.GetComponent<AttackHitbox>();
            if(_attackHitbox != null)
            {
                int damage = _attackHitbox.Damage;
                _enemyHealth.TakeDamage(damage);
            }
        }
    }
}
