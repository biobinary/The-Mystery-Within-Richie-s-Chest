using UnityEngine;
using TMPro;
using DigitalRuby.Tween;

public class NPCInteractiveText : FloatingText
{
    
    private TextMeshPro m_text;
    private Animator m_animator;

    private Vector3 m_originalScale = new Vector3(-0.15f, 0.15f, 0.15f);
    private Vector3Tween m_currentScaleTween = null;

    protected override void Awake() { 
        base.Awake();
        m_text = GetComponent<TextMeshPro>();
        m_animator = GetComponent<Animator>();
    }

    private void Start() {
        transform.localScale = Vector3.zero;
    }

    public void ShowText() {
        
        gameObject.SetActive(true);

        if (m_text.text != "[E] Interact")
            m_text.text = "[E] Interact";

        if ( m_currentScaleTween != null )
            TweenFactory.RemoveTween(m_currentScaleTween, TweenStopBehavior.DoNotModify);

        Vector3 currentScale = transform.localScale;
        Vector3 targetScale = m_originalScale;

        System.Action<ITween<Vector3>> updateScale = (s) => {
            transform.localScale = s.CurrentValue;
        };

        System.Action<ITween<Vector3>> onUpdateScaleComplete = (s) => {
            m_currentScaleTween = null;
        };

        m_currentScaleTween = TweenFactory.Tween(
            "TextPopUp",
            currentScale,
            targetScale,
            0.25f,
            TweenScaleFunctions.CubicEaseInOut,
            updateScale,
            onUpdateScaleComplete
        );

    }

    public void HideText() {

        if(m_text.text != "[E] Interact")
            m_text.text = "[E] Interact";

        if (m_currentScaleTween != null)
            TweenFactory.RemoveTween(m_currentScaleTween, TweenStopBehavior.DoNotModify);

        Vector3 currentScale = transform.localScale;
        Vector3 targetScale = Vector3.zero;

        System.Action<ITween<Vector3>> updateScale = (s) => {
            transform.localScale = s.CurrentValue;
        };

        System.Action<ITween<Vector3>> onUpdateScaleComplete = (s) => {
            m_currentScaleTween = null;
            gameObject.SetActive(false);
        };

        m_currentScaleTween = TweenFactory.Tween(
            "TextPopUp",
            currentScale,
            targetScale,
            0.25f,
            TweenScaleFunctions.CubicEaseInOut,
            updateScale,
            onUpdateScaleComplete
        );

    }

}
