using UnityEngine;

public class MainMenuManager : MenuManager
{

    [SerializeField] private AudioClip m_clickSound;
    [SerializeField] private TimelineManager m_timelineManager;

    private CanvasGroup m_canvasGroup;

    private void Awake() {
        m_canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public void Play() {
        
        if( m_timelineManager != null ) {
            AudioManager.instance.PlaySFX(m_clickSound);
            m_canvasGroup.interactable = false;
            m_timelineManager.PlayCutscene("Game Started");

        } else {
            Debug.LogError("Timeline Manager Reference Is Missing");
        
        }

    }

}
