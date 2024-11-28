using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EndMenuManager : MenuManager
{

    [SerializeField] private AudioClip m_clickSound;
    [SerializeField] Button m_selectedButton;

    public void RestartGame() {
        AudioManager.instance.PlaySFX(m_clickSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ActivateInteractable() {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = true;
        EventSystem.current.SetSelectedGameObject(m_selectedButton.gameObject);
    }

}
