using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource m_soundEffectSource;

    private void Awake() {

        if ( instance == null ) {
            instance = this;
            DontDestroyOnLoad(gameObject);

        } else if ( instance != null && instance != this ) {
            Destroy( gameObject );

        }

    }

    public void PlaySFX(AudioClip clip, float volumeScale = 1.0f) {
        if (clip == null) return;
        m_soundEffectSource.PlayOneShot(clip, volumeScale);
    }

}
