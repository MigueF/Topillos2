using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance { get; private set; }
    public int startCoins;
    public int currentCoins;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI warningText;
    [Space(30)]
    [Header("Prizes")]
    public int archer;
    public int stone;
    public int fire;
    public int ice;
    [Space(20)]
    [Header("Upgrade Prizes")]
    public int upgradeStartPrize;
    public int upgradePlusPrize;
    [Space(20)]
    [Header("Upgrade Prizes")]
    public int sellStartPrize;
    public int sellPlusPrize;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentCoins = startCoins;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateUI();
    }
    public bool SpendCoins(int amount)
    {
        if (currentCoins >= amount)
        {
            currentCoins -= amount;
            UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Insufficient coins!");
            ShowWarning("Not enough coins!");
            return false;
        }
    }
    public void UpdateUI()
    {
        coinText.text = currentCoins.ToString();
    }
    void ShowWarning(string message)
    {
        if (warningText != null)
        {
            warningText.text = message;
            Invoke("ClearWarning", 3f);
        }
    }
    void ClearWarning()
    {
        if (warningText != null)
        {
            warningText.text = ""; // Clear the warning text
        }
    }

    public bool CanAfford(int amount)
    {
        return currentCoins >= amount;
    }

    public bool PurchaseTower(TowerController.TowerType towerType)
    {
        int price = 0;
        switch (towerType)
        {
            case TowerController.TowerType.Archer:
                price = archer;
                break;
            case TowerController.TowerType.Stone:
                price = stone;
                break;
            case TowerController.TowerType.Fire:
                price = fire;
                break;
            case TowerController.TowerType.Ice:
                price = ice;
                break;
        }

        return SpendCoins(price);
    }
}


