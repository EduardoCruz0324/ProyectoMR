using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int playerLives = 3; // Vidas del jugador

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RatReachedCheese()
    {
        playerLives--;
        Debug.Log("El ratón alcanzó el queso. Vidas restantes: " + playerLives);

        if (playerLives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("¡Juego terminado!");
        // Aquí puedes añadir lógica para mostrar una pantalla de fin de juego
    }
}