using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterInputSystem : MonoBehaviour
{

    [Header("Custom Callback")]
    [SerializeField] UnityEvent<bool> m_onPlayerRunCallback;
    [SerializeField] UnityEvent m_onPlayerInteractCallback;

    private PlayerInputActionAsset m_playerInputAction;
    private InputAction m_move;
    private InputAction m_run;
    private InputAction m_interact;

    public Vector2 currentMovementInput { get; private set; }

    private void Awake() {
        m_playerInputAction = new PlayerInputActionAsset();
        m_move = m_playerInputAction.Gameplay.Move;
        m_run = m_playerInputAction.Gameplay.Run;
        m_interact = m_playerInputAction.Gameplay.Interact;
    }

    private void OnEnable() {
        
        m_move.Enable();
        m_move.performed += OnMove;
        m_move.canceled += OnMove;

        m_run.Enable();
        m_run.started += OnRun;
        m_run.canceled += OnRun;

        m_interact.Enable();
        m_interact.started += OnInteract;

    }

    private void OnDisable() {
        
        m_move.Disable();
        m_move.performed -= OnMove;
        m_move.canceled -= OnMove;

        m_run.Disable();
        m_run.started -= OnRun;
        m_run.canceled -= OnRun;

        m_interact.Disable();
        m_interact.started -= OnInteract;

    }

    private void OnMove(InputAction.CallbackContext context) {
        currentMovementInput = context.ReadValue<Vector2>();
    }

    private void OnRun(InputAction.CallbackContext context) {
        m_onPlayerRunCallback?.Invoke(context.ReadValueAsButton());
    }

    private void OnInteract(InputAction.CallbackContext context) {
        m_onPlayerInteractCallback.Invoke();
    }

    public void DisableMovementInput() {
        m_run.Disable();
        m_move.Disable();
        m_interact.Disable();
    }

    public void EnableMovementInput() {
        m_move.Enable();
        m_run.Enable();
        m_interact.Enable();
    }

}
