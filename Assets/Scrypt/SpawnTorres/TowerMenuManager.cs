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
        // Aseg�rate de que el men� est� desactivado al inicio
        menuPanel.SetActive(false);

        // A�ade listeners a los botones
        tower1Button.onClick.AddListener(() => PlaceTower(tower1Prefab));
        tower2Button.onClick.AddListener(() => PlaceTower(tower2Prefab));
        tower3Button.onClick.AddListener(() => PlaceTower(tower3Prefab));
        tower4Button.onClick.AddListener(() => PlaceTower(tower4Prefab));
    }

    // M�todo para abrir el men� en una base espec�fica
    public void OpenMenu(GameObject baseObject)
    {
        // Si el men� est� activo y corresponde a la misma base, ci�rralo
        if (menuPanel.activeSelf && selectedBase == baseObject)
        {
            CloseMenu();
            return;
        }


        selectedBase = baseObject;

        // Convertir la posici�n del objeto base al espacio de pantalla
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(baseObject.transform.position);

        // Posicionar el men� relativo al Canvas
        RectTransform menuRect = menuPanel.GetComponent<RectTransform>();
        menuRect.position = screenPosition;

        // Activar el men�
        menuPanel.SetActive(true);
    }

    // M�todo para colocar una torre
    public void PlaceTower(GameObject towerPrefab)
    {
        if (selectedBase != null)
        {
            Instantiate(towerPrefab, selectedBase.transform.position, Quaternion.identity); // Instanciar la torre
            selectedBase = null; // Reiniciar la base seleccionada
            menuPanel.SetActive(false); // Cerrar el men�
        }
    }

    public void CloseMenu()
    {
        // Desactiva el men�
        menuPanel.SetActive(false);
        selectedBase = null;
    }
}
