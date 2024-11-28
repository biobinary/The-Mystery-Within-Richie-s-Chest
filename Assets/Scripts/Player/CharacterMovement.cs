using UnityEngine;
using UnityEngine.Playables;

public class CharacterMovement : MonoBehaviour {

    // Component Initialization
    private CharacterController m_characterController;

    [Header("Player Input")]
    [SerializeField] private CharacterInputSystem m_characterInputSystem;

    [Header("Player Movement")]
    [SerializeField] private float m_gravityValue = -9.8f;
    [SerializeField] private float m_walkSpeed = 2.0f;
    [SerializeField] private float m_runSpeed = 5.0f;
    [SerializeField] private float m_rotationPerFrame = 5.0f;

    [Header("Player Animation")]
    [SerializeField] private Animator m_animator;

    [Header("Other")]
    [SerializeField] private TimelineManager m_timelineManager;
    [SerializeField] private ParticleSystem m_particleSystem;

    private Vector3 m_velocity; // Handle The Character Vertical Movement
    private bool m_groundedPlayer;

    private bool m_isWalking = true;
    private bool m_isRunning = false;

    private void Awake() {
        m_characterController = GetComponent<CharacterController>();
    }

    private void Start() {
        
        if (m_timelineManager != null) {
            m_timelineManager.ConnectToEvent(OnCutscenePlayed, TimelineManager.DIRECTOR_STATE.PLAYED);
            m_timelineManager.ConnectToEvent(OnCutsceneStopped, TimelineManager.DIRECTOR_STATE.STOPPED);
        }

        m_characterInputSystem.DisableMovementInput();

    }

    private void Move() {

        m_groundedPlayer = m_characterController.isGrounded;
        if (m_groundedPlayer && m_velocity.y < 0) {
            m_velocity.y = 0f;
        }

        Vector3 currentMovementDir = Vector3.zero;
        currentMovementDir.x = -m_characterInputSystem.currentMovementInput.x;
        currentMovementDir.z = -m_characterInputSystem.currentMovementInput.y;

        if (currentMovementDir != Vector3.zero) {

            m_isWalking = true;

            float movementSpeed = !m_isRunning ? m_walkSpeed : m_runSpeed;
            Vector3 targetMovement = currentMovementDir * movementSpeed; 
            m_characterController.Move(targetMovement * Time.deltaTime);

            HandleRotation( targetMovement );

            if (!m_particleSystem.isPlaying && m_groundedPlayer)
                m_particleSystem.Play();

            else if (m_particleSystem.isPlaying && !m_groundedPlayer)
                m_particleSystem.Stop();

        } else {

            m_isWalking = false;

            Vector3 targetMovement = Vector3.zero;
            m_characterController.Move(targetMovement);

            if (m_particleSystem.isPlaying)
                m_particleSystem.Stop();

        }

        m_velocity.y += m_gravityValue * Time.deltaTime;
        m_characterController.Move(m_velocity * Time.deltaTime);

    }

    private void HandleRotation(Vector3 lookAtTarget) {

        Vector3 positionToLookAt;

        positionToLookAt.x = lookAtTarget.x;
        positionToLookAt.y = 0.0f;
        positionToLookAt.z = lookAtTarget.z;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);

        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, m_rotationPerFrame * Time.deltaTime);

    }

    private void HandleAnimation() {
        
        bool animWalk = m_animator.GetBool("isWalking");
        bool animRun = m_animator.GetBool("isRunning");

        if(m_isWalking && !animWalk) {
            m_animator.SetBool("isWalking", true);

        } else if( !m_isWalking && animWalk ) {
            m_animator.SetBool("isWalking", false);
        }

        if((m_isWalking && m_isRunning) && !animRun) {
            m_animator.SetBool("isRunning", true);
        
        } else if((!m_isWalking || !m_isRunning) && animRun) {
            m_animator.SetBool("isRunning", false);

        }

    }

    private void Update() {
        Move();
        HandleAnimation();
    }

    public void OnPlayerRun(bool isButtonPressed) {
        m_isRunning = isButtonPressed;
    }

    private void OnCutscenePlayed(PlayableDirector director) {
        m_characterInputSystem.DisableMovementInput();
    }

    private void OnCutsceneStopped(PlayableDirector director) {
        m_characterInputSystem.EnableMovementInput();
    }

}
