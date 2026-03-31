using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;          
    public float distanceToDestroy = 1.5f;
    public float Speed = 5f;
 

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null || agent == null) return;

       
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(player.position);
        }

        
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= distanceToDestroy)
        {
            Destroy(gameObject);
            Debug.Log("Enemigo Destruido");

        }
    }
}