using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class NPCInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private ChestKey.TYPE m_keyTypeToGive = ChestKey.TYPE.A;
    [SerializeField] private ChestKey.TYPE m_requiredKey = ChestKey.TYPE.F;

    [Header("NPC Custom Cutscene")]
    [SerializeField] private TimelineManager m_mainTimelineManager;
    [SerializeField] private TextMeshPro m_dialogText;

    private NPCInteractiveText m_npcText;
    private bool m_playerHasInteract = false;

    private void Awake() {
        m_npcText = GetComponentInChildren<NPCInteractiveText>();
    }

    private void Start() {
        m_dialogText.enabled = false;
    }

    public void Interact(GameObject other) {

        if (m_mainTimelineManager != null) {
            m_mainTimelineManager.ConnectToEvent(OnCutscenePlayed, TimelineManager.DIRECTOR_STATE.PLAYED);
            m_mainTimelineManager.ConnectToEvent(OnCutsceneStopped, TimelineManager.DIRECTOR_STATE.STOPPED);

        }

        if (!m_playerHasInteract) {
            m_playerHasInteract = true;
            m_mainTimelineManager.PlayCutscene("Intro Monologue");

        } else {

            if( PlayerKeyInventory.HasKey(m_keyTypeToGive) ) {
                m_mainTimelineManager.PlayCutscene("Key Not Found");
            
            } else if( PlayerKeyInventory.HasKey(m_requiredKey) ) {
                m_mainTimelineManager.PlayCutscene("Key Found");

            } else {
                m_mainTimelineManager.PlayCutscene("Incorrect Key");

            }

        }
    
    }

    public void OnBeginFacing() {
        m_npcText.ShowText();
    }

    public void OnEndFacing() {
        m_npcText.HideText();
    }

    private void OnCutscenePlayed(PlayableDirector director) {
        m_dialogText.enabled = true;
        m_npcText.HideText();
    }

    private void OnCutsceneStopped(PlayableDirector director) {

        if (m_mainTimelineManager != null) {
            m_mainTimelineManager.DisconnectFromEvent(OnCutscenePlayed, TimelineManager.DIRECTOR_STATE.PLAYED);
            m_mainTimelineManager.DisconnectFromEvent(OnCutsceneStopped, TimelineManager.DIRECTOR_STATE.STOPPED);
        }

        m_dialogText.enabled = false;
        m_npcText.transform.localScale = Vector3.zero;
        m_npcText.ShowText();
    }

    public void GiveThePlayerKey() {
        PlayerKeyInventory.AddKey(m_keyTypeToGive);
    }

}
