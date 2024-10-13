using UnityEngine;
using TMPro;    

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
            // Lógica para manejar la derrota del jugador
            Debug.Log("Game Over");
        }
    }
    void Cambiartextovidas()
    {
        Vidastext.text = "Vidas: " + vidas;
    }
}

