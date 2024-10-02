using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeManager : MonoBehaviour
{
    public GameObject upgradeCanvas;
    public Button touchButton;
    public Button upgradeButton;
    public Button sellButton;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI upgradePrizeText;
    public TextMeshProUGUI sellPrizeText;
    private TowerController towerController;
    private int currentLevel;
    private int currentUpgradePrize;
    private int currentsellPrize;
    void Start()
    {
        upgradeCanvas.SetActive(false);
        currentLevel = 1;
        currentUpgradePrize = CoinManager.instance.upgradeStartPrize;
        currentsellPrize = CoinManager.instance.sellStartPrize;
        levelText.text = "Level: " + currentLevel.ToString();
        upgradePrizeText.text = "Prize: " + currentUpgradePrize.ToString();
        sellPrizeText.text = "Prize: " + currentsellPrize.ToString();
        towerController = gameObject.GetComponent<TowerController>();
        if (touchButton != null)
        {
            touchButton.onClick.RemoveAllListeners();
            touchButton.onClick.AddListener(Touch);
        }
        if (upgradeButton != null)
        {
            upgradeButton.onClick.RemoveAllListeners();
            upgradeButton.onClick.AddListener(Upgrade);
        }
        if(sellButton != null)
        {
            sellButton.onClick.RemoveAllListeners();
            sellButton.onClick.AddListener(Sell);
        }
    }
    private void Update()
    {
        if (upgradePrizeText != null)
        {
            upgradePrizeText.text = "Prize: " + currentUpgradePrize.ToString();

            if (currentUpgradePrize <= CoinManager.instance.currentCoins)
            {
                upgradePrizeText.color = Color.white;
            }
            else if (currentUpgradePrize > CoinManager.instance.currentCoins)
            {
                upgradePrizeText.color = Color.red;
            }
        }
    }
    public void Upgrade()
    {
        if (towerController != null && currentLevel < 5)
        {
            if (CoinManager.instance.SpendCoins(currentUpgradePrize))
            {
                towerController.attackRange += 0.5f;
                towerController.attackRate += 0.5f;
                towerController.damage += 5;

                //towerController.iceVariables.iceSlowRate += 0.05f;

                towerController.fireVariables.damageOverTimeInterval -= 0.1f;
                towerController.fireVariables.fireDamage += 2;

                towerController.stoneVariables.damageOverTimeInterval -= 0.1f;
                towerController.stoneVariables.stoneDamage += 2;

                towerController.RangeCircle();

                currentLevel += 1;

                currentUpgradePrize += CoinManager.instance.upgradePlusPrize;
                currentsellPrize += CoinManager.instance.sellPlusPrize;
                upgradePrizeText.text = "Prize: " + currentUpgradePrize.ToString();
                sellPrizeText.text = "Prize: " + currentsellPrize.ToString();

                levelText.text = "Level: " + currentLevel.ToString();
                if (currentLevel >= 5)
                {
                    levelText.text = "Max: " + currentLevel.ToString();
                }
            }
            else if (towerController == null)
            {
                towerController = gameObject.GetComponent<TowerController>();
                return;
            }
        }
    }
    public void Sell()
    {
        CoinManager.instance.AddCoins(currentsellPrize);       
        sellPrizeText.text = "Prize: " + currentsellPrize.ToString();
        Destroy(gameObject);
    }
    public void Touch()
    {
        upgradeCanvas.SetActive(true);

        if (touchButton != null)
        {
            touchButton.onClick.RemoveAllListeners();
            touchButton.onClick.AddListener(Close);
        }
    }
    public void Close()
    {
        upgradeCanvas.SetActive(false);

        if (touchButton != null)
        {
            touchButton.onClick.RemoveAllListeners();
            touchButton.onClick.AddListener(Touch);
        }
    }
}
