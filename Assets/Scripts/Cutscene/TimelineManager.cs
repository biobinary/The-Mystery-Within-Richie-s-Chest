using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[Serializable]
public class PlayableDictionary
{
    public string title;
    public PlayableAsset asset;
}

[RequireComponent(typeof(PlayableDirector))]
public class TimelineManager : MonoBehaviour {

    [SerializeField] List<PlayableDictionary> m_playableDictionaries;

    private PlayableDirector m_cutsceneDirector;
    private Dictionary<string, PlayableAsset> m_dictionary;

    private void Awake() {

        m_cutsceneDirector = GetComponent<PlayableDirector>();
        
        m_dictionary = new Dictionary<string, PlayableAsset>();
        foreach(var pair in m_playableDictionaries) {
            m_dictionary[pair.title] = pair.asset;
        }

    }

    public enum DIRECTOR_STATE { 
        PLAYED,
        PAUSED,
        STOPPED,
        ALL
    }

    public void PlayCutscene(string title) {
        PlayableAsset currentAsset = m_dictionary.GetValueOrDefault(title, null);
        if (currentAsset != null) { 
            m_cutsceneDirector.playableAsset = currentAsset;
            m_cutsceneDirector.Play();
        }
    }

    public void ConnectToEvent(Action<PlayableDirector> callback, DIRECTOR_STATE state_type) {

        switch (state_type) {
            
            case DIRECTOR_STATE.PLAYED:
                m_cutsceneDirector.played += callback;
                break;
            
            case DIRECTOR_STATE.PAUSED:
                m_cutsceneDirector.paused += callback;
                break;
            
            case DIRECTOR_STATE.STOPPED:
                m_cutsceneDirector.stopped += callback;
                break;

            default:
                m_cutsceneDirector.played += callback;
                m_cutsceneDirector.paused += callback;
                m_cutsceneDirector.stopped += callback;
                break;

        }

    }

    public void DisconnectFromEvent(Action<PlayableDirector> callback, DIRECTOR_STATE state_type) {

        switch (state_type) {

            case DIRECTOR_STATE.PLAYED:
                m_cutsceneDirector.played -= callback;
                break;

            case DIRECTOR_STATE.PAUSED:
                m_cutsceneDirector.paused -= callback;
                break;

            case DIRECTOR_STATE.STOPPED:
                m_cutsceneDirector.stopped -= callback;
                break;

            default:
                m_cutsceneDirector.played -= callback;
                m_cutsceneDirector.paused -= callback;
                m_cutsceneDirector.stopped -= callback;
                break;

        }

    }

}
