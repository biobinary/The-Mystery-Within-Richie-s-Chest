using UnityEngine;

public class FloatingText : MonoBehaviour
{

    private Transform m_cameraTransform;
    
    protected virtual void Awake() {
        m_cameraTransform = Camera.main.transform;
    }

    protected virtual void LateUpdate() {
        transform.LookAt(m_cameraTransform);
    }

}
