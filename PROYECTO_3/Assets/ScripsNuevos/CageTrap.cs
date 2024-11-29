using UnityEngine;

public class CageTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            // Efecto manejado en el script de la rata
        }
    }
}
