using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rat"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
