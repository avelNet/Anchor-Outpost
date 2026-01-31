using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private EnemyAI _enemyAI;
    private AttackHitbox _attackHitbox;

    private void Awake()
    {
        _attackHitbox = GetComponentInChildren<AttackHitbox>();
        _enemyAI = GetComponentInParent<EnemyAI>();
    }

    public void OnEndAttack()
    {
        _enemyAI.OnEndAttack();
    }

    public void EnableCollider()
    {
        _attackHitbox.EnableCollider();
    }
    public void DisableCollder()
    {
        _attackHitbox.DisableColldier();
    }
}
