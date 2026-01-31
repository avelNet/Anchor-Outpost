using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    public int Damage = 10;

    private Collider2D _collider;

    private Transform _player;
    private Transform _enemy;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;
    }

    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    public void DisableColldier()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
    }
}
