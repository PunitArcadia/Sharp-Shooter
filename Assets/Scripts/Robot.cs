using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] GameObject target;

    NavMeshAgent agent;
    EnemyHealth eh;
    PlayerHealth ph;
    const string PLAYER_TAG = "Player";

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        eh = GetComponent<EnemyHealth>();
    }

    private void Start()
    {
        ph = FindFirstObjectByType<PlayerHealth>();
        if (ph != null)
        {
            target = ph.gameObject;
        }
    }

    private void Update()
    {
        if (ph == null || ph.IsDead || target == null)
        {
            agent.isStopped = true;
            return;
        }
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(PLAYER_TAG)) return;
        if (ph == null || ph.IsDead) return;
        ph.TakeDamage(4);
        eh.SelfDestroy();
    }
}
