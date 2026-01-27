using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int hp = 10;

    public void TakeDamage(int damage)
    {
        hp -= damage;
        Debug.Log("Player HP " + hp);

        if (hp <= 0)
        {
            Debug.Log("Player Died");
        }
    }
}
