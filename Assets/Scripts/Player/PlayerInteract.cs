using UnityEngine;

public class PlayerInteract : MonoBehaviour
{

    [Header("Input")]
    [SerializeField] private CharacterInputSystem m_playerInput;

    [Header("Collision Detection")]
    [SerializeField] private Transform m_interactPosition;
    [SerializeField] private float m_interactRange = 0.4f;

    private IInteractable m_currentInteractable = null;

    private void Update() {
        CheckForCollider();
    }

    private void CheckForCollider() {

        Collider[] colliders = new Collider[10];
        int colliderCount = Physics.OverlapSphereNonAlloc(m_interactPosition.position, m_interactRange, colliders);

        bool isInteractableDetected = false;
        IInteractable detectedInteractable = null;

        for (int i = 0; i < colliderCount; i++) {
            
            Collider col = colliders[i];

            if (col.TryGetComponent(out detectedInteractable)) {
                if (IsAngleFacing(col)) {
                    isInteractableDetected = true;
                    break;
                }
            }

        }

        if (isInteractableDetected && detectedInteractable != m_currentInteractable) {

            if (m_currentInteractable != null) {
                m_currentInteractable.OnEndFacing();
            }

            m_currentInteractable = detectedInteractable;
            m_currentInteractable.OnBeginFacing();

        } else if (!isInteractableDetected && m_currentInteractable != null) {

            m_currentInteractable.OnEndFacing();
            m_currentInteractable = null;

        }

    }

    private bool IsAngleFacing(Collider col) {

        Vector2 interactableToPlayerDir = new Vector2(
                transform.position.x - col.transform.position.x,
                transform.position.z - col.transform.position.z);
        interactableToPlayerDir.Normalize();

        Vector2 interactableFacingDir = new Vector2(
                col.transform.forward.x,
                col.transform.forward.z
            );

        return Vector2.Dot(interactableToPlayerDir, interactableFacingDir) > 0.6f;

    }

    public void OnHandleInteract() {
        if (m_currentInteractable == null) return;
        m_currentInteractable.Interact(gameObject);

    }

}
