using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            // Efecto manejado en el script de la rata
        }
    }
}
