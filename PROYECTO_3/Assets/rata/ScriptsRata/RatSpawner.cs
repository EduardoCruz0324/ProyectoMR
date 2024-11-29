using UnityEngine;

public class RatSpawner : MonoBehaviour
{
    public GameObject ratPrefab;       // Prefab de la rata
    public int maxRats = 5;            // Cantidad máxima de ratas a generar
    public float spawnInterval = 1.0f; // Intervalo de tiempo entre cada generación

    private Transform cheeseTarget;    // Referencia al objetivo "queso"
    private float spawnTimer = 0.0f;   // Temporizador para control de intervalos
    private int ratsSpawned = 0;       // Contador de ratas generadas actualmente

    void Start()
    {
        // Busca el objetivo "queso"
        FindCheeseTarget();
    }

    void Update()
    {
        // Si no hay objetivo "queso", sigue buscando
        if (cheeseTarget == null)
        {
            FindCheeseTarget();
            return;
        }

        // Detiene la generación si ya se alcanzó el límite máximo de ratas
        if (ratsSpawned >= maxRats)
        {
            return;
        }

        // Controla el temporizador y genera ratas a intervalos
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnRat();
            spawnTimer = 0.0f; // Reinicia el temporizador
        }
    }

    void FindCheeseTarget()
    {
        // Busca el objeto "queso" en la escena usando la etiqueta "queso"
        GameObject cheeseObject = GameObject.FindGameObjectWithTag("queso");

        if (cheeseObject != null)
        {
            cheeseTarget = cheeseObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con la etiqueta 'queso'.");
        }
    }

    void SpawnRat()
    {
        GameObject newRat = Instantiate(ratPrefab, transform.position, Quaternion.identity);

        // Asigna el objetivo "queso" al script del ratón
        Rat ratScript = newRat.GetComponent<Rat>();
        if (ratScript != null)
        {
            ratScript.cheeseTarget = cheeseTarget;
        }

        ratsSpawned++;
        Debug.Log("Rata generada. Total ratas: " + ratsSpawned);
    }
}