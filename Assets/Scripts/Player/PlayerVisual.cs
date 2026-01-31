using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    public static PlayerVisual Instance { get; private set; }

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _inputMove;
    private bool _isFlipX;

    private const string IS_RUNNING = "isRunning";

    private void Awake()
    {
        Instance = this;

        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());
        flipXPlayer();
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public bool flipXPlayer()
    {
        _inputMove = GameInput.Instance.GetMoveDirection();

        if (_inputMove.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            _isFlipX = false;
        }
        else if (_inputMove.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            _isFlipX = true;
        }
        return _isFlipX;
    }
}
