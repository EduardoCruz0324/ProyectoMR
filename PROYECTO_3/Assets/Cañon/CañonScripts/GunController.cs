using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform spawnPoint; // Punto de aparición de la bala
    public float bulletSpeed = 1000f; // Velocidad de la bala
    public float fireRate = 1.0f; // Tasa de disparo (bala por segundo)

    private void Start()
    {
        // Comienza a disparar automáticamente cuando empieza el juego
        StartCoroutine(ShootAutomatically());
    }

    private IEnumerator ShootAutomatically()
    {
        while (true) // Esto asegura que el disparo se realice indefinidamente
        {
            Shoot(); // Dispara la bala
            yield return new WaitForSeconds(1f / fireRate); // Espera entre disparos según fireRate
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Faltan referencias al prefab de la bala o al punto de aparición.");
            return;
        }

        // Instancia una nueva bala en el punto de aparición
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.velocity = spawnPoint.forward * bulletSpeed; // Aplica velocidad a la bala
            Debug.Log("Bala disparada.");
        }
        else
        {
            Debug.LogError("El prefab de la bala no tiene un componente Rigidbody.");
        }

        // Destruye la bala después de 5 segundos para evitar la acumulación de objetos
        Destroy(bullet, 5f);
    }
}