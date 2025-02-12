using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TowerMenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public Button rotateForwardButton;
    public Button rotateBackwardButton;
    public Button tower1Button, tower2Button, tower3Button, tower4Button;
    public GameObject tower1Prefab, tower2Prefab, tower3Prefab, tower4Prefab;

    private GameObject selectedBase;
    public List<Transform> whiteBases = new List<Transform>();
    public List<Transform> blueBases = new List<Transform>();
    public List<Transform> greenBases = new List<Transform>();
    public List<Transform> yellowBases = new List<Transform>();
    public List<Transform> orangeBases = new List<Transform>();
    public List<Transform> pinkBases = new List<Transform>();

    void Start()
    {
        menuPanel.SetActive(false);
        rotateForwardButton.onClick.AddListener(() => RotateTowers(1));
        rotateBackwardButton.onClick.AddListener(() => RotateTowers(-1));

        tower1Button.onClick.AddListener(() => TryPlaceTower(tower1Prefab, TowerController.TowerType.Archer));
        tower2Button.onClick.AddListener(() => TryPlaceTower(tower2Prefab, TowerController.TowerType.Stone));
        tower3Button.onClick.AddListener(() => TryPlaceTower(tower3Prefab, TowerController.TowerType.Fire));
        tower4Button.onClick.AddListener(() => TryPlaceTower(tower4Prefab, TowerController.TowerType.Ice));
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
        menuPanel.GetComponent<RectTransform>().position = screenPosition;
        menuPanel.SetActive(true);
    }

    public void TryPlaceTower(GameObject towerPrefab, TowerController.TowerType towerType)
    {
        if (CoinManager.instance.PurchaseTower(towerType))
        {
            PlaceTower(towerPrefab);
        }
        else
        {
            Debug.Log("Not enough coins to place this tower!");
        }
    }

    public void PlaceTower(GameObject towerPrefab)
    {
        if (selectedBase != null)
        {
            Instantiate(towerPrefab, selectedBase.transform.position, Quaternion.identity);
            selectedBase.GetComponent<BaseClickHandler>().SetOccupied(true);
            CloseMenu();
        }
    }

    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        selectedBase = null;
    }

    public void RotateTowers(int direction)
    {
        Debug.Log($"RotateTowers called with direction: {direction}");
        RotateTowersInArray(whiteBases, direction);
        RotateTowersInArray(blueBases, direction);
        RotateTowersInArray(greenBases, direction);
        RotateTowersInArray(yellowBases, direction);
        RotateTowersInArray(orangeBases, direction);
        RotateTowersInArray(pinkBases, direction);
    }

    private void RotateTowersInArray(List<Transform> baseArray, int direction)
    {
        if (baseArray.Count < 2) return;

        if (direction > 0) // Rotar hacia adelante
        {
            GameObject lastTower = baseArray[baseArray.Count - 1].childCount > 0 ? baseArray[baseArray.Count - 1].GetChild(0).gameObject : null;
            Debug.Log($"Last tower: {lastTower?.name}");

            for (int i = baseArray.Count - 1; i > 0; i--)
            {
                if (baseArray[i - 1].childCount > 0)
                {
                    GameObject tower = baseArray[i - 1].GetChild(0).gameObject;
                    tower.transform.SetParent(baseArray[i]);
                    tower.transform.position = baseArray[i].position;
                    Debug.Log($"Moved tower from {baseArray[i - 1].name} to {baseArray[i].name}");
                    Debug.Log($"New parent of {tower.name}: {tower.transform.parent.name}");
                    Debug.Log($"New position of {tower.name}: {tower.transform.position}");
                }
            }

            if (lastTower != null)
            {
                lastTower.transform.SetParent(baseArray[0]);
                lastTower.transform.position = baseArray[0].position;
                Debug.Log($"Moved last tower to {baseArray[0].name}");
                Debug.Log($"New parent of {lastTower.name}: {lastTower.transform.parent.name}");
                Debug.Log($"New position of {lastTower.name}: {lastTower.transform.position}");
            }
        }
        else if (direction < 0) // Rotar hacia atrás
        {
            GameObject firstTower = baseArray[0].childCount > 0 ? baseArray[0].GetChild(0).gameObject : null;
            Debug.Log($"First tower: {firstTower?.name}");

            for (int i = 0; i < baseArray.Count - 1; i++)
            {
                if (baseArray[i + 1].childCount > 0)
                {
                    GameObject tower = baseArray[i + 1].GetChild(0).gameObject;
                    tower.transform.SetParent(baseArray[i]);
                    tower.transform.position = baseArray[i].position;
                    Debug.Log($"Moved tower from {baseArray[i + 1].name} to {baseArray[i].name}");
                    Debug.Log($"New parent of {tower.name}: {tower.transform.parent.name}");
                    Debug.Log($"New position of {tower.name}: {tower.transform.position}");
                }
            }

            if (firstTower != null)
            {
                firstTower.transform.SetParent(baseArray[baseArray.Count - 1]);
                firstTower.transform.position = baseArray[baseArray.Count - 1].position;
                Debug.Log($"Moved first tower to {baseArray[baseArray.Count - 1].name}");
                Debug.Log($"New parent of {firstTower.name}: {firstTower.transform.parent.name}");
                Debug.Log($"New position of {firstTower.name}: {firstTower.transform.position}");
            }
        }
    }
}

