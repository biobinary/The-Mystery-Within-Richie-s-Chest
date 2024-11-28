using UnityEngine;
using DigitalRuby.Tween;
using TMPro;

public class StatusLabelManager : MonoBehaviour {

    public static StatusLabelManager instance { get; private set; }

    [Header("Text Attributes")]
    [SerializeField] private TextMeshProUGUI m_textElement;
    private CanvasGroup m_canvasGroup;

    [SerializeField] private float m_displayTime = 2.0f;
    private float m_currentTime = 0.0f;

    private FloatTween m_currentTween = null;

    private void Awake() {
        if (instance != null && instance != this) {
            Debug.LogError("Multiple StatusLabelManager instances found in the scene. Please ensure only one exists.");
            Destroy(gameObject);
            return;
        }

        instance = this;

        m_canvasGroup = m_textElement.GetComponent<CanvasGroup>();
        if (m_canvasGroup == null) {
            m_canvasGroup = m_textElement.gameObject.AddComponent<CanvasGroup>();
        }

        m_textElement.text = "";
        m_textElement.gameObject.SetActive(false);
    }

    private void Update() {
        if (m_currentTime > 0.0f) {
            m_currentTime -= Time.deltaTime;
            if (m_currentTime <= 0.0f) {
                ClearText();
            }
        }
    }

    private void ClearText() {
        System.Action<ITween<float>> onFadeOut = (a) => {
            m_canvasGroup.alpha = a.CurrentValue;
        };

        System.Action<ITween<float>> onFadeOutComplete = (a) => {
            m_currentTween = null;
            m_textElement.text = "";
            m_textElement.gameObject.SetActive(false);
        };

        m_currentTween = TweenFactory.Tween(
            "StatusLabelFadeOut",
            1.0f,
            0.0f,
            0.25f,
            TweenScaleFunctions.SineEaseInOut,
            onFadeOut,
            onFadeOutComplete
        );
    }

    public void ShowText(string text) {
        if (m_currentTween != null) {
            TweenFactory.RemoveTween(m_currentTween, TweenStopBehavior.DoNotModify);
        }

        m_canvasGroup.alpha = 1.0f;
        m_textElement.text = text;
        m_textElement.gameObject.SetActive(true);
        m_currentTime = m_displayTime;
    }
}
