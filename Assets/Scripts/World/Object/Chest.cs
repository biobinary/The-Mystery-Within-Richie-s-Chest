using System.Collections;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable {

    private NPCInteractiveText m_npcText;
    private Animator m_animator;
    private ParticleSystem m_particleSystem;

    [Header("Chest Key Properties")]
    [SerializeField] private ChestKey.TYPE m_requiredKey = ChestKey.TYPE.A;
    [SerializeField] private ChestKey.TYPE m_keyToGive = ChestKey.TYPE.B;
    [SerializeField] private PhysicalChestKey m_physicalChestKey;
    [SerializeField] private KeyMaterialSO m_keyMaterialSO;

    [Header("Sound Effect CLips")]
    [SerializeField] private AudioClip m_chestOpenSFX;
    [SerializeField] private AudioClip m_chestIncorrectKeySFX;

    private bool m_isOpened = false;
    private GameObject m_targetPlayer = null;

    private void Awake() {
        m_npcText = GetComponentInChildren<NPCInteractiveText>();
        m_animator = GetComponentInChildren<Animator>();
        m_particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start() {
        ParticleSystemRenderer particleSystemRenderer = m_particleSystem.GetComponent<ParticleSystemRenderer>();
        particleSystemRenderer.material = m_keyMaterialSO.GetMaterialByKeyType(m_requiredKey);
        m_physicalChestKey.InitializePhysicalKey(m_keyToGive, m_keyMaterialSO.GetMaterialByKeyType(m_keyToGive));
    }

    private IEnumerator WaitForChestToOpen() {
        
        m_animator.SetBool("isOpened", m_isOpened);

        while(!m_animator.GetCurrentAnimatorStateInfo(0).IsName("ChestOpen")) {
            yield return null;
        }

        while(m_animator.GetCurrentAnimatorStateInfo(0).IsName("ChestOpen") &&
            m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f) {
            yield return null;
        }

        OnOpenIsFinished();

    }

    private void OnOpenIsFinished() {
        m_physicalChestKey.GoToPlayer(m_targetPlayer);
        m_targetPlayer = null;
    }

    public void Interact(GameObject other) {

        if( m_isOpened ) return;
        if (!PlayerKeyInventory.HasKey(m_requiredKey)) {
            StatusLabelManager.instance.ShowText(
                    PlayerKeyInventory.IsHaveKey() ? "Key doesn't fit!" : "Key required!");
            AudioManager.instance.PlaySFX(m_chestIncorrectKeySFX);
            return;
        }

        m_particleSystem.Stop();

        m_isOpened = true;
        m_npcText.HideText();
        m_targetPlayer = other;

        AudioManager.instance.PlaySFX(m_chestOpenSFX, 0.7f);
        PlayerKeyInventory.RemoveKey();

        StartCoroutine(WaitForChestToOpen());
    
    }

    public void OnBeginFacing() {
        if( m_isOpened ) return;
        m_npcText.ShowText();
    }

    public void OnEndFacing() {
        if (m_isOpened) return;
        m_npcText.HideText();
    }

}
