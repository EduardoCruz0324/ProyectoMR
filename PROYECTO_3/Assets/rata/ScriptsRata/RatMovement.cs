using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RatMovement : MonoBehaviour
{
    public NavMeshAgent agent; // Componente NavMeshAgent para la rata
    public Transform cheeseTarget; // Transform del queso
    public float moveRange = 10f; // Rango de movimiento aleatorio
    public float detectionRange = 15f; // Rango de detección del queso
    public float updateInterval = 5f; // Intervalo para cambiar de destino aleatorio

    private bool isChasingCheese = false; // Indica si está persiguiendo el queso
    private float nextUpdateTime;

    void Start()
    {
        // No volvemos a declarar 'hit', solo lo usamos directamente
        NavMeshHit hit;

        if (!NavMesh.SamplePosition(transform.position, out hit, 2.0f, NavMesh.AllAreas))
        {
            // Si no se encuentra una posición válida, mueve la rata a una posición predeterminada
            Debug.LogWarning("Posición inicial no válida. Moviendo a una posición predeterminada.");
            transform.position = new Vector3(0, 0, 0); // Ajusta a una posición válida en tu escena
        }
        else
        {
            transform.position = hit.position; // Ajusta la posición al NavMesh
        }
    }
    void Update()
    {
        // Verifica si el agente está activo y en el NavMesh
        if (!agent.isActiveAndEnabled || !agent.isOnNavMesh)
        {
            Debug.LogWarning("El NavMeshAgent no está configurado correctamente.");
            return;
        }

        if (isChasingCheese && cheeseTarget != null)
        {
            // Persigue el queso si está dentro del rango
            agent.SetDestination(cheeseTarget.position);

            // Verifica si llegó al queso
            if (Vector3.Distance(transform.position, cheeseTarget.position) <= agent.stoppingDistance)
            {
                OnReachCheese();
            }
        }
        else
        {
            // Movimiento aleatorio si no está persiguiendo el queso
            if (Time.time >= nextUpdateTime || (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance))
            {
                SetRandomDestination();
                nextUpdateTime = Time.time + updateInterval;
            }

            // Detecta el queso si está cerca
            if (cheeseTarget != null && Vector3.Distance(transform.position, cheeseTarget.position) <= detectionRange)
            {
                isChasingCheese = true;
            }
        }
    }

    void SetRandomDestination()
    {
        // Genera un destino aleatorio en el NavMesh
        Vector3 randomDirection = Random.insideUnitSphere * moveRange;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, moveRange, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    void OnReachCheese()
    {
        Debug.Log("La rata alcanzó el queso.");
        GameManager.Instance?.RatReachedCheese(); // Notifica al GameManager
        Destroy(gameObject); // Destruye la rata
    }
    
}