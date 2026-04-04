using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public Transform player;
    public float distanceToDestroy = 1.5f;
    public float Speed = 5f;
    public float ActiveRadius = 10f;


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
    void OnDrawGizmos()
    {
        if (player == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, ActiveRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, player.position);

        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.cyan;

            Vector3[] corners = agent.path.corners;

            for (int i = 0; i < corners.Length - 1; i++)
            {
                Gizmos.DrawLine(corners[i], corners[i + 1]);
            }
        }
    }
}
        