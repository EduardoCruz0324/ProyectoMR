using UnityEngine;
using Vuforia;

public class TargetController : MonoBehaviour
{
    public GameObject rata; // Arrastra tu objeto de la rata aquí en el inspector

    void Start()
    {
        // Asegúrate de que la rata esté desactivada al inicio
        rata.SetActive(false);
    }

    // Método que se ejecuta cuando el Image Target es detectado
    public void OnTrackingFound()
    {
        rata.SetActive(true); // Activa la rata
    }

    // Método que se ejecuta cuando el Image Target deja de ser detectado
    public void OnTrackingLost()
    {
        rata.SetActive(false); // Desactiva la rata
    }
}

