using UnityEngine;

public class EnemyEvents : MonoBehaviour
{
    private EnemyAI _enemyAI;

    private void Awake()
    {
        _enemyAI = GetComponentInParent<EnemyAI>();
    }

    public void OnEndAttack()
    {
        _enemyAI.OnEndAttack();
    }
}
