using UnityEngine;
using UnityEngine.AI;

public class RatonController : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public Transform target; // Punto final al que el ratón se moverá

    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();

        // Mueve el ratón hacia el objetivo inicial en el NavMesh
        if (target != null)
        {
            navAgent.SetDestination(target.position);
        }
    }

    private void Update()
    {
        // Verifica si el ratón está fuera del NavMesh
        if (!navAgent.isOnNavMesh)
        {
            Debug.LogWarning("El ratón ha salido del NavMesh, reposicionándolo.");
            RepositionOnNavMesh();
        }
    }

    private void RepositionOnNavMesh()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas))
        {
            // Reposiciona al ratón dentro del NavMesh
            transform.position = hit.position;
            navAgent.Warp(hit.position);
        }
        else
        {
            Debug.LogError("No se encontró una posición válida en el NavMesh.");
        }
    }
}
