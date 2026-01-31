using UnityEngine;

public class GetCollider : MonoBehaviour
{
    public Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }
}
