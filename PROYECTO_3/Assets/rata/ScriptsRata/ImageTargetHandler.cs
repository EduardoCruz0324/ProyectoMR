using UnityEngine;
using UnityEngine.AI;
using Vuforia;

public class ImageTargetHandler : MonoBehaviour
{
    public GameObject ratPrefab;        // Prefab del ratón
    public GameObject cheesePrefab;    // Prefab del queso
    public GameObject ratImageTarget;  // Image Target del ratón
    public GameObject cheeseImageTarget; // Image Target del queso

    private GameObject spawnedRat;    // Referencia al ratón instanciado
    private GameObject spawnedCheese; // Referencia al queso instanciado
    private bool cheeseSpawned = false; // Asegura que el queso inicial solo se instancie una vez

    void Update()
    {
        // Verifica si el Image Target del ratón está visible y no se ha instanciado un ratón
        if (ratImageTarget.GetComponent<ObserverBehaviour>().TargetStatus.Status == Status.TRACKED && spawnedRat == null)
        {
            // Instancia el ratón sobre el target
            spawnedRat = Instantiate(ratPrefab, ratImageTarget.transform.position, Quaternion.identity);
            Debug.Log("Ratón instanciado en el Image Target.");

            // Configura al ratón para buscar el queso
            ConfigureRat(spawnedRat);
        }

        // Verifica si el Image Target del queso está visible y aún no se ha instanciado el queso
        if (cheeseImageTarget.GetComponent<ObserverBehaviour>().TargetStatus.Status == Status.TRACKED && !cheeseSpawned)
        {
            // Instancia el queso sobre el target
            spawnedCheese = Instantiate(cheesePrefab, cheeseImageTarget.transform.position, Quaternion.identity);
            cheeseSpawned = true; // Marca que el queso inicial ya ha sido instanciado
            Debug.Log("Queso instanciado en el Image Target.");
        }
    }

    private void ConfigureRat(GameObject rat)
    {
        if (spawnedCheese == null)
        {
            Debug.LogWarning("No se ha encontrado el queso. El ratón no tiene destino.");
            return;
        }

        // Asegúrate de que el ratón tenga un NavMeshAgent para moverse
        NavMeshAgent agent = rat.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("El ratón no tiene un componente NavMeshAgent. No podrá moverse.");
            return;
        }

        // Configura el destino del ratón hacia el queso
        agent.SetDestination(spawnedCheese.transform.position);

        // Revisa constantemente si llegó al queso
        StartCoroutine(CheckIfRatReachedCheese(rat, agent));
    }

    private System.Collections.IEnumerator CheckIfRatReachedCheese(GameObject rat, NavMeshAgent agent)
    {
        while (rat != null && agent != null)
        {
            // Si el ratón está cerca del queso, destrúyelo
            if (spawnedCheese != null && Vector3.Distance(rat.transform.position, spawnedCheese.transform.position) <= agent.stoppingDistance)
            {
                Debug.Log("El ratón ha alcanzado el queso.");
                Destroy(rat); // Destruye el ratón
                break;
            }
            yield return null;
        }
    }

    public void CreateNewCheese(Vector3 position)
    {
        if (spawnedCheese != null)
        {
            Destroy(spawnedCheese); // Elimina el queso existente antes de crear uno nuevo
        }

        spawnedCheese = Instantiate(cheesePrefab, position, Quaternion.identity);
        Debug.Log("Nuevo queso instanciado.");
    }
}