
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AiTarget : MonoBehaviour
{
    [Header("Chase Settings")] [SerializeField]
    private Transform player;

    [SerializeField] private float chaseRadius = 10f;
    [SerializeField] private float greetingRange = 2f;

    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private string conversationFlag = "HadConversationWith_Mike";

    private NavMeshAgent m_Agent;
    private Animator m_Animator;
    private Vector3 m_StartPosition;
    private Vector3 target;
    private bool hadConversation = false;
    [SerializeField] private Transform destinationPoint;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_StartPosition = transform.position;
        target = destinationPoint != null ? destinationPoint.position : m_StartPosition;
        if (GameState.Instance != null) GameState.Instance.OnFlagChanged += HandleFlagChanged;

        // safety: ensure audio source exists if clip provided
        if (audioSource == null && walkSound != null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = true;
        }
    }

    void OnDestroy()
    {
        if (GameState.Instance != null) GameState.Instance.OnFlagChanged -= HandleFlagChanged;
    }

    private void HandleFlagChanged(string key, bool value)
    {
        if (key == conversationFlag && value)
            hadConversation = true;
    }

    void Update()
    {
        if (player == null || m_Agent == null) return;

        if (hadConversation)
        {
            GoToDestination(target);
            return;
        }

        // use sqrMagnitude for performance
        float sqrDistToPlayer = (player.position - transform.position).sqrMagnitude;
        float chaseRadiusSqr = chaseRadius * chaseRadius;
        float greetingRangeSqr = greetingRange * greetingRange;

        if (sqrDistToPlayer <= chaseRadiusSqr)
        {
            m_Animator?.SetBool("Idle", false);
            m_Agent.isStopped = false;
            m_Agent.SetDestination(player.position);
            EnsureFootstepsPlaying();

            if (sqrDistToPlayer <= greetingRangeSqr)
            {
                m_Agent.isStopped = true;
                m_Animator?.SetBool("Greeting", true);
                m_Animator?.SetBool("Idle", true);
                StopFootsteps();
            }
            else
            {
                m_Animator?.SetBool("Greeting", false);
                m_Animator?.SetBool("Idle", false);
            }
        }
        else
        {
            GoToDestination(m_StartPosition);
        }
    }

    void GoToDestination(Vector3 position)
    {
        if (m_Agent == null) return;

        // Only set destination when it's meaningfully different
        if ((m_Agent.destination - position).sqrMagnitude > 0.01f)
        {
            m_Animator?.SetBool("Greeting", false);
            m_Animator?.SetBool("Idle", false);
            m_Agent.isStopped = false;
            m_Agent.SetDestination(position);
            EnsureFootstepsPlaying();
        }

        // Arrival detection using remainingDistance/stoppingDistance
        if (!m_Agent.pathPending)
        {
            if (m_Agent.remainingDistance <= Mathf.Max(1f, m_Agent.stoppingDistance) || !m_Agent.hasPath)
            {
                m_Animator?.SetBool("Idle", true);
                m_Agent.isStopped = true;
                StopFootsteps();
            }
        }
    }

    private void EnsureFootstepsPlaying()
    {
        if (audioSource == null || walkSound == null) return;

        // assign clip only when different to avoid restarting playback
        if (audioSource.clip != walkSound)
            audioSource.clip = walkSound;

        audioSource.loop = true;
        if (!audioSource.isPlaying)
            audioSource.Play();
    }

    private void StopFootsteps()
    {
        if (audioSource == null) return;

        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}