using UnityEngine;

public class GameManage : MonoBehaviour
{
    public static GameManage Instance;
    public int lives = 3;

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

    public void LoseLife()
    {
        lives--;
        Debug.Log($"Lives remaining: {lives}");
        if (lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over");
        // Aquí puedes cargar una escena de fin del juego o mostrar un mensaje
    }
}
