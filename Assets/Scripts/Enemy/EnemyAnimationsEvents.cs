using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private EnemyAttack _enemyAttack;

    private void Awake()
    {
        _enemyAttack = GetComponentInChildren<EnemyAttack>();
        _enemyAI = GetComponentInParent<EnemyAI>();
    }

    public void OnEndAttack()
    {
        _enemyAI.OnEndAttack();
    }

    public void OnEnableAttackCollider()
    {
        _enemyAttack.EnableAttackCollider();
    }
    public void OnDisableAttackCollider()
    {
        _enemyAttack.DisableAttackCollider();
    }
}
