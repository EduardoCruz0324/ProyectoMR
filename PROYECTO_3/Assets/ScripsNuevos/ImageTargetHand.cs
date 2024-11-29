using UnityEngine;

public class ImageTargetHand : MonoBehaviour
{
    public GameObject prefabToSpawn; // Asigna el prefab correspondiente (rata, queso, cañón o trampa)

    private GameObject spawnedObject;

    public void OnTrackingFound()
    {
        if (spawnedObject == null)
        {
            spawnedObject = Instantiate(prefabToSpawn, transform.position, transform.rotation);
        }
    }

    public void OnTrackingLost()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
    }
} 
