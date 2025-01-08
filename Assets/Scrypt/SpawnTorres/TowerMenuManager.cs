using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TowerMenuManager : MonoBehaviour
{
    public GameObject menuPanel; // Panel del menú principal
    public GameObject colorSelectionPanel; // Panel de selección de color
    public Button rotateForwardButton; // Botón para rotar hacia adelante
    public Button rotateBackwardButton; // Botón para rotar hacia atrás

    public Button tower1Button, tower2Button, tower3Button, tower4Button; // Botones de torres
    public GameObject tower1Prefab, tower2Prefab, tower3Prefab, tower4Prefab; // Prefabs de torres

    private GameObject selectedBase; // Base seleccionada
    private List<GameObject> selectedBases; // Lista de bases seleccionadas por color

    void Start()
    {
        menuPanel.SetActive(false);
        colorSelectionPanel.SetActive(false);

        tower1Button.onClick.AddListener(() => PlaceTower(tower1Prefab));
        tower2Button.onClick.AddListener(() => PlaceTower(tower2Prefab));
        tower3Button.onClick.AddListener(() => PlaceTower(tower3Prefab));
        tower4Button.onClick.AddListener(() => PlaceTower(tower4Prefab));

        rotateForwardButton.onClick.AddListener(() => {
            OpenColorSelection();
            RotateTowers(1);
        });

        rotateBackwardButton.onClick.AddListener(() => {
            OpenColorSelection();
            RotateTowers(-1);
        });
    }

    public void OpenMenu(GameObject baseObject)
    {
        if (menuPanel.activeSelf && selectedBase == baseObject)
        {
            CloseMenu();
            return;
        }

        selectedBase = baseObject;

        Vector2 screenPosition = Camera.main.WorldToScreenPoint(baseObject.transform.position);
        RectTransform menuRect = menuPanel.GetComponent<RectTransform>();
        menuRect.position = screenPosition;

        menuPanel.SetActive(true);
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        selectedBase = null;
    }

    public void PlaceTower(GameObject towerPrefab)
    {
        if (selectedBase != null)
        {
            GameObject newTower = Instantiate(towerPrefab, selectedBase.transform.position, Quaternion.identity);
            selectedBase.GetComponent<BaseClickHandler>().SetOccupied(true, newTower);
            CloseMenu();
        }
    }

    public void OpenColorSelection()
    {
        menuPanel.SetActive(false); // Oculta el menú principal
        colorSelectionPanel.SetActive(true); // Muestra la selección de color
    }

    public void SelectBases(string color)
    {
        selectedBases = new List<GameObject>();

        // Buscar todas las bases del color seleccionado
        foreach (BaseClickHandler baseHandler in FindObjectsOfType<BaseClickHandler>())
        {
            if (baseHandler.baseColor == color)
            {
                selectedBases.Add(baseHandler.gameObject);
            }
        }

        colorSelectionPanel.SetActive(false);
        Debug.Log("Bases seleccionadas: " + selectedBases.Count);
    }

    public void RotateTowers(int direction)
    {
        if (selectedBases == null || selectedBases.Count == 0) return;

        List<GameObject> towers = new List<GameObject>();

        // Recolectar las torres de las bases seleccionadas
        foreach (GameObject baseObj in selectedBases)
        {
            BaseClickHandler baseHandler = baseObj.GetComponent<BaseClickHandler>();
            if (baseHandler != null && baseHandler.GetTower() != null)
            {
                towers.Add(baseHandler.GetTower());
            }
            else
            {
                towers.Add(null); // Añadir vacío si no hay torre
            }
        }

        // Verificar que la lista de torres tenga el mismo tamaño que la lista de bases seleccionadas
        if (towers.Count != selectedBases.Count)
        {
            Debug.LogError("La lista de torres y la lista de bases seleccionadas no tienen el mismo tamaño.");
            return;
        }

        // Rotación de torres
        if (direction > 0) // Hacia adelante
        {
            GameObject lastTower = towers[towers.Count - 1];
            for (int i = towers.Count - 1; i > 0; i--)
            {
                towers[i] = towers[i - 1];
            }
            towers[0] = lastTower;
        }
        else if (direction < 0) // Hacia atrás
        {
            GameObject firstTower = towers[0];
            for (int i = 0; i < towers.Count - 1; i++)
            {
                towers[i] = towers[i + 1];
            }
            towers[towers.Count - 1] = firstTower;
        }

        // Actualizar las posiciones de las torres
        for (int i = 0; i < selectedBases.Count; i++)
        {
            if (towers[i] != null)
            {
                towers[i].transform.SetParent(selectedBases[i].transform);
                towers[i].transform.localPosition = Vector3.zero;
            }
        }
    }
}
