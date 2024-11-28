using UnityEngine;

public class FootstepAudioEffect : MonoBehaviour
{
    [Header("Sound Effect Clips")]
    [SerializeField] private AudioClip[] m_footstepWalkSFX;
    [SerializeField] private AudioClip[] m_footstepWalkTileSFX;
    [SerializeField] private AudioClip[] m_footstepRunSFX;
    [SerializeField] private AudioClip[] m_footsetpRunTileSFX;

    public void PlayWakSFX() {

        Vector3 origin = transform.position;

        LayerMask floorLayerMask = LayerMask.GetMask("Ground");
        if( Physics.CheckSphere(origin, 0.15f, floorLayerMask) ) {
            AudioClip clip = m_footstepWalkSFX[Random.Range(0, m_footstepWalkSFX.Length)];
            AudioManager.instance.PlaySFX(clip, 0.5f);
            return;

        }

        LayerMask worldLayerMask = LayerMask.GetMask("World");
        if( Physics.CheckSphere(origin, 0.15f, worldLayerMask) ) {
            AudioClip clip = m_footstepWalkTileSFX[Random.Range(0, m_footstepWalkTileSFX.Length)];
            AudioManager.instance.PlaySFX(clip, 0.5f);
            return;

        }

    }

    public void PlayRunSFX() {

        Vector3 origin = transform.position;

        LayerMask floorLayerMask = LayerMask.GetMask("Ground");
        if (Physics.CheckSphere(origin, 0.15f, floorLayerMask)) {
            AudioClip clip = m_footstepRunSFX[Random.Range(0, m_footstepRunSFX.Length)];
            AudioManager.instance.PlaySFX(clip, 0.5f);
            return; 

        }

        LayerMask worldLayerMask = LayerMask.GetMask("World");
        if (Physics.CheckSphere(origin, 0.15f, worldLayerMask)) {
            AudioClip clip = m_footsetpRunTileSFX[Random.Range(0, m_footsetpRunTileSFX.Length)];
            AudioManager.instance.PlaySFX(clip, 0.5f);
            return;

        }

    }

}
