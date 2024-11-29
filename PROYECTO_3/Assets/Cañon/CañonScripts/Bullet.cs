
     using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage = 10;  // Daño que aplica la bala

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si la bala colisiona con un ratón
        if (other.CompareTag("Rat"))
        {
            Debug.Log("Ratón golpeado por la bala.");

            // Destruye el ratón
            Destroy(other.gameObject);

            // Destruye la bala
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Asegúrate de que la bala tenga coordenadas globales correctas al instanciarla
        transform.position = transform.position;
        transform.rotation = transform.rotation;
    }
}