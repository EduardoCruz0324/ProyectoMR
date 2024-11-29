using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;

    private float nextFireTime;
    private Transform target;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Rat") && target == other.transform)
        {
            target = null;
        }
    }

    void Update()
    {
        if (target != null && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        Vector3 direction = (target.position - firePoint.position).normalized;
        rb.velocity = direction * bulletSpeed;

        Destroy(bullet, 3f);
    }
}
