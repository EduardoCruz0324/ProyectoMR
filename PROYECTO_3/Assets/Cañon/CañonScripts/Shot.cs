using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public Transform spawnPoint; // Punto de aparición de la bala
    public GameObject bulletPrefab; // Prefab de la bala
    public float shotForce = 1500f; // Fuerza del disparo
    public float shotRate = 0.5f; // Intervalo entre disparos
    private float nextShotTime = 0f; // Controla el tiempo del próximo disparo

    void Update()
    {
        // Solo dispara si ha pasado el tiempo suficiente
        if (Time.time >= nextShotTime)
        {
            Shoot();  // Dispara
            nextShotTime = Time.time + shotRate;  // Establece el tiempo para el siguiente disparo
        }
    }

    void Shoot()
    {
        Debug.Log("Disparando...");

        // Visualiza la dirección en la que la bala será disparada
        Debug.DrawRay(spawnPoint.position, spawnPoint.forward * 10, Color.red, 1f);  // Verás una línea roja por 1 segundo

        // Instancia una nueva bala en el punto de aparición
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);

        // Asegúrate de que el prefab de la bala tenga un Rigidbody
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            // Aplica fuerza a la bala en la dirección en la que está mirando el spawnPoint (hacia adelante)
            bulletRb.AddForce(spawnPoint.forward * shotForce, ForceMode.Impulse);  // Aplica una fuerza de impulso
            Debug.Log("Bala instanciada en: " + spawnPoint.position);

            // Destruir la bala después de 5 segundos para evitar la acumulación de objetos
            Destroy(bullet, 5f);
        }
        else
        {
            Debug.LogError("El prefab de la bala no tiene un componente Rigidbody.");
        }
    }
}