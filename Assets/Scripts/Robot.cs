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
    }

    private void Update()
    {
        agent.SetDestination(target.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG)) 
        {
            ph.TakeDamage(4);
            eh.SelfDestroy();
        }
    }
}
