using UnityEngine;
using UnityEngine.UI;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance {  get; private set; }

    private PlayerInputAction _plInAction;
    private Vector2 moveInput;

    private void Awake()
    {
        Instance = this;

        _plInAction = new PlayerInputAction();
        _plInAction.Enable();
    }

    private void Update()
    {
        GetMoveDirection();
    }

    public Vector2 GetMoveDirection()
    {
        moveInput = _plInAction.Player.Move.ReadValue<Vector2>();
        return moveInput;
    }
}
