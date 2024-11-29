using UnityEngine;
using UnityEngine.AI;

public class RatBehavior : MonoBehaviour
{
    public Transform cheeseTarget; // Asigna el queso en el inspector
    public float speed = 3.5f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Verifica si el NavMeshAgent está presente
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent no encontrado en el prefab de la rata.");
            return;
        }

        agent.speed = speed;

        // Verifica si el queso está asignado
        if (cheeseTarget == null)
        {
            Debug.LogError("El queso no está asignado en el inspector.");
            return;
        }

        agent.SetDestination(cheeseTarget.position);

        // Debug para verificar que la rata tiene un destino válido
        Debug.Log("La rata se mueve hacia el queso en la posición: " + cheeseTarget.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cheese"))
        {
            GameManage.Instance.LoseLife();
            Destroy(gameObject);
        }
        else if (other.CompareTag("SlowTrap"))
        {
            agent.speed /= 2; // Reduce la velocidad
        }
        else if (other.CompareTag("CageTrap"))
        {
            Destroy(gameObject); // Rata atrapada
        }
    }
} 
