using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat Instance {  get; private set; }

    [SerializeField] private float dashDistance = 2.5f;
    [SerializeField] private float dashDuration = 0.2f;

    private float dashTimer;
    private bool _isDashing;
    private bool _canDash = true;
    private float _canDashTimer = 0f;
    private float _dashCoolDown = 2f;
    private bool _isRunning;
    private bool _isFlipX;

    private Animator _animator;
    private Rigidbody2D _rb;

    private Vector2 dashStartPos;
    private Vector2 dashTargetPos;

    private const string ATTACK = "Attack";
    private const string DASH_ATTACK = "DashAttack";


    private void Awake()
    {
        Instance = this;

        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _isRunning = Player.Instance.IsRunning();
        HandleInput();

        _isFlipX = PlayerVisual.Instance.flipXPlayer();

        if(!_canDash)
        {
            _canDashTimer += Time.deltaTime;
            if(_canDashTimer >= _dashCoolDown)
            {
                _canDash = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (_isDashing)
        {
            ContinueDash();
        }
        _canDashTimer += Time.fixedDeltaTime;
    }

    private void HandleInput()
    {
        if (_isDashing) return;

        if (Input.GetButtonUp("Fire1") && !_isRunning)
        {
            Attack();
            _animator.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(_canDash)
            {
                DashAttack();
                _animator.SetTrigger(DASH_ATTACK);

                _canDash = false;
                _canDashTimer = 0f;
            }
        }
    }

    private void Attack()
    {
        _animator.SetTrigger(ATTACK);
    }

    private void DashAttack()
    {
        Debug.Log("DashDistance: " + dashDistance);

        _animator.SetTrigger(DASH_ATTACK);

        _isDashing = true;
        dashTimer = 0f;

        dashStartPos = _rb.position;
        if(_isFlipX == true)
        {
            dashTargetPos = dashStartPos + (Vector2)(-transform.right) * dashDistance;
        }
        else if (_isFlipX == false)
        {
            dashTargetPos = dashStartPos + (Vector2)transform.right * dashDistance;
        }
        else
        {
            Debug.Log("Error");
        }
    }

    private void ContinueDash()
    {
        dashTimer += Time.fixedDeltaTime; // fixedDeltaTime != deltaTime, fixedDeltaTime = 1 / 50 = 0.02/sek
        float t = dashTimer / dashDuration;

        if (t >= 1f) // if = true
        {
            t = 1f;
            _isDashing = false;
        }

        Vector2 newPos = Vector2.Lerp(dashStartPos, dashTargetPos, t);
        _rb.MovePosition(newPos);
    }

    public bool IsDashing()
    {
        return _isDashing;
    }
}
