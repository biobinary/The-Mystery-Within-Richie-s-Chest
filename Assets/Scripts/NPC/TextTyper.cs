using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Events;

public class TextTyper : MonoBehaviour
{

    private TMP_Text m_textMeshPro;

    [SerializeField] private float m_typingSpeed = 0.05f;
    [SerializeField] private UnityEvent m_onTypingAnimationStarted;
    [SerializeField] private UnityEvent m_onTypingAnimationComplete;

    private void Awake() {
        m_textMeshPro = GetComponent<TMP_Text>();
    }

    private IEnumerator TypeText(string fullText) {

        m_textMeshPro.text = "";

        foreach(char c in fullText) {
            m_textMeshPro.text += c;
            yield return new WaitForSeconds(m_typingSpeed);
        }

        m_onTypingAnimationComplete.Invoke();

    }

    public void StartTyping(string fullText) {
        m_onTypingAnimationStarted.Invoke();
        StartCoroutine(TypeText(fullText));
    }


}
