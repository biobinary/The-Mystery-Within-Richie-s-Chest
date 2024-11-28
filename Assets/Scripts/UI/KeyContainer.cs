using UnityEngine;

public class KeyContainer : MonoBehaviour
{

    [SerializeField] private KeyMaterialSO m_materials;
    [SerializeField] private float m_rotateSpeed = 20.0f;
    
    private GameObject m_physicalKeyUI;

    private void Awake() {
        m_physicalKeyUI = transform.GetChild(0).gameObject;
    }

    private void Update() {
        if (!m_physicalKeyUI.activeSelf) return;
        m_physicalKeyUI.transform.Rotate(new Vector3(m_rotateSpeed * Time.deltaTime, 0.0f, 0.0f));
    }

    private void Start() {
        m_physicalKeyUI.SetActive(false);
        PlayerKeyInventory.onNewKeyAdded += OnNewKeyAdded;
        PlayerKeyInventory.onRemoveOldKey += OnRemoveOldKey;
    }

    private void OnNewKeyAdded(ChestKey.TYPE type) {
        m_physicalKeyUI.GetComponent<MeshRenderer>().material = m_materials.GetMaterialByKeyType(type);
        m_physicalKeyUI.SetActive(true);
    }

    private void OnRemoveOldKey(ChestKey.TYPE type) {
        m_physicalKeyUI.SetActive(false);
    }



}
