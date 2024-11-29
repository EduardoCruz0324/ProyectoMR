using UnityEngine;

public class Rat : MonoBehaviour
{
    public Transform cheeseTarget; // Referencia al objetivo (queso)
    public float speed = 2.0f;     // Velocidad de movimiento del ratón

    void Update()
    {
        // Si el objetivo del queso está asignado, mueve al ratón hacia él
        if (cheeseTarget != null)
        {
            Vector3 direction = (cheeseTarget.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el ratón colisiona con el queso
        if (other.CompareTag("queso"))
        {
            Debug.Log("El ratón tocó el queso y será destruido.");
            Destroy(gameObject); // Destruye el ratón
        }
    }
}