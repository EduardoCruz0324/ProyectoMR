using UnityEngine;

public class CheeseSpawner : MonoBehaviour
{
    public GameObject cheesePrefab; // Prefab del queso
    private GameObject spawnedCheese; // Referencia al queso instanciado

    // Llamado cuando el ImageTarget es encontrado (visible)
    public void OnTrackingFound()
    {
        // Solo instancia el queso si no se ha instanciado previamente
        if (spawnedCheese == null)
        {
            spawnedCheese = Instantiate(cheesePrefab, transform.position, Quaternion.identity);
        }
    }

    // Llamado cuando el ImageTarget es perdido (invisible)
    public void OnTrackingLost()
    {
        // Destruye el queso solo si está instanciado
        if (spawnedCheese != null)
        {
            Destroy(spawnedCheese);
        }
    }

    // Método para obtener el queso instanciado (si lo necesitas en otro lugar)
    public GameObject GetCheese()
    {
        return spawnedCheese;
    }
}

