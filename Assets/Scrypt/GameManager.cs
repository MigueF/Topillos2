using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int vidas = 3;
    public TextMeshProUGUI Vidastext;

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

        if (vidas <= 0)
        {
           // SceneManager.LoadScene("MainMenu");
           Application.Quit();
        }
    }
    void Cambiartextovidas()
    {
        Vidastext.text = "Vidas: " + vidas;
    }
}

