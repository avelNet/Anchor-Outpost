using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySo", menuName = "Scriptable Objects/EnemySo")]
public class EnemySO : ScriptableObject
{
    public int agroRange;
    public int attackRange;
    public int walkingRange;
    public float movingEnemySpeed;
    public float enemyHit;
    public float maxHPEnemy;
}
