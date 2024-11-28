using System.Collections;
using UnityEngine;

public class ChestKey {
    public enum TYPE {
        A, B, C, D, E, F
    }
}

public class PhysicalChestKey : MonoBehaviour {

    [SerializeField] private float m_maxMoveDuration = 1.0f;
    [SerializeField] private AudioClip m_keySFX;

    private ChestKey.TYPE m_physicalKeyType;
    private GameObject m_target;
    private MeshRenderer m_renderer;

    private Vector3 m_offset = new Vector3(0.0f, 0.4f, 0.0f);
    private Vector3 m_velocity = Vector3.zero;

    private void Awake() {
        m_renderer = GetComponentInChildren<MeshRenderer>();
    }

    private IEnumerator SmoothMoveToPlayer() {

        float elapsedTime = 0.0f;

        while (elapsedTime < m_maxMoveDuration) { 
            
            elapsedTime += Time.deltaTime;
            
            Vector3 currentTargetPosition = m_target.transform.position + m_offset;
            transform.position = Vector3.Lerp(transform.position, currentTargetPosition, elapsedTime / m_maxMoveDuration);

            if (Vector3.Distance(transform.position, currentTargetPosition) < 0.5f) {
                transform.position = m_target.transform.position;
                KeyHasGivenToPlayer();
                yield break;
            }
            
            yield return null;
        
        }

        transform.position = m_target.transform.position;
        KeyHasGivenToPlayer();

    }

    private void KeyHasGivenToPlayer() {
        AudioManager.instance.PlaySFX(m_keySFX);
        gameObject.SetActive(false);
        PlayerKeyInventory.AddKey(m_physicalKeyType);
    }

    public void InitializePhysicalKey(ChestKey.TYPE type, Material material) {
        m_physicalKeyType = type;
        m_renderer.material = material;
    }

    public void GoToPlayer(GameObject other) {
        m_target = other;
        StartCoroutine(SmoothMoveToPlayer());
    }

}
