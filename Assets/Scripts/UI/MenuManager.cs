using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    
    [SerializeField] private AudioClip m_navigationSound;

    private GameObject m_lastSelected;

    private void Start() {
        m_lastSelected = EventSystem.current.currentSelectedGameObject;
    }

    private void Update() {

        GameObject currentSelected = EventSystem.current.currentSelectedGameObject;

        if (currentSelected != null && currentSelected != m_lastSelected) {
            AudioManager.instance.PlaySFX(m_navigationSound, 0.4f);
            m_lastSelected = currentSelected;
        }

    }

    public virtual void OnQuit() {
        
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    
    }

}
