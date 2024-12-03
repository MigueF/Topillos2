using UnityEngine;
using UnityEngine.UI;

public class TowerMenuManager : MonoBehaviour
{
    public GameObject menuPanel; // El Panel que contiene los botones
    public Button tower1Button, tower2Button, tower3Button, tower4Button; // Referencias a los botones
    public GameObject tower1Prefab, tower2Prefab, tower3Prefab, tower4Prefab; // Prefabs de las torres

    private GameObject selectedBase; // Base seleccionada

    void Start()
    {
        // Asegúrate de que el menú esté desactivado al inicio
        menuPanel.SetActive(false);

        // Añade listeners a los botones
        tower1Button.onClick.AddListener(() => PlaceTower(tower1Prefab));
        tower2Button.onClick.AddListener(() => PlaceTower(tower2Prefab));
        tower3Button.onClick.AddListener(() => PlaceTower(tower3Prefab));
        tower4Button.onClick.AddListener(() => PlaceTower(tower4Prefab));
    }

    // Método para abrir el menú en una base específica
    public void OpenMenu(GameObject baseObject)
    {
        // Si el menú está activo y corresponde a la misma base, ciérralo
        if (menuPanel.activeSelf && selectedBase == baseObject)
        {
            CloseMenu();
            return;
        }


        selectedBase = baseObject;

        // Convertir la posición del objeto base al espacio de pantalla
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(baseObject.transform.position);

        // Posicionar el menú relativo al Canvas
        RectTransform menuRect = menuPanel.GetComponent<RectTransform>();
        menuRect.position = screenPosition;

        // Activar el menú
        menuPanel.SetActive(true);
    }

    // Método para colocar una torre
    public void PlaceTower(GameObject towerPrefab)
    {
        if (selectedBase != null)
        {
            Instantiate(towerPrefab, selectedBase.transform.position, Quaternion.identity); // Instanciar la torre
            selectedBase = null; // Reiniciar la base seleccionada
            menuPanel.SetActive(false); // Cerrar el menú
        }
    }

    public void CloseMenu()
    {
        // Desactiva el menú
        menuPanel.SetActive(false);
        selectedBase = null;
    }
}
