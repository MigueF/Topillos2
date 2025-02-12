using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int vidas = 3;
    public TextMeshProUGUI Vidastext;
    private bool isPaused = false; // Variable para rastrear el estado de pausa

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cambiartextovidas();
    }

    public void ReduceLives(int amount)
    {
        vidas = vidas - amount;
        Cambiartextovidas();

        Debug.Log("ReduceLives called. Lives remaining: " + vidas);

        // Llamar al método de flash de la pantalla
        if (ScreenFlash.instance != null)
        {
            Debug.Log("Calling ScreenFlash.instance.Flash()");
            ScreenFlash.instance.Flash();
        }
        else
        {
            Debug.LogWarning("ScreenFlash.instance is null");
        }

        if (vidas <= 0)
        {
            // SceneManager.LoadScene("MainMenu");
            Application.Quit();
        }
    }

    void Cambiartextovidas()
    {
        Vidastext.text = "" + vidas;
    }

    // Método para pausar y reanudar el juego
    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0f; // Pausar el juego
        }
        else
        {
            Time.timeScale = 1f; // Reanudar el juego
        }
    }
}


